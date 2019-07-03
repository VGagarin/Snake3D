using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class PathFinder {
	private static HashSet<PointNode> pendingNodes;
	private static HashSet<PointNode> scannedNodes;
	private static PointNode currentNode;
	private static PointNode currentNeighbor;
	private static PointNode endNode;

	public static Position GetNextPosition(PointNode node, DirectionOption directionOption) {
		sbyte[] coords = node.Position.Coordinates;
		coords[directionOption.AxisNumber] += directionOption.Direction;
		return new Position(coords);
	}

	public static Stack<PointNode> FindPath(PointNode start, PointNode finish) {
		Initialize(start, finish);

		while (pendingNodes.Count > 0) {
			if (NextNodeIsEnd())
				return GetPathForCurrentNode();
			NodeProcessing();
		}
		return new Stack<PointNode>();
	}

	private static void Initialize(PointNode start, PointNode finish) {
		endNode = finish;
		ZeroizeNode(start);
		InitializeNodeSets(start);
	}

	private static void InitializeNodeSets(PointNode startNode) {
		pendingNodes = new HashSet<PointNode>() { startNode };
		scannedNodes = new HashSet<PointNode>();
	}

	private static void ZeroizeNode(PointNode node) {
		node.PreviousPoint = null;
		node.PathLengthFromStart = 0;
		node.HeuristicPathLength = 0;
	}

	private static bool NextNodeIsEnd() {
		currentNode = pendingNodes.OrderBy(node => node.EstimateFullPathLength).First();
		return currentNode == endNode;
	}

	private static void NodeProcessing() {
		MoveCurrentNodeToScanned();
		BypassNeighbors();
	}

	private static void MoveCurrentNodeToScanned() {
		pendingNodes.Remove(currentNode);
		scannedNodes.Add(currentNode);
	}

	private static void BypassNeighbors() {
		foreach (PointNode neighbor in currentNode.NeighborsInAccess) {
			if (scannedNodes.Contains(neighbor))
				continue;
			currentNeighbor = neighbor;
			ProcessNeighbor();
		}
	}

	private static void ProcessNeighbor() {
		if (pendingNodes.Contains(currentNeighbor))
			UpdateIfIsFoundShorterPath();
		else
			FillNodeAndAddToPendingSet();
	}

	private static void UpdateIfIsFoundShorterPath() {
		if (IsFoundShorterPath())
			UpdatePreviousNodeAndLengthFromStart();
	}

	private static bool IsFoundShorterPath() =>
		currentNode.PathLengthFromStart + 1 < currentNeighbor.PathLengthFromStart;

	private static void UpdatePreviousNodeAndLengthFromStart() {
		currentNeighbor.PreviousPoint = currentNode;
		currentNeighbor.PathLengthFromStart = (byte)(currentNode.PathLengthFromStart + 1);
	}

	private static void FillNodeAndAddToPendingSet() {
		FillNode();
		pendingNodes.Add(currentNeighbor);
	}

	private static void FillNode() {
		UpdatePreviousNodeAndLengthFromStart();
		currentNeighbor.HeuristicPathLength = GetHeuristicPathLength(currentNeighbor.Position, endNode.Position);
	}

	public static short GetHeuristicPathLength(Position start, Position end) {
		int length = Mathf.Abs(start.X - end.X) + 
			Mathf.Abs(start.Y - end.Y) + 
			Mathf.Abs(start.Z - end.Z);
		return (short)length;
	}

	public static Stack<PointNode> GetPathForCurrentNode() {
		Stack<PointNode> path = new Stack<PointNode>();
		while (currentNode.PreviousPoint != null) {
			path.Push(currentNode);
			currentNode = currentNode.PreviousPoint;
		}
		return path;
	}
}
