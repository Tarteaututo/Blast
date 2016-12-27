using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {
	
	public bool isActive = false;

	[Range(0f, 10f)] public int reloadAmmo = 3;

	[Space(10)]
	public Charger[] linkedCharger;

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player")) {
			if (!this.isActive) {
				GameManager.Instance.SetLastAvailableSpawner(this);
			}
			other.GetComponent<BlastGun>().Ammo = reloadAmmo;
			this.ReloadLinkedCharger();
		}
	}

	void ReloadLinkedCharger() {
		for (int i = 0; i < this.linkedCharger.Length; i++) {
			this.linkedCharger[i].RechargeCharger();
		}
	}
}
