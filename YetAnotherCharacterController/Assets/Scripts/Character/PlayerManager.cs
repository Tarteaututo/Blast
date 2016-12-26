using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

	[SerializeField] public GameObject camPlayer;
	[SerializeField] public Transform gun;

	[HideInInspector] public WeaponOffset weaponOffset;
	[HideInInspector] public GunAnimation gunAnimation;

	void Awake() {
		this.weaponOffset = this.gun.GetComponentInChildren<WeaponOffset>();
		this.gunAnimation = this.weaponOffset.gunAnimation;
	}
}
