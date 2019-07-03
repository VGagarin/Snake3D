using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCycle : MonoBehaviour {
	[SerializeField] private WallCreator wallCreator;
	[SerializeField] private ItemSpawner itemSpawner;
	[SerializeField] private SnakeSpawner snakeSpawner;
	[SerializeField] UI ui;
	private List<Snake> snakes;
	private Map map;
	private float delayBeforeMove = 0.05f;
	private float delayCoef = 1f;
	private float minDelayCoef = 0.4f;
	private float maxDelayCoef = 3f;

	public void Start() => StartCoroutine(InitializeAndStart());

	public IEnumerator InitializeAndStart() {
		yield return StartCoroutine(wallCreator.Create());
		map = new Map();
		itemSpawner.ActiveMap = map;
		snakeSpawner.Spawn();
		StartCoroutine(Run());
	}

	public IEnumerator Run() {
		snakes = snakeSpawner.Snakes;
		while (snakes.Count > 1) {
			itemSpawner.Spawn();
			NextSnakesMove();
			yield return new WaitForSeconds(delayBeforeMove * delayCoef);
		}
		ui.AddWinnerRecord(snakes?[0]);
		StopAllCoroutines();
	}

	protected void NextSnakesMove() {
		for (int i = 0; i < snakes.Count; i++) {
			if (!snakes[i].IsAlive) {
				RemoveSnake(snakes[i--]);
				continue;
			}
			snakes[i].NextMove();
		}
	}

	protected void RemoveSnake(Snake snake) {
		snakes.Remove(snake);
		ui.AddDeathRecord(snake);
		snake.AncestorDied();
	}

	public void IncreaseDelay(float increaseCoef) {
		delayCoef += minDelayCoef * increaseCoef;
		CorrectingDelayCoef();
	}

	private void CorrectingDelayCoef() {
		if (delayCoef < minDelayCoef)
			delayCoef = minDelayCoef;
		if (delayCoef > maxDelayCoef)
			delayCoef = maxDelayCoef;
	}
}
