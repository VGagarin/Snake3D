using System.Collections;
using UnityEngine;

public class WallCreator : MonoBehaviour {
	[SerializeField] private GameObject wallPrefab;
	private byte mapSize = Map.MapSize;

	public IEnumerator Create() {
		for (byte i = 0; i < mapSize; i++) {
			CreateLines(i);
			yield return null;
		}
	}

	private void CreateLines(byte lineNum) {
		for (byte j = 0; j < mapSize; j++) {
			CreateWallElement(new Vector3(-1, lineNum, j));
			CreateWallElement(new Vector3(lineNum, -1, j));
			CreateWallElement(new Vector3(lineNum, j, mapSize));
		}
	}

	private void CreateWallElement(Vector3 position) =>
		Instantiate(wallPrefab, position, transform.rotation, this.transform);
}
