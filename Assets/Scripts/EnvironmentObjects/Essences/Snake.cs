using System.Collections.Generic;
using UnityEngine;
using System;

public class Snake : BodySection {
	public event EventHandler<CollisionHappenedEventArgs> CollisionHappened;
	protected GameObject bodySection;
	protected Stack<PointNode> path = new Stack<PointNode>();
	protected DirectionOption directionOption;
	protected Position NextPosition => PathFinder.GetNextPosition(Node, directionOption);
	public bool IsGrows { get; set; }
	public bool IsAlive { get; set; } = true;
	public int Length { get; private set; } = 1;
	public string Name { get; private set; }

	public void InitializeFields(PointNode node, GameObject bodySection, string name) {
		Node = node;
		this.bodySection = bodySection;
		Name = name;
		directionOption = AI.GetRandomDirectionOption(node);
		CollisionHappened += CollisionHandler.OnCollisionHappend;
	}

	public void NextMove() {
		UpdatePathIfNeeded();
		Move(path.Pop());
	}

	protected void UpdatePathIfNeeded() {
		if (PathIsEmpty() || PathIsUnsafe())
			UpdatePath();
	}

	protected bool PathIsEmpty() => path.Count == 0;

	protected bool PathIsUnsafe() => !PathIsEmpty() && !MoveIsSafe(path.Peek().Position);

	protected bool MoveIsSafe(Position position) => Map.MoveIsSafe(position);

	protected void UpdatePath() {
		path = AI.GetPathForTargetOrEmptyPath(Node);

		if (PathIsEmpty())
			ExtandPath();
	}

	protected void ExtandPath() {
		ChageDirectionIfNextPositionUnsafe();
		path.Push(Map.GetNode(NextPosition));
	}

	protected void ChageDirectionIfNextPositionUnsafe() {
		if (!MoveIsSafe(NextPosition))
			directionOption = AI.GetRandomDirectionOption(Node);
	}

	public override void Move(PointNode newPointNode) {
		CheckCollision(newPointNode);

		base.Move(newPointNode);
		ChangeNodeState(newPointNode, NodeStates.Busy);
		GrowUpOrMovePreviousSection();
	}

	protected void CheckCollision(PointNode newPointNode) {
		if (newPointNode.State != NodeStates.Free) {
			var e = new CollisionHappenedEventArgs(newPointNode);
			OnCollisionHandler(e);
		}
	}

	protected virtual void OnCollisionHandler(CollisionHappenedEventArgs e) => CollisionHappened?.Invoke(this, e);

	protected void GrowUpOrMovePreviousSection() {
		if (IsGrows)
			SpawnSection();
		else
			MovePreviousSectionOrToFreeNode();
	}

	protected void SpawnSection() {
		ImplementSection(CreateBodySection());
		Length++;
		IsGrows = false;
	}

	protected BodySection CreateBodySection() {
		GameObject newSectionObject = Instantiate(bodySection, PreviousNode.Position.Vector, Quaternion.identity) as GameObject;
		return newSectionObject.GetComponent<BodySection>();
	}

	protected void ImplementSection(BodySection section) {
		section.Node = PreviousNode;
		section.PreviousSection = this.previousSection;
		this.previousSection = section;
	}
}
