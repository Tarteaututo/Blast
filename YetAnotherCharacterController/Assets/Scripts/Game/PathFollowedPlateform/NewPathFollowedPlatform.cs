using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NewPathFollowedPlatform : MonoBehaviour {
	public float speed = 10f;
	[Range(0f, 20f)]
	public float delay = 0;

	public iTween.EaseType easeType = iTween.EaseType.linear;
	public iTween.LoopType loopType = iTween.LoopType.none;

	GameObject plateform;
	PlatformFeedback platformFeedback;
	[SerializeField]
	public List<List<Transform>> path = new List<List<Transform>>();

	[HideInInspector] public bool isActive;
	[HideInInspector] public bool isLinked = false;
	public bool isPathSequentiel = false;
	public bool isActiveAtStart = true;
	bool isFirstTime = true;

	int currentPathindex = 0;

	void Awake() {
		this.GetPath();
	}

	void Start() {
		this.isActive = this.isActiveAtStart;
		if (this.platformFeedback)
			this.platformFeedback.Init(this.isActive);

		if (!this.isLinked) {
			if (this.isActive) {
				this.Move(true);
			}
		}
	}

	void GetPath() {
		this.plateform = this.transform.FindChild("Plateform").gameObject;
		this.GetPlatformFeedback();

		Transform pathFolder = this.transform.FindChild("PathsFolder");

		foreach (Transform pathElement in pathFolder) {
			List<Transform> pathContainer = new List<Transform>();
			foreach (Transform position in pathElement) {
				pathContainer.Add(position);
			}
			this.path.Add(pathContainer);
		}
	}

	void GetPlatformFeedback() {
		this.platformFeedback = this.plateform.transform.FindChild("Mesh").GetComponentInChildren<PlatformFeedback>();
	}

	public void Move(bool hasToMove) {
		//Debug.Log(hasToMove + " hasToMove | Start " + this.isActiveAtStart + " | isActive : " + this.isActive);

		if (!hasToMove && !this.isActive && !this.isActiveAtStart)
			return;
		//if ((this.isActiveAtStart && hasToMove == this.isActive) || !this.isActiveAtStart && hasToMove != this.isActive)
			StartCoroutine(this.OnMove());
	}

	IEnumerator OnMove() {
		yield return new WaitForSeconds(this.delay);
		iTween.MoveTo(this.plateform, iTween.Hash(
			"name", "PathFollowedPlateform",
			"path", this.path[this.GetPathIndex++].ToArray(),
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
			//"orienttopath", true
		));
	}

	void OnBeginMove() {
		this.isActive = true;

		if (this.platformFeedback)
			this.platformFeedback.SwitchState();
	}

	void OnEndMove() {
		if (this.loopType == iTween.LoopType.none)
			iTween.Stop(this.plateform);
		this.isActive = false;

		//this.isPaused = false;

		if (!this.isLinked && this.isPathSequentiel)
			this.Move(true);
	}

	int GetPathIndex {
		get {
			if (this.loopType == iTween.LoopType.pingPong)
				return 0;

			return currentPathindex;
		}
		set {
			this.currentPathindex = value;
			if (this.currentPathindex > this.path.Count -1)
				this.currentPathindex = 0;
		}
	}

}

