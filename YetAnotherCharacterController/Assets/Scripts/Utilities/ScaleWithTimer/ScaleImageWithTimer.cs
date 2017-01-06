using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScaleImageWithTimer : ScaleWithTimer {
	public Transform target;
	Image bar;

	protected override void Awake() {
		if (this.target == null)
			this.target = this.transform;
	
		this.bar = this.target.GetChild(0).GetComponent<Image>();
		this.bar.color = this.timerReadyColor;

	}

	public override void UpdateScale(float value, float valueMax) {
		Vector3 newScale = this.target.localScale;
		newScale.y = ValueToPercentage(value, valueMax);
		this.target.localScale = newScale;

		this.bar.color = Color.Lerp(this.timerDepletedColor, this.timerReadyColor, newScale.y);
		this.bar.enabled = (value == valueMax ? false : true);
	}

}
