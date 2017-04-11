using UnityEngine;
using System.Collections;

public class ScaleMeshWithTimer : ScaleWithTimer {
	public Transform target;
	MeshRenderer meshRenderer;

	protected override void Awake() {
		if (this.target == null)
			this.target = this.transform;
		this.meshRenderer = this.target.GetComponentInChildren<MeshRenderer>();
		this.meshRenderer.material.color = this.timerReadyColor;
	}

	public override void UpdateScale(float value, float valueMax) {

		Vector3 newScale = this.target.localScale;
		newScale.y = ValueToPercentage(value, valueMax);
		this.target.localScale = newScale;

		this.meshRenderer.material.color = Color.Lerp(this.timerDepletedColor, this.timerReadyColor, newScale.y);
	}
}
