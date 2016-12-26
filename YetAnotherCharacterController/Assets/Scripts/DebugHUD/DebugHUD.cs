using UnityEngine;
using System.Collections;

public class DebugHUD : MonoBehaviour {
	Canvas canvas;
	FpsWalkerController charController;
	MouseLook mouseLook;
	TriggerGun triggerGun;

	GameManager.PlayerSettings playerSettings;

	void Awake() {
		this.canvas = this.GetComponent<Canvas>();
		this.canvas.enabled = false;
	}

	void Start() {
		this.charController = GameManager.Instance.charController;
		this.mouseLook = this.charController.mouseLook;
		this.triggerGun = GameManager.Instance.triggerGun;

		this.playerSettings = GameManager.Instance.playerSettings;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Keypad0)) {
			this.SwitchDebugHUDActivation();
		}
	}

	void SwitchDebugHUDActivation() {
		bool state = this.canvas.isActiveAndEnabled;
		this.canvas.enabled = !state;
		this.mouseLook.lockCursor = state;
		Cursor.visible = true;

		this.charController.enabled = state;
		if (this.playerSettings.canTriggerGun)
			this.triggerGun.enabled = state;
	}

	// HUD Driven

	// Player Settings
	public void CanDoubleJumpButton() {
		this.playerSettings.canDoubleJump = !this.playerSettings.canDoubleJump;
		this.playerSettings.SetJumpMode(this.charController);
	}

	public void CanTriggerGunButton() {
		this.playerSettings.canTriggerGun = !this.playerSettings.canTriggerGun;
		this.playerSettings.SetCanGunMode(this.triggerGun);
	}

	public void CanSwitchGunButton() {
		this.playerSettings.canSwitchGun = !this.playerSettings.canSwitchGun;
		this.playerSettings.SetCanGunMode(this.triggerGun);
		this.triggerGun.SwitchGunMode();
	}

	public void CanBlastGunButton() {
		this.playerSettings.canBlastGun = !this.playerSettings.canBlastGun;
		this.playerSettings.SetCanGunMode(this.triggerGun);
		this.triggerGun.SwitchGunMode();
	}

	public void GunModeDropdown() {
		
	}
	// Buttons
}
