using UnityEngine;
using System.Collections;

public class BlastGun : MonoBehaviour {
	public Transform blastGunWeaponOffset;
	
	public GameObject projectilePrefab;
	public float projectileSpeed = 1f;
	public iTween.EaseType easeType = iTween.EaseType.linear;

	private PlayerManager playerManager;
	private WeaponOffset weaponOffset;
	private Transform destinationOffset;

	void Start() {
		this.playerManager = this.GetComponent<PlayerManager>();
		this.weaponOffset = this.playerManager.weaponOffset;
		this.destinationOffset = this.blastGunWeaponOffset.transform.GetChild(0);
	}

	public void Launch() {
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
	}

	void OnEndBlast(BlastProjectile projectile) {
		projectile.DestroyProjectile();
	}

}
