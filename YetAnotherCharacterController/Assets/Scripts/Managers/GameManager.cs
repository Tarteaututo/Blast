using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : GameManagerSingle {
	
	protected override void Awake() {
		DontDestroyOnLoad(this.gameObject);

		if (!Application.isEditor)
			SceneManager.LoadScene("Level - BumperLoader", LoadSceneMode.Additive);
	}
}
