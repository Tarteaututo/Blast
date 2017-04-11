using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NewLoader))]
public class LoaderLinkedOrientObject : LoaderLinked {
	public LinkedOrientObjectSettings[] orientObject = new LinkedOrientObjectSettings[0];

	protected override void InitializeLinkedsElements() {
		foreach (LinkedOrientObjectSettings element in this.orientObject) {
			element.Initialize();
		}
	}

	protected override void SetState(bool isActive) {
		foreach (LinkedOrientObjectSettings element in this.orientObject) {
			element.SwitchState();
		}
	}
}
