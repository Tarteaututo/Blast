using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponOffset : MonoBehaviour {

	public GunAnimation gunAnimation;

	Camera playerCam;
	private float effectTime;
	List<GameObject> childParticleSystem = new List<GameObject>();
	private bool isOnShooting = false;

	void Start() {
		this.playerCam = this.GetComponentInParent<Camera>();
		this.effectTime = this.GetComponentInParent<TriggerGun>().fireRate;
		foreach (Transform element in this.transform) {
			childParticleSystem.Add(element.gameObject);
		}
	}

	public void LaunchEffects(TriggerGun.GunMode gunMode) {
		if (gunMode == TriggerGun.GunMode.SWITCH)
			StartCoroutine(this.LaunchSwitchEffect());
		else if (gunMode == TriggerGun.GunMode.BLAST)
			StartCoroutine(this.LaunchBlastEffect());
	}

	IEnumerator LaunchBlastEffect() {
		this.gunAnimation.PlayBlastShoot();

		this.isOnShooting = true;
		yield return new WaitForSeconds(this.effectTime);
		this.isOnShooting = false;

		this.gunAnimation.StopBlastShoot();

	}

	IEnumerator LaunchSwitchEffect() {
		foreach (GameObject element in this.childParticleSystem) {
			element.SetActive(true);
		}
		this.gunAnimation.PlaySwitchShoot();

		this.isOnShooting = true;
		yield return new WaitForSeconds(this.effectTime);
		this.isOnShooting = false;

		foreach (GameObject element in this.childParticleSystem) {
			element.SetActive(false);
		}
		this.gunAnimation.StopSwitchShoot();


		//	this.launchEffectParticleSystem.Stop();
	}

}
