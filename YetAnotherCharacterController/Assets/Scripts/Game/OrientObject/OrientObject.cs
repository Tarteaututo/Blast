using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OrientObject : MonoBehaviour {
	
	[Range(0, 20)] public float timer = 1;
	[HideInInspector] public List<Transform> positionsList = new List<Transform>();
	[HideInInspector] public bool isLinked = false;

	Transform targetPivot;
	int nextPositionIndex = -1;
	[HideInInspector] public bool hasToMove = false;

	Quaternion currentPosition;
	Quaternion nextPosition;

	public float speed = 2;

	void Awake() {
		this.targetPivot = this.transform.FindChild("Pivot");
		Transform pathFolder = this.transform.FindChild("Path");
		foreach (Transform child in pathFolder) {
			this.positionsList.Add(child);
		}
	}

	void Start() {
		if (!this.isLinked)
			StartCoroutine(this.OnTimer());
	}

	IEnumerator OnTimer() {
		this.hasToMove = true;
		this.GetNextPosition();
		yield return new WaitForSeconds(this.timer);
		
		while (this.hasToMove)
			yield return null;
		yield return OnTimer();
	}

	void Update() {
		if (this.hasToMove) {
			this.MoveNextPosition();
		}
	}

	void MoveNextPosition() {
		// Eased rotation
		//this.targetPivot.rotation = Quaternion.RotateTowards(this.targetPivot.rotation, this.nextPosition, Time.deltaTime * this.speed * 10);

		// Non Eased rotation
		this.targetPivot.rotation = Quaternion.RotateTowards(this.targetPivot.rotation, this.nextPosition, Time.deltaTime * this.speed * 10);
		
		if (this.targetPivot.transform.rotation == this.positionsList[this.nextPositionIndex].rotation)
			this.hasToMove = false;
	}

	public int GetNextPosition() {
		this.nextPositionIndex += 1;
		if (this.nextPositionIndex > this.positionsList.Count - 1)
			this.nextPositionIndex = 0;
		
		this.currentPosition = this.transform.rotation;
		this.nextPosition = this.positionsList[nextPositionIndex].rotation;
		return this.nextPositionIndex;
	}
}
