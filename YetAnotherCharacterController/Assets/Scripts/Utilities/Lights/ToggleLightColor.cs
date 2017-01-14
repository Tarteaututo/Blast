using UnityEngine;
using System.Collections;

public class ToggleLightColor : MonoBehaviour {

	[SerializeField] Color activeColor;
	[SerializeField] Color unactiveColor;
	
	Light _light;

	Light GetLight {
		get {
			if (this._light == null)
				this._light = this.GetComponentInChildren<Light>();
			return this._light;
		}
	}

	public void Toggle(bool isActive) {
		if (isActive) {
			this.GetLight.color = this.activeColor;
		} else {
			this.GetLight.color = this.unactiveColor;
		}
	}
}
