using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {

	public bool isActive = false;

	[Range(0f, 10f)] public int reloadAmmo = 3;

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player")) {
			if (!this.isActive) {
				GameManager.Instance.SetLastAvailableSpawner(this);
			}
			other.GetComponent<BlastGun>().Ammo = reloadAmmo;
		}
	}
}
