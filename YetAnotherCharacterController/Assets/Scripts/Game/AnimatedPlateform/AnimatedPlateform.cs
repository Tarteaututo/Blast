using UnityEngine;
using System.Collections;

public abstract class AnimatedPlateform : MonoBehaviour {
	protected Animator animator;
	protected virtual void Awake() {
		this.animator = this.GetComponentInChildren<Animator>();
	}

	protected virtual void Start() {}

	public abstract void SwitchState();
}
