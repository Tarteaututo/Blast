﻿using UnityEngine;
using System.Collections;

public class Bumper : MonoBehaviour {
	[SerializeField] protected Transform destination;
	[SerializeField] protected Material activeMaterial;
	[SerializeField] protected Material inactiveMaterial;

	public bool isBumpActiveAtStart = true;
	public float speed = 15f;
	public iTween.EaseType easeType = iTween.EaseType.linear;
	
	[Space(10)]
	public bool hasTimer = false;
	[Range(0, 20f)] public float timer = 1;

	protected MeshRenderer overlayRenderer;
	[HideInInspector] protected ParticleSystem particleSystem;
	protected bool isOnBump = false;
	protected bool isOnTimer = false;
	protected bool isBumpActive;

	protected virtual void Start() {
		this.particleSystem = this.GetComponentInChildren<ParticleSystem>();
		this.overlayRenderer = this.GetComponentInChildren<MeshRenderer>();

		this.isBumpActive = this.isBumpActiveAtStart;

		this.SetSwitchValues();
	}

	protected virtual void OnTriggerStay(Collider other) {
		if (this.isBumpActive && !this.isOnBump && other.CompareTag("Player")) {
			this.BumpPlayerToPosition(other.gameObject);
		}

		if (this.isBumpActive && other.CompareTag("Blast")) {
			this.BumpBlastProjectileToPosition(other.gameObject);
		}
	}

	// Bump blast projectile
	protected void BumpBlastProjectileToPosition(GameObject projectile) {
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
	
	protected void OnStartBumpBlast(BlastProjectile blastProjectile) {

	}

	protected void OnEndBumpBlast(BlastProjectile blastProjectile) {
		blastProjectile.DestroyProjectile();
	}
	//

	// Bump Player
	protected void BumpPlayerToPosition(GameObject player) {
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

	protected IEnumerator TweakOnBump() {
		yield return new WaitForSeconds(0.5f);
		this.isOnBump = false;
	}

	protected void OnStartBump(FpsWalkerController charController) {
		charController.playerControl = false;
		this.isOnBump = true;
	}

	protected void OnEndBump(FpsWalkerController charController) {
		charController.playerControl = true;
		if (charController.CanDoubleJump)
			charController.doubleJumpActive = true;
		this.isOnBump = false;
	}
	// Bump

	// Switch
	public virtual void Switch() {

	}


	protected virtual void SetSwitchValues() {
		StartCoroutine(SetActivation());
	}

	protected IEnumerator SetActivation() {
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
