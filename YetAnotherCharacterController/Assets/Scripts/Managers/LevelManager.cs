using UnityEngine;
using System.Collections;

public class LevelManager : LevelManagerSingle {

	[HideInInspector]
	public Transform player;

	[System.Serializable]
	public class PlayerSettings {
		public bool canDoubleJump;
		public bool canTriggerGun;
		public bool canSwitchGun;
		public bool canBlastGun;
		public TriggerGun.GunMode startGunMode;

		public void Set(FpsWalkerController charController, TriggerGun triggerGun) {
			this.SetJumpMode(charController);
			this.SetGunMode(triggerGun);
			this.SetCanGunMode(triggerGun);
			triggerGun.enabled = this.canTriggerGun;
		}

		public void SetJumpMode(FpsWalkerController charController) {
			bool newBool = this.canDoubleJump; // tweak : 
			charController.CanDoubleJump = newBool;//this.canDoubleJump;
		}

		public void SetCanGunMode(TriggerGun triggerGun) {
			triggerGun.canSwitchGun = this.canSwitchGun;
			triggerGun.canBlastGun = this.canBlastGun;

			triggerGun.SetGunMode();
		}

		public void SetGunMode(TriggerGun triggerGun) {
			triggerGun.gunMode = this.startGunMode;
		}
	}

	public PlayerSettings playerSettings;

	[HideInInspector]
	public FpsWalkerController charController;
	[HideInInspector]
	public TriggerGun triggerGun;
	Spawn[] spawns;

	protected override void Awake() {
		base.Awake();

		this.player = GameObject.FindGameObjectWithTag("Player").transform;

		if (player == null) {
			Debug.LogError("GameManager : Player not set");
			Debug.Break();
			return;
		}

		this.charController = this.player.GetComponent<FpsWalkerController>();
		this.triggerGun = this.player.GetComponent<TriggerGun>();
		this.playerSettings.Set(this.charController, this.triggerGun);

		this.spawns = FindObjectsOfType<Spawn>();
		this.SpawnAtFirstAvailableSpawner();
	}

	public void SpawnAtFirstAvailableSpawner() {
		foreach (Spawn element in this.spawns) {
			if (element.isActive) {
				this.player.transform.position = element.transform.position;
				this.player.transform.rotation = element.transform.rotation;
				return;
			}
		}
	}

	public void SetLastAvailableSpawner(Spawn spawner) {
		bool safetyFlag = false;
		foreach (Spawn element in this.spawns) {
			if (element == spawner) {
				element.isActive = safetyFlag = true;
			} else {
				element.isActive = false;
			}
			element.feedbackSpawner.SetFeedbackColors(element.isActive, element.isDiscovered);
		}

		if (!safetyFlag)
			Debug.LogError("GameManage.SetLastAvailableSpawner Probleme : aucun spawn ne corresponds");
	}
}
