using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

	[SerializeField] public GameObject camPlayer;
	[SerializeField] public Transform gun;

	[HideInInspector] public WeaponOffset weaponOffset;
	[HideInInspector] public GunAnimation gunAnimation;

	FpsWalkerController charController;

	public float timerRetry = 1f;
	bool canRetry = true;
	bool inputRetry = false;
	bool isLockRetry = false;
	float timeInputRetryHeld;

	void Awake() {
		this.weaponOffset = this.gun.GetComponentInChildren<WeaponOffset>();
		this.gunAnimation = this.weaponOffset.gunAnimation;
		this.charController = this.GetComponent<FpsWalkerController>();
	}

	void Update() {
		this.GetRetryInput();
	}

	void GetRetryInput() {
		this.inputRetry = Input.GetKey(KeyCode.R);
		if (this.inputRetry) {
			if  (this.canRetry) {
				this.canRetry = false;
				this.timeInputRetryHeld = Time.time + this.timerRetry;
			} else if (this.timeInputRetryHeld < Time.time && !this.isLockRetry) {
				this.Retry();
			}
			
			if (!this.isLockRetry)
				PlayerHUDManager.Instance.retryFeedback.UpdateScale(this.timeInputRetryHeld - Time.time, this.timerRetry);

		} else if (!this.canRetry) {
			this.canRetry = true;
			this.isLockRetry = false;
			PlayerHUDManager.Instance.retryFeedback.UpdateScale(1, 1);

		}
	}

	void Retry() {
		this.isLockRetry = true;
		LevelManager.Instance.SpawnAtFirstAvailableSpawner();
		PlayerHUDManager.Instance.retryFeedback.UpdateScale(1, 1);
		//iTween.Stop(this.gameObject);
	}

}