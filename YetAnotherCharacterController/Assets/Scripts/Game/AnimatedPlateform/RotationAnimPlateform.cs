using UnityEngine;
using System.Collections;
using System;

public class RotationAnimPlateform : AnimatedPlateform {
	public bool isHorizontalAtStart;

	bool isHorizontal;

	protected override void Start() {
		base.Start();
		// Tweak : le Loader switch automatiquement la valeur au start, ici on mets l'inverse dans le start pour avoir la bonne.
		this.isHorizontal = this.isHorizontalAtStart;

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
