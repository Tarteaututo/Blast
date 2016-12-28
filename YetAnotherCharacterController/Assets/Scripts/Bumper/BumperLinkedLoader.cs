using UnityEngine;
using System.Collections;

public class BumperLinkedLoader : Bumper {


	protected override void Start() {
		base.Start();

		if (this.hasTimer) {
			StartCoroutine(OnSelfTimer());
		}
	}

	IEnumerator OnSelfTimer() {
		yield return new WaitForSeconds(this.timer);

		this.SwitchByLoader(!this.isBumpActive);

		yield return OnSelfTimer();
	}

	public void SwitchByLoader(bool isActivate) {
		if (!this.isActiveAtStart)
			isActivate = !isActivate;
	
		this.isBumpActive = isActivate;

		this.isOnBump = true;
		StartCoroutine(SetActivation());
	}
}
