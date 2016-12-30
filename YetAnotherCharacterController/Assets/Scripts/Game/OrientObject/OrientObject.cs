using UnityEngine;
using System.Collections;

public class OrientObject : MonoBehaviour {
	public bool isActiveAtStart = false;

	bool isActive;

	void Start() {
	//	Debug.Log("Start");

		this.isActive = this.isActiveAtStart;
	}

	public void SwitchState() {
	//	Debug.Log("Switch State" + this.isActive);
	}
}
