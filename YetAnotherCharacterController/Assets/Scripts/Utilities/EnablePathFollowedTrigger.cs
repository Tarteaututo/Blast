using UnityEngine;
using System.Collections;

public class EnablePathFollowedTrigger : MonoBehaviour {
	public NewPathFollowedPlatform target;

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player")) {
			this.ToggleEnable();
		}
	}

	void ToggleEnable() {
		this.target.enabled = !this.target.enabled;
	}
}
