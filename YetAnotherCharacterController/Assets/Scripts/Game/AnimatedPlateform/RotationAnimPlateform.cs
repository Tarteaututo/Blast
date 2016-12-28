using UnityEngine;
using System.Collections;
using System;

public class RotationAnimPlateform : AnimatedPlateform {
	public bool isHorizontalAtStart;

	bool isHorizontal;

	protected override void Awake() {
		base.Awake();
		this.isHorizontal = this.isHorizontalAtStart;
	}

	protected override void Start() {
		base.Start();
		

		this.SetState();
	}

	public override void SwitchState() {
		this.isHorizontal = !this.isHorizontal;
		this.SetState();
	}

	private void SetState() {
		this.animator.SetBool("IsHorizontal", this.isHorizontal);
	}
}
