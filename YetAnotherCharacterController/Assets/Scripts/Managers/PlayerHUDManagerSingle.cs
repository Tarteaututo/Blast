using UnityEngine;
using System.Collections;

public class PlayerHUDManagerSingle : MonoBehaviour {
	protected static PlayerHUDManager instance;

	public bool isOriginal = false;

	public static PlayerHUDManager Instance {
		get {
			if (instance == null) {
				PlayerHUDManager[] list = GameObject.FindObjectsOfType<PlayerHUDManager>();
				foreach (PlayerHUDManager element in list) {
					if (element.GetComponent<PlayerHUDManager>().isOriginal) {
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
