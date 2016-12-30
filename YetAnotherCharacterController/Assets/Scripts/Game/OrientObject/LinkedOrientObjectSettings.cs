using UnityEngine;
using System.Collections;

[System.Serializable]
public class LinkedOrientObjectSettings {
	public OrientObject orientObject;
	[HideInInspector] public Transform [] positionsList;

	public bool isActiveAtStart = false;

	public void Initialize() {
		this.orientObject.isActiveAtStart = this.isActiveAtStart;
		Debug.Log("Initialize");
	}

	public void SwitchState() {
		this.orientObject.SwitchState();
	}
}
