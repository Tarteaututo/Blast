using UnityEngine;
using System.Collections;

[System.Serializable]
public class LinkedAnimatedPlateformSettings {
	public AnimatedPlateform animatedPlateform;

	public void Initialize(bool isActive) {
		this.animatedPlateform.settings.hasTimer = false;
		this.animatedPlateform.isActiveAtStart = isActive;
	}
}
