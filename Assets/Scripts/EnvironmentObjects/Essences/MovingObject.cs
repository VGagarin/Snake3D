public class MovingObject : BaseObject {
	protected PointNode PreviousNode { get; set; }

	public override void Move(PointNode newPointNode) {
		ChangePreviousNode(Node);
		base.Move(newPointNode);
	}

	protected void ChangePreviousNode(PointNode node) => PreviousNode = node;
}
