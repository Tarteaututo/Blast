using UnityEngine;
using System.Collections;

public class ToggleLightColor : MonoBehaviour {

	[SerializeField] Color activeColor;
	[SerializeField] Color unactiveColor;
	
	Light _light;

	bool isOnLerp = false;
	bool isActive = false;
	[SerializeField] float lerpTime = 0f;
	float currentLerpTime;


	Light GetLight {
		get {
			if (this._light == null)
				this._light = this.GetComponentInChildren<Light>();
			return this._light;
		}
	}

	public void Toggle2(bool isActive) {
		this.isOnLerp = true;
		this.isActive = isActive;
	}

	public void Toggle(bool isActive) {
		if (isActive) {
			this.GetLight.color = this.activeColor;
		} else {
			this.GetLight.color = this.unactiveColor;
		}
	}

	void Update() {
		if (this.isOnLerp) {
			currentLerpTime += Time.deltaTime;
			if (this.currentLerpTime > this.lerpTime) {
				this.currentLerpTime = this.lerpTime;
				this.isOnLerp = false;

			}
			float perc = this.currentLerpTime / this.lerpTime;

			if (this.isActive)
				this.GetLight.color = Color.Lerp(this.activeColor, this.unactiveColor, perc);
			else
				this.GetLight.color = Color.Lerp(this.unactiveColor, this.activeColor, perc);
		}
	}
}
