using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UI : MonoBehaviour {
	[SerializeField] private List<Text> leaderbords;
	[SerializeField] private GameObject restartButton;

	public void AddDeathRecord(Snake snake) =>
		AddRecord(CreateTextRecortd(snake.Name, "died", snake.Length));

	public void AddWinnerRecord(Snake snake) {
		if (leaderbords.Count > 0)
			AddRecord(CreateTextRecortd(snake.Name, "won", snake.Length));
		EnableRestartButton();
	}

	private string CreateTextRecortd(string name, string state, int length) =>
		string.Format("{0} {1} \nLength: {2}", name, state, length);

	private void AddRecord(string text) {
		leaderbords.Last().text = text;
		leaderbords.RemoveAt(leaderbords.Count - 1);
	}

	public void EnableRestartButton() => restartButton.SetActive(true);
}
