using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour {

	[SerializeField] Material activeMaterial;
	[SerializeField] Material inactiveMaterial;

    [Space(10)]
    public bool isActiveAtStart;
	public bool hasTimer = false;
	[Range(0, 20f)]  public float timer = 3f;

	[Space(10)]
	[SerializeField] AnimatedPlateform[] linkedAnimatedPlateform = new AnimatedPlateform[0];
	[SerializeField] PathFollowedPlateform[] linkedPathFollowedPlateform = new PathFollowedPlateform[0];

	[Space(10)]
	public bool isMovePausable = false;

	MeshRenderer meshRenderer;
	Animator blastAnimator;
	bool isActive;
    float timeUntilSwitchState;
	bool isLockedByLinkedElements = true;

	void Start() {
		this.meshRenderer = this.GetComponentInChildren<MeshRenderer>();
		this.blastAnimator = this.GetComponentInChildren<Animator>();

        this.timeUntilSwitchState = Time.time;

        this.isActive = this.isActiveAtStart;
        this.SetState(true);
	}

	void OnTriggerEnter(Collider other) {
        if (!this.isMovePausable && this.hasTimer && this.isActive != this.isActiveAtStart)
            return;

		if (other.CompareTag("Blast")) {
			this.SwitchState();
		}
	}

    void Update() {
        if (this.hasTimer && this.isActiveAtStart != this.isActive && Time.time > this.timeUntilSwitchState) {

			if (this.IsLinkedPathFollowedPlateformFinished())
				this.SwitchState();
        }
    }

	void SwitchState() {
        if (this.blastAnimator.IsInTransition(0))
            return;

        this.isActive = !this.isActive;
        this.SetState(false);
        if (this.hasTimer)
            this.timeUntilSwitchState = Time.time + this.timer;
    }

    void SetState(bool isInit) {
        this.blastAnimator.SetBool("IsLoaded", this.isActive);

        if (this.isActive) {
            this.meshRenderer.material = this.activeMaterial;
		} else {
            this.meshRenderer.material = this.inactiveMaterial;
        }

		if (!isInit)
			this.SetLinkedPathFollowedPlateform();
		this.SetLinkedAnimPlatform();
    }

	// Linked Animated Plateform
	void SetLinkedAnimPlatform() {
		foreach (AnimatedPlateform element in this.linkedAnimatedPlateform) {
			element.SwitchState();
		}
	}
	//

	// Linked Path Followed Plateform
	void SetLinkedPathFollowedPlateform() {
		foreach (PathFollowedPlateform element in this.linkedPathFollowedPlateform) {
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
}
