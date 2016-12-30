using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OrientObject : MonoBehaviour {
	[HideInInspector] public List<Transform> positionsList = new List<Transform>();


	void Awake() {
		Transform pathFolder = this.transform.FindChild("Path");
		foreach (Transform child in pathFolder) {
			this.positionsList.Add(child);
		}
	}

	void Update() {
		
	}

	public void SetEnabled(bool activation) {
		if (this.isActiveAndEnabled && activation) {
			// inverse puis coroutine disable
		} else {
			// normal puis active tout de suite
		}
		this.enabled = activation;
	}
}
