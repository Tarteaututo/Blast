using UnityEngine;
using System.Collections;

public class BumperLinkedLoader : Bumper {

	protected override void Start() {
		base.Start();

	}

	public void SwitchByLoader(bool isActivate) {
		this.isBumpActive = isActivate;
		this.isOnBump = true;
		StartCoroutine(SetActivation());
	}
}
