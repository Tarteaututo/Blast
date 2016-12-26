using UnityEngine;
using System.Collections;

public class SwitchGun : MonoBehaviour {
	public float weaponRange = 50f;
	public LayerMask layerMask;

	PlayerManager playerManager;
	Camera playerCam;

	WeaponOffset weaponOffset;
	GameObject effect;

	void Start() {
		this.playerManager = this.GetComponent<PlayerManager>();
		this.playerCam = this.GetComponentInChildren<Camera>();
		this.weaponOffset = this.playerManager.weaponOffset;
	}

	public void Launch() {
		this.weaponOffset.LaunchEffects(TriggerGun.GunMode.SWITCH);
		Vector3 rayOrigin = this.playerCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));

		RaycastHit hit;

		Ray ray = new Ray(rayOrigin, this.playerCam.transform.forward);
		if (Physics.Raycast(ray, out hit, this.weaponRange, this.layerMask)) {
			Bumper bumper = hit.collider.GetComponentInParent<Bumper>();

			if (bumper) {
				bumper.Switch();
			}
		}
	}


}
