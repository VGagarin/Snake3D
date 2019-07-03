using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour {
	[SerializeField] GameObject ApplePrefab;
	protected List<Apple> activeApples = new List<Apple>();
	public ObjectPooling<Apple> applePool = new ObjectPooling<Apple>();
	public Map ActiveMap { get; set; }
	public float TimeRespawn { get; set; }
	public int TicsForRespawn { get; set; } = 4;
	private int ticsCounter = 1;

	public void Spawn() {
		if (ticsCounter++ == TicsForRespawn) {
			SpawnApple();
			ticsCounter = 1;
		}
	}

	protected void SpawnApple() {
		if (ActiveMap.FreeNodes.Count != 0)
			InitializeApple();
	}

	protected void InitializeApple() {
		Apple apple = GetApple();
		apple.Move(ActiveMap.RandomFreeNode);
		ActiveMap.AddItem(apple);
	}

	protected Apple GetApple() {
		if (applePool.IsEmpty)
			return CreateApple();
		return applePool.Pop();
	}

	protected Apple CreateApple() {
		GameObject appleObject = Instantiate(ApplePrefab) as GameObject;
		Apple apple = appleObject.GetComponent<Apple>();
		apple.pool = applePool;
		return apple;
	}
}
