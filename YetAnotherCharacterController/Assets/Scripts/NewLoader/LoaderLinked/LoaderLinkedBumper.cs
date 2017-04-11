using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NewLoader))]
public class LoaderLinkedBumper : LoaderLinked {
	[SerializeField] protected LinkedBumperSettings[] linkedBumpers = new LinkedBumperSettings[0];

	protected override void InitializeLinkedsElements() {
		foreach (LinkedBumperSettings bumperSettings in this.linkedBumpers) {
			bumperSettings.Initialize();
		}
	}

	protected override void SetState(bool isActive) {
		foreach (LinkedBumperSettings bumperSettings in this.linkedBumpers) {
			bumperSettings.Initialize();
			bumperSettings.bumper.NewSwitchByLoader();
		}
	}
}
