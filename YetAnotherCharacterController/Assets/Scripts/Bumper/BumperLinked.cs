using UnityEngine;
using System.Collections;

public class BumperLinked : Bumper {


	protected override void Start() {
		base.Start();

		if (this.hasTimer && !this.isLinked) {
			StartCoroutine(OnSelfTimer());
		}
	}

	IEnumerator OnSelfTimer() {
		yield return new WaitForSeconds(this.timer);

		this.SelfSwitch(!this.isBumpActive);

		yield return OnSelfTimer();
	}

	public void SelfSwitch(bool isActivate) {
		
		if (!this.isActiveAtStart) {
			isActivate = !isActivate;
			Debug.Log("Switch inverted");
		}

		this.isBumpActive = isActivate;

		this.isOnBump = true;
		StartCoroutine(SetActivation());
	}

	public void NewSwitchByLoader() {
		if (this.isOnBump)
			return;
		this.isBumpActive = !this.isBumpActive;

		this.isOnBump = true;
		StartCoroutine(SetActivation());
		
		if (this.hasTimer)
			StartCoroutine(HandleLocalTimer());
	}

	IEnumerator HandleLocalTimer() {
		yield return new WaitForSeconds(this.timer);
		this.AfterTimerSwitchByLoader();
	}

	public void AfterTimerSwitchByLoader() {
		if (this.isOnBump)
			return;
		this.isBumpActive = !this.isBumpActive;

		this.isOnBump = true;
		StartCoroutine(SetActivation());

	}
}
