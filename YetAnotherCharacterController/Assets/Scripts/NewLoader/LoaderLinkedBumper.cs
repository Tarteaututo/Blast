using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NewLoader))]
public class LoaderLinkedBumper : MonoBehaviour {
		[SerializeField] LinkedBumperSettings[] linkedBumpers = new LinkedBumperSettings[0];
		NewLoader loader;
	
	
	void Awake() {
		this.loader = this.GetComponent<NewLoader>();
		NewLoader.LinkedElement DelSetState = this.SetState;
		this.loader.linkedElements += this.SetState;
		this.InitializeLinkedsElements();

	}

	void Start() {
	}

	void InitializeLinkedsElements() {
		foreach (LinkedBumperSettings bumperSettings in this.linkedBumpers) {
			bumperSettings.Initialize();
		}
	}

	void SetState(bool isActive) {
		foreach (LinkedBumperSettings bumperSettings in this.linkedBumpers) {
			bumperSettings.bumper.NewSwitchByLoader();
		}
	}
}
