using UnityEngine;
using System.Collections;

public class GameManagerSingle : MonoBehaviour {
	protected static GameManager instance;

	public bool isOriginal = false;

	public static GameManager Instance {
		get {
			if (instance == null) {
				GameManager[] list = GameObject.FindObjectsOfType<GameManager>();
				foreach (GameManager element in list) {
					if (element.GetComponent<GameManager>().isOriginal) {
						instance = element;
					} else {
						Destroy(element.gameObject);
					}
				}
			}
			return instance;
		}
	}

	protected virtual void Awake() {
		DontDestroyOnLoad(this.gameObject);
	}
}
