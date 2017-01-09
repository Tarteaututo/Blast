using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class StartGame : MonoBehaviour {

	void Awake() {
		SceneManager.LoadScene("Game");
		SceneManager.LoadScene("MenuRoom", LoadSceneMode.Additive);
	}
}
