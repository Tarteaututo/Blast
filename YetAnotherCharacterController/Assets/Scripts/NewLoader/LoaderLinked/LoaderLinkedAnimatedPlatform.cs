using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NewLoader))]
public class LoaderLinkedAnimatedPlatform : LoaderLinked {
	[SerializeField] protected LinkedAnimatedPlateformSettings[] linkedAnimatedPlateform = new LinkedAnimatedPlateformSettings[0];

	protected override void InitializeLinkedsElements() {
		foreach (LinkedAnimatedPlateformSettings element in this.linkedAnimatedPlateform) {
			element.Initialize(this.loader.isActiveAtStart);
		}
	}

	protected override void SetState(bool isActive) {
		foreach (LinkedAnimatedPlateformSettings element in this.linkedAnimatedPlateform) {
			element.animatedPlateform.SwitchState();
		}
	}
}
