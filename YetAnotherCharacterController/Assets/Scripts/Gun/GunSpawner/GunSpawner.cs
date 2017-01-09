using UnityEngine;
using System.Collections;

public class GunSpawner : MonoBehaviour {
	[SerializeField] TriggerGun.GunMode gunMode;

	[SerializeField] float timeEffectGetGun = 1f;
	Transform pivot;
	Transform feedbackParticle;
	bool isGunGiven = false;

	void Awake() {
		this.pivot = this.transform.FindChild("Pivot");
		this.feedbackParticle = this.transform.FindChild("Feedback");
	}

	void Update() {
		if (!this.isGunGiven)
			this.pivot.transform.Rotate(Vector3.up);
	}

	void OnTriggerEnter(Collider other) {
		if (this.isGunGiven)
			return;
		
		if (other.CompareTag("Player")) {
			this.GiveGunToPlayer(other.GetComponent<TriggerGun>());
		}
	}

	void GiveGunToPlayer(TriggerGun triggerGun) {
		this.isGunGiven = true;

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

		this.pivot.gameObject.SetActive(false);
		this.PlayFeedbackParticles();
		StartCoroutine(this.OnCoolEffect(triggerGun.gameObject));
	}

	void PlayFeedbackParticles() {
		ParticleSystem[] particles = this.feedbackParticle.GetComponentsInChildren<ParticleSystem>();

		foreach (ParticleSystem element in particles) {
			if (element.name == "ParticlesIdle") {
				element.Stop();
			} else if (element.name == "ParticlesGetGun") {
				element.Play();
			}
		}
	}

	IEnumerator OnCoolEffect(GameObject player) {
		//Time.timeScale = 0.5f;
		yield return new WaitForSeconds(this.timeEffectGetGun);

		//Time.timeScale = 1f;


	}
}