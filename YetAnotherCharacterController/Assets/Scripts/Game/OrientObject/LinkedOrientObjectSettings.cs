using UnityEngine;
using System.Collections;

[System.Serializable]
public class LinkedOrientObjectSettings {
	public OrientObject orientObject;
	[HideInInspector] public Transform [] positionsList;

	public bool isEnabled = false;

	public void Initialize() {
		this.orientObject.isLinked = true;
		this.orientObject.StartCoroutine(OnAwake());
		
	}

	IEnumerator OnAwake() {
		yield return new WaitForEndOfFrame();

		if (this.isEnabled)
			this.orientObject.GetNextPosition();

		this.orientObject.hasToMove = this.isEnabled;

	}

	public void SwitchState() {
		this.orientObject.GetNextPosition();
		this.orientObject.hasToMove = true;
	}
}
