﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathFollowedPlateform : MonoBehaviour {
	public bool isMovingAtStart = false;
	public float speed = 10f;
	[Range(0f, 20f)] public float delay = 0;

	public iTween.EaseType easeType = iTween.EaseType.linear;
	public iTween.LoopType loopType = iTween.LoopType.none;

	GameObject plateform;
	[SerializeField] public List<List<Transform>> path = new List<List<Transform>>();
	int currentPath = 0;

	[HideInInspector] public bool isMoving;
	[HideInInspector] public bool isPaused = false;


	void Awake() {
		this.plateform = this.transform.FindChild("Plateform").gameObject;

		Transform pathFolder = this.transform.FindChild("PathsFolder");
		
		foreach (Transform pathElement in pathFolder) {
			List<Transform> pathContainer = new List<Transform>();
			foreach(Transform position in pathElement) {
				pathContainer.Add(position);
			}
			this.path.Add(pathContainer);
		}

	}

	void Start() {
		this.isMoving = this.isMovingAtStart;
		if (this.isMoving)
			StartCoroutine(this.Move());
	}

	public void MoveSwitch(bool hasToMove) {
		if (!hasToMove && this.isMoving) {
			iTween.Pause(this.plateform);
			this.isMoving = false;
			this.isPaused = true;
			return;
		}

		if (this.isPaused) {
			this.isPaused = false;
			this.isMoving = true;
			iTween.Resume(this.plateform);
			return;
		}

		if (hasToMove)
			StartCoroutine(this.Move());
	}

	public void MoveFlipFlop() {
		StartCoroutine(this.Move());
	}

	IEnumerator Move() {
		yield return new WaitForSeconds(this.delay);
		if (this.currentPath > this.path.Count - 1)
			this.currentPath  = 0;
		
			iTween.MoveTo(this.plateform, iTween.Hash(
		"name", "PathFollowedPlateform",
		"path", this.path[this.currentPath++].ToArray(),
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
		this.isMoving = true;
	}

	void OnEndMove() {
		if (this.loopType == iTween.LoopType.none) 
			iTween.Stop(this.plateform);
		this.isMoving = false;
		this.isPaused = false;
	}

	/*
	TODO : 
	comportement voulu
		Si loopType == none
			bloquer l'animation du loader, empecher la réactivation.
		si '' == pingpong ou loop
			bloquer l'animation du loader, mais permettre la réactivation.
			Mettre en pause le mouvement à la désactivation et le réactiver
			si réactivé.
	
	void OnBeginMove() {
		if (this.loopType == iTween.LoopType.none)	
			this.isMoving = true;
	}

	void OnEndMove() {
		if (this.loopType == iTween.LoopType.none) {
			iTween.Stop(this.plateform);
			this.isMoving = false;
		}
	}
	*/
}
