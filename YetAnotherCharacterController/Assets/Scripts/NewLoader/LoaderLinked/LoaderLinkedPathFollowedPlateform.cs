using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(NewLoader))]
public class LoaderLinkedPathFollowedPlateform : LoaderLinked {
	[SerializeField] protected LinkedPathFollowedSettings[] linkedElements = new LinkedPathFollowedSettings[0];

	protected override void InitializeLinkedsElements() {
		foreach(LinkedPathFollowedSettings elementSettings in linkedElements) {
			elementSettings.Initialize();
		}
	}

	protected override void SetState(bool isActive) {
		foreach(LinkedPathFollowedSettings elementSettings in linkedElements) {
		bool container;

			if (!this.loader.isOnTimer) {
				if (this.loader.isActiveAtStart) {
					if (this.loader.isActiveAtStart != isActive)
						container = !isActive;
						//elementSettings.plateform.Move(!isActive);
					else
						container = isActive;
						//elementSettings.plateform.Move(isActive);
				} else {
				container = isActive;
					//elementSettings.plateform.Move(isActive);
				}

			} else {
				container = true;
				//elementSettings.plateform.Move(true);
			}

			elementSettings.plateform.Move(container);
		}


	}
}
