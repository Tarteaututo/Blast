using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {


	public bool isActive = false;
	public bool isDiscovered;

	[Range(0f, 10f)] public int reloadAmmo = 3;

	[Space(10)]
	public Charger[] linkedCharger;

	[HideInInspector] public FeedbackSpawner feedbackSpawner;

	void Start() {
		this.feedbackSpawner = this.transform.FindChild("Mesh").GetComponentInChildren<FeedbackSpawner>();
		this.isDiscovered = this.isActive;

		this.feedbackSpawner.SetFeedbackColors(this.isActive, this.isDiscovered);
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player")) {
			if (!this.isActive) {
				GameManager.Instance.SetLastAvailableSpawner(this);
				if (!this.isDiscovered) {
					this.isDiscovered = true;
				}
			}
			other.GetComponent<BlastGun>().Ammo = reloadAmmo;
			this.ReloadLinkedCharger();
			this.feedbackSpawner.SetFeedbackColors(this.isActive, this.isDiscovered);
		}
	}

	void ReloadLinkedCharger() {
		for (int i = 0; i < this.linkedCharger.Length; i++) {
			this.linkedCharger[i].RechargeCharger();
		}
	}


}
