using UnityEngine;
using System.Collections;
using System;

public class RotationAnimPlateform : AnimatedPlateform {

	bool isActive;

	protected override void Awake() {
		base.Awake();
	}

	protected override void Start() {
		base.Start();

		this.isActive = this.isActiveAtStart;

		this.SetState();
	}

	public override void SwitchState() {
		this.isActive = !this.isActive;
		this.SetState();
	}

	private void SetState() {
		if (this.animator)
			this.animator.SetBool("IsHorizontal", this.isActive);
	}
}
