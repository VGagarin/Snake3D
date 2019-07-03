using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class AI {
	public static DirectionOption GetRandomDirectionOption(PointNode node) {
		IEnumerable<PointNode> availableNeighbors = node.NeighborsInAccess;
		if (availableNeighbors.Count() > 0) {
			PointNode nextNode = GetRandomElement(ref availableNeighbors);
			return DirectionOption.IdentifyDirectionOption(node.Position, nextNode.Position);
		}
		
		return DirectionOption.IdentifyDirectionOption(node.Position, node.Neighbors.First().Position);
	}

	public static T GetRandomElement<T>(ref IEnumerable<T> colection){
		int index = Random.Range(0, colection.Count());
		return colection.ElementAt(index);
	}

	public static Stack<PointNode> GetPathForTargetOrEmptyPath(PointNode clientNode) {
		PointNode target = GetTargetOrNull(clientNode);
		if (target != null)
			return PathFinder.FindPath(clientNode, target);
		return new Stack<PointNode>();
	}

	public static PointNode GetTargetOrNull(PointNode node) {
		PointNode target = null;
		if (Map.ActiveMap.Items.Count > 0)
			target = FindTarget(node, Map.ActiveMap.Items);
		return target;
	}

	public static PointNode FindTarget(PointNode startNode, List<Item> items) {
		Item target = (from item in items
					   orderby (PathFinder.GetHeuristicPathLength(startNode.Position, item.Position))
					   select item).First();
		return target.Node;
	}
}
