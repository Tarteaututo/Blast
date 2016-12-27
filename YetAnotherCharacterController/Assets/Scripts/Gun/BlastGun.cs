using UnityEngine;
using System.Collections;

public class BlastGun : MonoBehaviour {
	public Transform blastGunWeaponOffset;
	
	public GameObject projectilePrefab;
	public float projectileSpeed = 1f;
	public iTween.EaseType easeType = iTween.EaseType.linear;

	public GameObject lowAmmoParticlePrefab;
	public GameObject lowAmmoTrailParticlePrefab;
	[Space(10)]
	public int ammoMax = 3;

	private PlayerManager playerManager;
	private WeaponOffset weaponOffset;
	private Transform destinationOffset;
	[SerializeField] int ammo = 0; // Hide

	void Start() {
		this.playerManager = this.GetComponent<PlayerManager>();
		this.weaponOffset = this.playerManager.weaponOffset;
		this.destinationOffset = this.blastGunWeaponOffset.transform.GetChild(0);
		this.Ammo = this.ammoMax;
	}

	public void Launch() {
		if (this.Ammo <= 0) {
			GameObject particleWorld = Instantiate(this.lowAmmoParticlePrefab, this.blastGunWeaponOffset.transform.position, this.blastGunWeaponOffset.transform.rotation) as GameObject;
			return;
		}
		this.Ammo -= 1;

		this.weaponOffset.LaunchEffects(TriggerGun.GunMode.BLAST);

		Vector3 destination = this.destinationOffset.transform.position;
		GameObject projectile = Instantiate(this.projectilePrefab, this.blastGunWeaponOffset.transform.position, this.blastGunWeaponOffset.transform.rotation) as GameObject;
		BlastProjectile blastProjectile = projectile.GetComponent<BlastProjectile>();

		iTween.MoveTo(projectile, iTween.Hash(
			"position", destination,
			"looptype", iTween.LoopType.none,
			"speed", this.projectileSpeed,
			"onstart", "OnStartBlast",
			"onstarttarget", this.gameObject,
			"onstartparams", blastProjectile,
			"oncomplete", "OnEndBlast",
			"oncompletetarget", this.gameObject,
			"oncompleteparams", blastProjectile,
			"islocal", true,
			"easetype", easeType
		));

	}

	void OnStartBlast(BlastProjectile projectile) {
		GameObject particleTrail = Instantiate(this.lowAmmoTrailParticlePrefab, this.blastGunWeaponOffset.transform.position, this.blastGunWeaponOffset.transform.rotation) as GameObject;
		particleTrail.transform.parent = this.transform;
	}

	void OnEndBlast(BlastProjectile projectile) {
		projectile.DestroyProjectile();
	}

	// Amoo

	public int Ammo {
		get {
			return this.ammo;
		}
		set {
			this.ammo = Mathf.Clamp(value, 0, this.ammoMax);
		}
	}
}
