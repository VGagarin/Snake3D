using System.Collections.Generic;
using System.Linq;

public class PointNode {
	public NodeStates State { get; set; } = NodeStates.Free;
	public Position Position { get; }
	public PointNode PreviousPoint { get; set; }
	public short PathLengthFromStart { get; set; }
	public short HeuristicPathLength { get; set; }
	public short EstimateFullPathLength {
		get => (short)(PathLengthFromStart + HeuristicPathLength);
	}
	public HashSet<PointNode> Neighbors { get; } = new HashSet<PointNode>();
	public IEnumerable<PointNode> NeighborsInAccess {
		get => from n in Neighbors
			   where Map.NodeInAccess(n.Position)
			   select n;
	}

	public PointNode(Position position) => Position = position;

	public void AddNeighbor(PointNode neighbor) => Neighbors.Add(neighbor);
}
