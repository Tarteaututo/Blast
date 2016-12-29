using UnityEngine;
using System.Collections;

public class BumperSelfLoad : Bumper {
	protected Animator switchAnimator;

	protected override void Start() {
		this.particleSystem = this.GetComponentInChildren<ParticleSystem>();
		this.overlayRenderer = this.GetComponentInChildren<MeshRenderer>();

		this.isBumpActive = this.isActiveAtStart;//this.isBumpActiveAtStart;

		this.switchAnimator = this.GetComponentInChildren<Animator>();
		this.SetSwitchValues();
	}

	public override void Switch() {
		if (this.isOnTimer || this.switchAnimator.IsInTransition(0))
			return;

		this.isBumpActive = !this.isBumpActive;
		this.isOnBump = true; // tweak delay after anim

		this.SetSwitchValues();
	}

	protected override void SetSwitchValues() {
		if (this.isBumpActive) {
			this.switchAnimator.SetBool("IsSwitchOn", true);
		} else {
			this.switchAnimator.SetBool("IsSwitchOn", false);
		}
		StartCoroutine(this.SetActivation());
	}
}
