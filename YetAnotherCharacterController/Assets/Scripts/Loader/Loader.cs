﻿using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour {

	[SerializeField] Material activeMaterial;
	[SerializeField] Material inactiveMaterial;

    [Space(10)]
    public bool isActiveAtStart;
	public bool hasTimer = false;
	[Range(0, 20f)]  public float timer = 3f;
	public bool isTimerFlipFlopLinkedElements;

	[Space(10)]
	[SerializeField] LinkedAnimatedPlateformSettings[] linkedAnimatedPlateform = new LinkedAnimatedPlateformSettings[0];
	[SerializeField] PathFollowedPlateform[] linkedPathFollowedPlateform = new PathFollowedPlateform[0];
	[SerializeField] BumperLinkedLoader[] linkedBumper = new BumperLinkedLoader[0];
	[SerializeField] LinkedBumperSettings[] linkedBumperSettings = new LinkedBumperSettings[0];
	[SerializeField] LinkedParticlePoolerSettings[] linkedPoolerSettings = new LinkedParticlePoolerSettings[0];
	[SerializeField] PoolerRing[] linkedPoolerRing = new PoolerRing[0];

	[Space(10)]
	public bool isMovePausable = false;
	public bool isLoaderHasToBeLockedByLinkedElements;

	MeshRenderer meshRenderer;
	Animator blastAnimator;
	ScaleWithTimer animationTimer;
	bool isActive;
    float timeUntilSwitchState;
	bool isLockedByLinkedElements = true;
	bool isLinkedElementFlipFlop = false;

	void Awake() {
		this.InitializeLinkedElements();

	}

	void Start() {
		this.meshRenderer = this.GetComponentInChildren<MeshRenderer>();
		this.blastAnimator = this.GetComponentInChildren<Animator>();
		this.animationTimer = this.GetComponentInChildren<ScaleWithTimer>();

        this.timeUntilSwitchState = Time.time;

        this.isActive = this.isActiveAtStart;
        this.SetState(true);
		
		if (this.hasTimer) {
			this.isActive = !this.isActiveAtStart;
			this.SwitchState();
		}
	}

	void InitializeLinkedElements() {
		foreach(LinkedParticlePoolerSettings pooler in this.linkedPoolerSettings) {
			pooler.Awake();
		}

		foreach (LinkedAnimatedPlateformSettings plateform in this.linkedAnimatedPlateform) {
			plateform.Initialize();
		}

		foreach (LinkedBumperSettings bumper in this.linkedBumperSettings) {
			bumper.Initialize();
		}
	}

	void OnTriggerEnter(Collider other) {
        if (!this.isMovePausable && this.hasTimer && this.isActive != this.isActiveAtStart)
            return;

		if (other.CompareTag("Blast")) {
			if (this.isTimerFlipFlopLinkedElements)
				this.isLinkedElementFlipFlop = false;
			this.SwitchState();
		}
	}

    void Update() {
		if (this.isActive) {
			if (this.timeUntilSwitchState > Time.time) {
				this.animationTimer.UpdateScale(this.timeUntilSwitchState - Time.time, this.timer);
			} else {
				this.animationTimer.UpdateScale(1, 1);
			}
		}

        if (this.hasTimer && this.isActiveAtStart != this.isActive && Time.time > this.timeUntilSwitchState) {
			this.SetLinkedBumper(this.isActive);

			if (!this.isLoaderHasToBeLockedByLinkedElements || this.IsLinkedPathFollowedPlateformFinished()) {
				if (this.isTimerFlipFlopLinkedElements)
					this.isLinkedElementFlipFlop = true;

				this.SwitchState();
			}
        }
    }

	void SwitchState() {
        if (this.blastAnimator.IsInTransition(0))
            return;

        this.isActive = !this.isActive;
        this.SetState(false);
        if (this.hasTimer) {
            this.timeUntilSwitchState = Time.time + this.timer;
			//this.animationTimer.isOnTimer = true;
		}
    }

    void SetState(bool isInit) {
        this.blastAnimator.SetBool("IsLoaded", this.isActive);

        if (this.isActive) {
            this.meshRenderer.material = this.activeMaterial;
		} else {
            this.meshRenderer.material = this.inactiveMaterial;
        }

		this.SetLinkedBumper(this.isActive);
		this.SetLinkedPooler(this.isActive);
		this.SetLinkedPoolerRing(this.isActive);

		if (!isInit)
			this.SetLinkedPathFollowedPlateform();
		this.SetLinkedAnimPlatform();
    }

	// Linked Animated Plateform
	void SetLinkedAnimPlatform() {
		foreach (LinkedAnimatedPlateformSettings element in this.linkedAnimatedPlateform) {
			element.animatedPlateform.SwitchState();
			// Ici : gérer le non Flip flop
		}
	}
	//

	// Linked Path Followed Plateform
	void SetLinkedPathFollowedPlateform() {
		foreach (PathFollowedPlateform element in this.linkedPathFollowedPlateform) {
			
		if (this.isTimerFlipFlopLinkedElements && this.isLinkedElementFlipFlop)
				element.MoveFlipFlop();
			else
				element.MoveSwitch(this.isActive);

			this.isLockedByLinkedElements = false;
		}
	}

	bool IsLinkedPathFollowedPlateformFinished() {
		if (this.isLockedByLinkedElements)
			return true;

		bool isAllFinished = true;
		foreach (PathFollowedPlateform element in this.linkedPathFollowedPlateform) {
			if (element.isMoving) {
				isAllFinished = false;
			}
		}
		this.isLockedByLinkedElements = isAllFinished;
		return isAllFinished;
	}
	//

	// Linked Bumper
	void SetLinkedBumper(bool activation) {
		for (int i = 0; i < this.linkedBumper.Length; i++) {
			this.linkedBumper[i].SwitchByLoader(activation);
		}
	}

	// Linked PoolerRing
	void SetLinkedPoolerRing(bool activation) {
		for (int i = 0; i < this.linkedPoolerRing.Length; i++) {
			this.linkedPoolerRing[i].SetPoolersActivation(activation);
		}
	}

	void SetLinkedPooler(bool activation) {
		for (int i = 0; i < this.linkedPoolerSettings.Length; i++) {
			this.linkedPoolerSettings[i].Load(activation);
		}
	}
}
