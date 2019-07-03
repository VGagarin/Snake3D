using System.Collections.Generic;
using UnityEngine;

public class SnakeSpawner : MonoBehaviour {
	[SerializeField] protected Material[] materials;
	[SerializeField] protected GameObject[] bodySections;
	[SerializeField] protected GameObject snakeHead;
	protected string[] names = { "Red", "Purple", "Orange" };
	public List<Snake> Snakes { get; } = new List<Snake>();

	public void Spawn() {
		for (int snakeNumber = 0; snakeNumber < materials.Length; snakeNumber++)
			SpawnSnake(snakeNumber);
	}

	protected void SpawnSnake(int snakeNumber) {
		PointNode node = Map.ActiveMap.RandomFreeNode;
		Snake snake = CreateSnake(snakeNumber, node);
		snake.InitializeFields(node, bodySections[snakeNumber], names[snakeNumber]);
		Snakes.Add(snake);
	}

	protected Snake CreateSnake(int snakeNumber, PointNode node) {
		GameObject snakeObject = Instantiate(snakeHead, node.Position.Vector, Quaternion.identity) as GameObject;
		ChangeMaterial(snakeObject, materials[snakeNumber]);
		return snakeObject.GetComponent<Snake>();
	}

	protected void ChangeMaterial(GameObject obj, Material material) => obj.GetComponent<MeshRenderer>().material = material;
}
