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

	MeshRenderer meshRenderer;
	Animator blastAnimator;
	bool isActive;
    float timeUntilSwitchState;

	void Start() {
		this.meshRenderer = this.GetComponentInChildren<MeshRenderer>();
		this.blastAnimator = this.GetComponentInChildren<Animator>();

        this.timeUntilSwitchState = Time.time;

        this.isActive = this.isActiveAtStart;
        this.SetState();
	}

	void OnTriggerEnter(Collider other) {
        if (this.hasTimer && this.isActive != this.isActiveAtStart)
            return;

		if (other.CompareTag("Blast")) {
			this.SwitchState();
		}
	}

    void Update() {
        if (this.hasTimer && this.isActiveAtStart != this.isActive && Time.time > this.timeUntilSwitchState) {
            this.SwitchState();
        }
    }

	void SwitchState() {
        if (this.blastAnimator.IsInTransition(0))
            return;

        this.isActive = !this.isActive;
        this.SetState();
        if (this.hasTimer)
            this.timeUntilSwitchState = Time.time + this.timer;
    }

    void SetState() {
        this.blastAnimator.SetBool("IsLoaded", this.isActive);

        if (this.isActive) {
            this.meshRenderer.material = this.activeMaterial;
        } else {
            this.meshRenderer.material = this.inactiveMaterial;
        }

		this.SetLinkedAnimPlatform();
    }

	// Linked Animated Plateform
	void SetLinkedAnimPlatform() {
		Debug.Log(this.linkedAnimatedPlateform.Length);

		foreach (AnimatedPlateform element in this.linkedAnimatedPlateform) {
			element.SwitchState();
		}
	}

	// Linked Animated Plateform

}
