using UnityEngine;
using System.Collections;

public class Killzone : MonoBehaviour {
	
	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player")) {
			this.KillPlayer(other.transform);
		}
	}

	void KillPlayer(Transform player) {
		LevelManager.Instance.SpawnAtFirstAvailableSpawner();
	}
}
