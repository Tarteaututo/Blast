using UnityEngine;
using System.Collections;

public class GunSpawner : MonoBehaviour {
	Transform pivot;
	[SerializeField] TriggerGun.GunMode gunMode;

	void Awake() {
		this.pivot = this.transform.FindChild("Pivot");
	}

	void Update() {
		this.pivot.transform.Rotate(Vector3.up);
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player")) {
			this.GiveGunToPlayer(other.GetComponent<TriggerGun>());
		}
	}

	void GiveGunToPlayer(TriggerGun triggerGun) {
		switch (gunMode) {
			case TriggerGun.GunMode.SWITCH:
				triggerGun.canSwitchGun = true;
				triggerGun.gunMode = TriggerGun.GunMode.SWITCH;
				triggerGun.SetGunMode();
				break;
			case TriggerGun.GunMode.BLAST:
				triggerGun.canBlastGun = true;
				triggerGun.gunMode = TriggerGun.GunMode.BLAST;
				triggerGun.SetGunMode();
				break;
			default:
				break;
		}
	}
}
