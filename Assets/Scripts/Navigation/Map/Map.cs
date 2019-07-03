using System.Collections.Generic;
using System.Linq;

public class Map {
	public static byte MapSize { get; } = 15;
	public static Map ActiveMap { get; private set; }
	public List<PointNode> FreeNodes { get; } = new List<PointNode>();
	public List<Item> Items { get; } = new List<Item>();

	private PointNode[,,] nodes = new PointNode[MapSize, MapSize, MapSize];

	public PointNode RandomFreeNode {
		get {
			IEnumerable<PointNode> freeNodes = FreeNodes;
			PointNode node = AI.GetRandomElement(ref freeNodes);
			FreeNodes.Remove(node);
			return node;
		}
	}

	public Map() {
		ActiveMap = this;
		CreateNodes();
		AssigningNeighbors();
	}

	private void CreateNodes() {
		foreach (Position position in MapHelper.AllPositions())
			CreatePointNode(position);
	}

	private void CreatePointNode(Position pos) {
		PointNode node = new PointNode(pos);
		nodes[pos.X, pos.Y, pos.Z] = node;
		FreeNodes.Add(node);
	}

	private void AssigningNeighbors() {
		foreach (Position position in MapHelper.AllPositions())
			AssignNeighbors(position);
	}

	private void AssignNeighbors(Position pos) {
		PointNode node = GetNode(pos);
		foreach (Position neighborPosition in MapHelper.NeighborPositions(pos)) {
			PointNode neighbor = GetNode(neighborPosition);
			node.AddNeighbor(neighbor);
		}
	}

	public static PointNode GetNode (Position pos) => ActiveMap.nodes[pos.X, pos.Y, pos.Z];

	public void AddItem(Item item) => Items.Add(item);

	public static void RemoveItem(PointNode itemNode) {
		Item item = FindItem(itemNode);
		item.PutInPool();
		ActiveMap.Items.Remove(item);
	}

	private static Item FindItem(PointNode itemNode) {
		List<Item> items = ActiveMap.Items;
		for (int i = 0; i < items.Count; i++)
			if (items[i].Node == itemNode)
				return items[i];
		return null;
	}

	public bool NodeIsFree(PointNode node) => node.State == NodeStates.Free;

	public static bool MoveIsSafe(Position pos) => IsWithinDiapason(pos) && NodeInAccess(pos);

	public static bool IsWithinDiapason(Position pos) {
		var coords = pos.Coordinates;
		if (coords.Min() < 0 || coords.Max() > MapSize - 1)
			return false;
		return true;
	}

	public static bool NodeInAccess(Position pos) => (GetNode(pos).State & NodeStates.InAccess) != 0;

	public static void ChangeNodeState(PointNode node, NodeStates state) {
		UpdateFreeNodes(node, state);
		node.State = state;
	}

	private static void UpdateFreeNodes(PointNode node, NodeStates state) {
		if (state == NodeStates.Free)
			ActiveMap.FreeNodes.Add(node);
		else
			RemoveNodeFromFreeNodes(node);
	}

	private static void RemoveNodeFromFreeNodes(PointNode node) {
		int index = ActiveMap.FreeNodes.IndexOf(node);
		if (index >= 0)
			ActiveMap.FreeNodes.RemoveAt(index);
	}
}
