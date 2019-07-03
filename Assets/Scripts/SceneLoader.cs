using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
	public void Update() {
		if (Input.GetButtonDown("Cancel"))
			if (SceneManager.GetActiveScene().buildIndex == 1)
				SceneManager.LoadScene(0);
			else
				Application.Quit();
	}

	public void LoadScene(int scene_numb) => SceneManager.LoadScene(scene_numb);
}
