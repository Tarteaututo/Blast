using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {

	public bool isActive = false;

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player")) {
			if (!this.isActive) {
				GameManager.Instance.SetLastAvailableSpawner(this);
			}
		}
	}
}
