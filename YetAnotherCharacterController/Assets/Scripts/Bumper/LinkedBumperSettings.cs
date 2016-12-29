using UnityEngine;
using System.Collections;

[System.Serializable]
public class LinkedBumperSettings {
	public BumperLinked bumper;
	public bool isActiveAtStart;

	public bool hasTimer = false;
	[Range(0, 20f)]
	public float timer = 1;

	[HideInInspector]
	public bool isLinked = false;

	public void Initialize() {
		if (!bumper) {
			Debug.LogError("Missing reference Bumper Linked Loader");
		}
		bumper.isActiveAtStart = this.isActiveAtStart;
		bumper.hasTimer = this.hasTimer;
		bumper.timer = this.timer;
		bumper.isLinked = true;
	}
}