using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathFollowedPlateform : MonoBehaviour {
	public bool isMovingAtStart = false;
	public float speed = 10f;

	public iTween.EaseType easeType = iTween.EaseType.linear;
	public iTween.LoopType loopType = iTween.LoopType.none;

	GameObject plateform;
	List<Transform> path = new List<Transform>();
	bool isMoving;

	void Awake() {
		this.plateform = this.transform.FindChild("Plateform").gameObject;
		foreach (Transform element in this.transform.FindChild("Path")) {
			this.path.Add(element);
		}
	}

	void Start() {
		this.isMoving = this.isMovingAtStart;
		if (this.isMoving) {
			//this.Move();
		}
	}

	public void Move() {

	/*
	
	TODO :
	Loader.LockByLinkedObject : faire réactiver le timer par itween.

	set directement en tableau plutot qu'en liste
	
	*/

	Transform[] currentPath = new Transform[this.path.Count];
	for (int i = 0; i < this.path.Count; i++) {
		currentPath[i] = this.path[i];
	}

	iTween.MoveTo(this.plateform, iTween.Hash(
		"path", currentPath,
		"looptype", this.loopType,
		"speed", this.speed,
		"onstart", "OnBeginMove",
		"onstarttarget", this.gameObject,
		//"onstartparams", blastProjectile,
		"oncomplete", "OnEndMove",
		"oncompletetarget", this.gameObject,
		//"oncompleteparams", blastProjectile,
		"islocal", false,
		"easetype", easeType
	));

	}

	void OnBeginMove() {

	}

	void OnEndMove() {

	}

}
