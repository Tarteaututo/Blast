using UnityEngine;
using System.Collections;

public class GunAnimation : MonoBehaviour {
	Animator animator;

	void Start() {
		//this.animator = this.GetComponent<Animator>();
		this.animator = this.GetAnimator;
	}

	Animator GetAnimator {
		get {
			if (this.animator == null)
				this.animator = this.GetComponent<Animator>();
			return this.animator;
		}
	}

	//ChangeGunMode
	public void ChangeGunMode(TriggerGun.GunMode gunMode) {
		this.GetAnimator.SetInteger("GunMode", (int)gunMode);
	}
	//

	//Switch
	public void PlaySwitchShoot() {
		this.GetAnimator.Play("GunSwitchShoot");
		this.GetAnimator.SetBool("OnShoot", true);

	}

	public void StopSwitchShoot() {
		this.GetAnimator.SetBool("OnShoot", false)  ;

	}
	//

	//Blast
	public void PlayBlastShoot() {
		this.GetAnimator.Play("GunBlastShoot");
		this.GetAnimator.SetBool("OnShoot", true);

	}

	public void StopBlastShoot() {
		this.GetAnimator.SetBool("OnShoot", false);

	}
	//
}
