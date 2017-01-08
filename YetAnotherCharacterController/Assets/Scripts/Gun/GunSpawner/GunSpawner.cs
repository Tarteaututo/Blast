using UnityEngine;
using System.Collections;

public class GunSpawner : MonoBehaviour {
	Transform pivot;
	void Awake() {
		this.pivot = this.transform.FindChild("Pivot");
	}

	void Update() {
		this.pivot.transform.Rotate(Vector3.up);
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player")) {
			
		}
	}

	void GiveGunToPlayer(TriggerGun.GunMode gunMode) {
		switch (gunMode) {
			case TriggerGun.GunMode.SWITCH:
				break;
			case TriggerGun.GunMode.BLAST:
				break;
			default:
				break;
		}
	}
}
