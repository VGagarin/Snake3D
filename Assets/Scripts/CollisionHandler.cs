using System.Collections.Generic;

public static class CollisionHandler {
	public static void OnCollisionHappend(object sender, CollisionHappenedEventArgs e) {
		Snake snake = (Snake)sender;
		if (SnakeIsPickedUpItem(e.Node))
			GrowSnakeAndRemoveItem(snake, e.Node);
		else
			KillSnake(snake);
	}

	private static bool SnakeIsPickedUpItem(PointNode node) => node.State == NodeStates.OccupiedByItem;

	private static void GrowSnakeAndRemoveItem(Snake snake, PointNode node) {
		snake.IsGrows = true;
		RemoveEatenItem(node);
	}

	private static void RemoveEatenItem(PointNode node) =>	Map.RemoveItem(node);

	private static void KillSnake(Snake snake) => snake.IsAlive = false;
}
