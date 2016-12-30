using UnityEngine;
using System.Collections;

[System.Serializable]
public class LinkedOrientObjectSettings {
	public OrientObject orientObject;
	[HideInInspector] public Transform [] positionsList;

	[System.Serializable]
	public class Settings {
		public bool isEnabled = false;

	}

	public Settings startSettings = new Settings();
	public Settings loadSettings = new Settings();

	bool isStartActive = false;

	public void Initialize() {
		this.orientObject.SetEnabled(this.startSettings.isEnabled);
		this.isStartActive = true;

	}

	public void SwitchState() {
		this.isStartActive = !this.isStartActive;
		if (!this.isStartActive) {
			this.orientObject.SetEnabled(this.loadSettings.isEnabled);
		} else {
			this.orientObject.SetEnabled(this.startSettings.isEnabled);

		}
	}
}
