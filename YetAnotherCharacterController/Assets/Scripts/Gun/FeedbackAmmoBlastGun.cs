using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FeedbackAmmoBlastGun : MonoBehaviour {
	Image ammoIcon;
	
	void Awake() {
		this.ammoIcon = this.GetComponent<Image>();
	}

	public void SetAmmo(int current, int max) {
		this.ammoIcon.fillAmount = ((float)current / (float)max);
	}

	public float ValueToPercentage(int value, int valueMax) {
		return ((((float)value * 100) / (float)valueMax) / 100);
	}
}
