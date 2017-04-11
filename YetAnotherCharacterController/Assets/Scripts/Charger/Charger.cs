using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Charger : MonoBehaviour {
	public Transform blastFeedbackAmmoPrefab;
	public Transform particulesLowAmmo;
	public int ammoMax = 3;

	Transform feedbackAmmoOffset;
	List<Transform> feedbackAmmoList = new List<Transform>();
	int ammo;

	public int Ammo {
		get {
			return this.ammo;
		}
		set {
			this.ammo = Mathf.Clamp(value, 0, this.ammoMax);
		}
	}

	void Start() {
		this.feedbackAmmoOffset = this.transform.FindChild("FeedbackAmmo");
		this.ammo = this.ammoMax;
		
		SpawnCircle(this.feedbackAmmoOffset.position, 0.5f);
	}

	void SpawnCircle(Vector3 center, float radius) {
		float slice = 2 * Mathf.PI / this.ammoMax;

		for (int i = 0; i < this.ammoMax; i++) {
			float angle = slice * i;
			float newX = (center.x + radius * Mathf.Cos(angle));
			float newY = (center.y + radius * Mathf.Sin(angle));
			Transform feedback = Instantiate(this.blastFeedbackAmmoPrefab, new Vector3(newX, newY, this.feedbackAmmoOffset.position.z), Quaternion.identity) as Transform;
			feedback.SetParent(this.feedbackAmmoOffset);
			this.feedbackAmmoList.Add(feedback);
		}
	}

	void Update() {
		this.feedbackAmmoOffset.Rotate(Vector3.forward);
		this.feedbackAmmoOffset.Rotate(Vector3.up);
	}

	public void Reload(BlastGun blastGun) {
		if (blastGun.Ammo < blastGun.ammoMax && this.Ammo > 0) {
			this.Ammo -= 1;
			blastGun.Ammo += 1;
			this.UnactiveProjectile();
		}
	}

	public void RechargeCharger() {
		this.Ammo = this.ammoMax;
		foreach (Transform element in this.feedbackAmmoList) {
			if (!element.gameObject.activeSelf)
				element.gameObject.SetActive(true);
		}
	}
	
	void UnactiveProjectile() {
		this.GetFirstVisibleProjectile().gameObject.SetActive(false);
		Instantiate(this.particulesLowAmmo, this.feedbackAmmoOffset.position, Quaternion.identity);
	}

	Transform GetFirstVisibleProjectile() {
		foreach (Transform element in this.feedbackAmmoList) {
			if (element.gameObject.activeSelf) {
				return element;
			}
		}
		return null;
	}
}
