using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : GameManagerSingle {
	
	protected override void Awake() {
		DontDestroyOnLoad(this.gameObject);

	}

}
