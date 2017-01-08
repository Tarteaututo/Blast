using UnityEngine;
using System.Collections;

/*
TODO : Editor qui montre les bool en readonly
*/


public class TriggerGun : MonoBehaviour {
	public enum GunMode {
		SWITCH,
		BLAST,
		NONE
	}
	public GunMode gunMode = GunMode.SWITCH;

	public bool canSwitchGun;
	public bool canBlastGun;

	public float fireRate = 0.25f;

	[HideInInspector] public bool isOnChangeGunMode = false;
	PlayerManager playerManager;
	SwitchGun switchGun;
	BlastGun blastGun;
	GunAnimation gunAnimation;
	private float nextFire;
	private bool canTrigger = true;

	PlayerManager GetPlayerManager {
		get {
			if (this.playerManager == null)
				this.playerManager = this.GetComponent<PlayerManager>();
			return this.playerManager;
		}
	}

	GunAnimation GetGunAnimation {
		get {
			if (this.gunAnimation == null)
				this.gunAnimation = this.GetPlayerManager.gunAnimation;
				return this.gunAnimation;
		}
	}

	void Awake() {
		this.playerManager = this.GetComponent<PlayerManager>();
		this.switchGun = this.GetComponent<SwitchGun>();
		this.blastGun = this.GetComponent<BlastGun>();
		this.gunAnimation = this.playerManager.gunAnimation;

		this.gunAnimation.ChangeGunMode(this.gunMode);
	}

	void Start() {
	
	}

	void Update() {
		this.SwitchGunMode();
		if (!isOnChangeGunMode)
			this.Trigger();
	}

	public void SwitchGunMode() {
		bool inputSwitchGunMode = InputsManager.Instance.GetKey(Keys.FIRE2);
		if (inputSwitchGunMode) {
			if (this.gunMode == GunMode.BLAST && this.canSwitchGun) {
				this.gunMode = GunMode.SWITCH;
			} else if (this.gunMode == GunMode.SWITCH && this.canBlastGun) {
				this.gunMode = GunMode.BLAST;
			} else if (this.gunMode == GunMode.NONE) {
				if (this.canSwitchGun) {
					this.gunMode = GunMode.SWITCH;
				} else if (this.canBlastGun) {
					this.gunMode = GunMode.BLAST;
				}
			}

			this.SetGunMode();
		}
	}

	public void SetGunMode() {

			this.GetGunAnimation.ChangeGunMode(this.gunMode);
	}

	void Trigger() {
		float inputFire = InputsManager.Instance.GetAxisPad(Keys.FIRE1);

		if (inputFire != 0 && Time.time > this.nextFire && this.canTrigger) {
			this.canTrigger = false;
			this.nextFire = Time.time + this.fireRate;
			LaunchSelectedGunMode();
		} else {
			if (!this.canTrigger && (inputFire == 0))
				this.canTrigger = true;
		}
	}

	void LaunchSelectedGunMode() {
		switch (this.gunMode) {
			case GunMode.SWITCH:
				this.switchGun.Launch();
				break;
			case GunMode.BLAST:
				this.blastGun.Launch();
				break;
			default:
				break;
		}
	}
}
