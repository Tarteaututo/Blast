using UnityEngine;
using System.Collections;

public class NewLoader: MonoBehaviour {

	[SerializeField]
	Material activeMaterial;
	[SerializeField]
	Material inactiveMaterial;

	[Space(10)]
	public bool isActiveAtStart;
	public bool hasTimer = false;
	[Range(0, 20f)]	public float timer = 3f;

	MeshRenderer meshRenderer;
	Animator blastAnimator;
	ScaleWithTimer animationTimer;
	private bool isActive;
	float timeUntilSwitchState;
	[HideInInspector] public bool isOnTimer = false;

	public delegate void LinkedElement(bool isActive);
	public LinkedElement linkedElements;

	void Awake() {
		this.meshRenderer = this.GetComponentInChildren<MeshRenderer>();
		this.blastAnimator = this.GetComponentInChildren<Animator>();
		this.animationTimer = this.GetComponentInChildren<ScaleWithTimer>();

		this.isActive = this.isActiveAtStart;

		this.blastAnimator.SetBool("IsLoaded", this.isActive);

		if (this.isActive) {
			this.meshRenderer.material = this.activeMaterial;
		} else {
			this.meshRenderer.material = this.inactiveMaterial;
		}
		
		if (this.linkedElements != null) {
			this.linkedElements(this.isActive);
		} else {
		}
	}

	void Start() {

	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Blast") && !this.isOnTimer) {
				StartCoroutine(this.OnSwitchState(false));
		}
	}

	void Update() {
		if (this.isActive != this.isActiveAtStart) {
			if (this.hasTimer) {
				if (this.timeUntilSwitchState > Time.time) {
					this.animationTimer.UpdateScale(this.timeUntilSwitchState - Time.time, this.timer);
				} else {
		
				}
			}
		}
	}

	IEnumerator OnSwitchState(bool isInit) {

		if (!this.blastAnimator.IsInTransition(0)) {
			this.isActive = !this.isActive;
			this.SetState();

			if (this.hasTimer && !isInit) {
				this.timeUntilSwitchState = Time.time + this.timer;
			}
		}

		if (this.hasTimer && !isInit) {
			this.isOnTimer = true;

			yield return new WaitForSeconds(this.timer);
			this.animationTimer.UpdateScale(1, 1);
			this.isActive = !this.isActive;
			SetState();
			this.isOnTimer = false;
		}
	}
	
	void SetState() {
		this.blastAnimator.SetBool("IsLoaded", this.isActive);
		
		if (this.isActive) {
			this.meshRenderer.material = this.activeMaterial;
		} else {
			this.meshRenderer.material = this.inactiveMaterial;
		}
		if (this.linkedElements != null) {
			if (this.isActiveAtStart != this.isActive)
				this.linkedElements(this.isActive);
			else
				this.linkedElements(!this.isActive);
		}
	}
}
