using UnityEngine;
using System.Collections;

public class LevelManagerSingle : MonoBehaviour {
	protected static LevelManager instance;

	public bool isOriginal = false;

	public static LevelManager Instance {
		get {
			if (instance == null) {
				LevelManager[] list = GameObject.FindObjectsOfType<LevelManager>();
				foreach (LevelManager element in list) {
					if (element.GetComponent<LevelManager>().isOriginal) {
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
	}
}
