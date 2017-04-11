﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DebugHUD : MonoBehaviour {
	Canvas canvas;
	FpsWalkerController charController;
	MouseLook mouseLook;
	TriggerGun triggerGun;

	LevelManager.PlayerSettings playerSettings;

	Color[] feedbackColors = new Color[2] { Color.red, Color.green };

	void Awake() {
		this.canvas = this.GetComponent<Canvas>();
		this.canvas.enabled = false;
	}

	void Start() {
		this.charController = LevelManager.Instance.charController;
		this.mouseLook = this.charController.mouseLook;
		this.triggerGun = LevelManager.Instance.triggerGun;

		this.playerSettings = LevelManager.Instance.playerSettings;
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
		this.mouseLook.SetCursorLock(state);

		this.charController.enabled = state;
		if (this.playerSettings.startGunMode == TriggerGun.GunMode.NONE)
			this.triggerGun.enabled = state;
	}

	// HUD Driven

	// Player Settings
	public void CanDoubleJumpButton(Image feedback) {
		bool newBool;// = !this.playerSettings.canDoubleJump; // tweak : sinon il garde une référence vers fpswalkercontroller ? WTF
		//this.playerSettings.canDoubleJump = newBool;// !this.playerSettings.canDoubleJump;

		if (this.playerSettings.canDoubleJump)
			newBool = false;
		else
			newBool = true;
		this.playerSettings.canDoubleJump = newBool;// !this.playerSettings.canDoubleJump;

		this.playerSettings.SetJumpMode(this.charController);

		feedback.color = feedbackColors[this.playerSettings.canDoubleJump.GetHashCode()];
	}

	public void CanSwitchGunButton(Image feedback) {
		this.playerSettings.canSwitchGun = !this.playerSettings.canSwitchGun;
		this.playerSettings.SetCanGunMode(this.triggerGun);
		this.triggerGun.SwitchGunMode();

		feedback.color = feedbackColors[this.playerSettings.canSwitchGun.GetHashCode()];
	}

	public void CanBlastGunButton(Image feedback) {
		this.playerSettings.canBlastGun = !this.playerSettings.canBlastGun;
		this.playerSettings.SetCanGunMode(this.triggerGun);
		this.triggerGun.SwitchGunMode();

		feedback.color = feedbackColors[this.playerSettings.canBlastGun.GetHashCode()];
	}
	// HUD Driven
}
