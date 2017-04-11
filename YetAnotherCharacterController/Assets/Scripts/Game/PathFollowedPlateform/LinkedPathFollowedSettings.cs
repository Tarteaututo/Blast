using UnityEngine;
using System.Collections;

[System.Serializable]
public class LinkedPathFollowedSettings {
	public NewPathFollowedPlatform plateform;

	[HideInInspector] public bool isActiveAtStart;

	public void Initialize() {
		plateform.isActiveAtStart = this.isActiveAtStart;

		plateform.isLinked = true;
	}
}
