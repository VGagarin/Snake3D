public class BodySection : MovingObject {
	protected BodySection previousSection;
	protected bool IsTail => previousSection == null;
	public BodySection PreviousSection {
		set { previousSection = value; }
	}

	public void AncestorMoveHappened(PointNode nextNode) {
		Move(nextNode);
		MovePreviousSectionOrToFreeNode();
	}

	protected void MovePreviousSectionOrToFreeNode() {
		if (!IsTail)
			previousSection.AncestorMoveHappened(PreviousNode);
		else
			ToFreeNode(PreviousNode);
	}

	protected void ToFreeNode(PointNode node) => ChangeNodeState(node, NodeStates.Free);

	public void AncestorDied() {
		previousSection?.AncestorDied();
		ToFreeNode(Node);
		Destroy(gameObject);
	}
}
