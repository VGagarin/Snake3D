using System;

public class CollisionHappenedEventArgs : EventArgs {
	public PointNode Node { get; }

	public CollisionHappenedEventArgs(PointNode node) => Node = node;
}
