using UnityEngine;
using System.Collections;

public class Bumper : MonoBehaviour {
	[SerializeField] Transform destination;
	[SerializeField] Material activeMaterial;
	[SerializeField] Material inactiveMaterial;

	public bool isBumpActiveAtStart = true;
	public float speed = 15f;
	public iTween.EaseType easeType = iTween.EaseType.linear;
	
	[Space(10)]
	public bool hasTimer = false;
	[Range(0, 20f)] public float timer = 1;

	MeshRenderer overlayRenderer;
	Animator switchAnimator;
	[HideInInspector] ParticleSystem particleSystem;
	private bool isOnBump = false;
	private bool isOnTimer = false;
	bool isBumpActive;

	void Start() {
		this.particleSystem = this.GetComponentInChildren<ParticleSystem>();
		this.overlayRenderer = this.GetComponentInChildren<MeshRenderer>();
		this.switchAnimator = this.GetComponentInChildren<Animator>();

		this.isBumpActive = this.isBumpActiveAtStart;

		this.SetSwitchValues();
	}

	void OnTriggerStay(Collider other) {
		if (this.isBumpActive && !this.isOnBump && other.CompareTag("Player")) {
			this.BumpPlayerToPosition(other.gameObject);
		}

		if (this.isBumpActive && other.CompareTag("Blast")) {
			this.BumpBlastProjectileToPosition(other.gameObject);
		}
	}

	// Bump blast projectile
	void BumpBlastProjectileToPosition(GameObject projectile) {
		BlastProjectile blastProjectile = projectile.GetComponent<BlastProjectile>();

		Vector3 dest = this.destination.position;

		iTween.MoveTo(projectile, iTween.Hash(
			"position", dest,
			"looptype", iTween.LoopType.none,
			"speed", speed,
			"onstart", "OnStartBumpBlast",
			"onstarttarget", this.gameObject,
			"onstartparams", blastProjectile,
			"oncomplete", "OnEndBumpBlast",
			"oncompletetarget", this.gameObject,
			"oncompleteparams", blastProjectile,
			"islocal", true,
			"easetype", easeType
		));
	}
	
	void OnStartBumpBlast(BlastProjectile blastProjectile) {

	}

	void OnEndBumpBlast(BlastProjectile blastProjectile) {
		blastProjectile.DestroyProjectile();
	}
	//

	// Bump Player
	void BumpPlayerToPosition(GameObject player) {
		FpsWalkerController charController = player.GetComponent<FpsWalkerController>();
		Vector3 dest = this.destination.position;

		iTween.MoveTo(player, iTween.Hash(
			"position", dest,
			"looptype", iTween.LoopType.none,
			"speed", speed,
			"onstart", "OnStartBump",
			"onstarttarget", this.gameObject,
			"onstartparams", charController,
			"oncomplete", "OnEndBump",
			"oncompletetarget", this.gameObject,
			"oncompleteparams", charController,
			"islocal", true,
			"easetype", easeType
		));
		StartCoroutine(TweakOnBump());
	}

	IEnumerator TweakOnBump() {
		yield return new WaitForSeconds(0.5f);
		this.isOnBump = false;
	}

	void OnStartBump(FpsWalkerController charController) {
		charController.playerControl = false;
		this.isOnBump = true;
	}

	void OnEndBump(FpsWalkerController charController) {
		charController.playerControl = true;
		if (charController.CanDoubleJump)
			charController.doubleJumpActive = true;
		this.isOnBump = false;
	}
	// Bump

	// Switch
	public void Switch() {
		if (this.isOnTimer || this.switchAnimator.IsInTransition(0))
			return;

		this.isBumpActive = !this.isBumpActive;
		this.isOnBump = true; // tweak delay after anim

		this.SetSwitchValues();
	}

	void SetSwitchValues() {
		if (this.isBumpActive) {
			this.switchAnimator.SetBool("IsSwitchOn", true);
		} else {
			this.switchAnimator.SetBool("IsSwitchOn", false);
		}
		StartCoroutine(DelayAfterAnim());
	}

	IEnumerator DelayAfterAnim() {
		yield return new WaitForSeconds(0.25f);
		this.isOnBump = false;

		if (this.isBumpActive) {
			this.particleSystem.Play();
			this.overlayRenderer.material = this.activeMaterial;

		} else {
			this.particleSystem.Stop();
			this.overlayRenderer.material = this.inactiveMaterial;
		}

		if (this.hasTimer && this.isBumpActive != this.isBumpActiveAtStart)
			StartCoroutine(this.OnTimer());
	}
	// Switch

	//Timer
	IEnumerator OnTimer() {
		this.isOnTimer = true;

		yield return new WaitForSeconds(this.timer);
		this.isOnTimer = false;

		this.Switch();
	}
	//Timer
}
