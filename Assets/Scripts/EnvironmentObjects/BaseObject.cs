using UnityEngine;

public class BaseObject : MonoBehaviour {
	public PointNode Node { get; set; }
	public Position Position => Node.Position;
	protected Transform transformComponent;

	public void Awake() => transformComponent = GetComponent<Transform>();

	public virtual void Move(PointNode newPointNode) {
		ChangePosition(newPointNode.Position);
		ChangeCurrentNode(newPointNode);
	}

	protected void ChangePosition(Position newPosision) => transformComponent.position = newPosision.Vector;

	protected void ChangeCurrentNode(PointNode newPointNode) => Node = newPointNode;

	protected void ChangeNodeState(PointNode node, NodeStates state) {
		if (node != null)
			Map.ChangeNodeState(node, state);
	}
}
