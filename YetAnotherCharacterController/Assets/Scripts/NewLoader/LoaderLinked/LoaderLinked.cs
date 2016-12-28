using UnityEngine;
using System.Collections;

public abstract class LoaderLinked : MonoBehaviour {
	protected NewLoader loader;

	protected virtual void Awake() {
		this.loader = this.GetComponent<NewLoader>();
		NewLoader.LinkedElement DelSetState = this.SetState;
		this.loader.linkedElements += this.SetState;
		this.InitializeLinkedsElements();
	}

	protected virtual void Start() {
	}

	protected abstract void InitializeLinkedsElements();
	protected abstract void SetState(bool isActive);
}
