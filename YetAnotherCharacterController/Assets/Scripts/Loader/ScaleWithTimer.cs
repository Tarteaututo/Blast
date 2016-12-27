using UnityEngine;
using System.Collections;

public class ScaleWithTimer : MonoBehaviour {
	[HideInInspector] public bool isOnTimer = false;
	public MeshRenderer meshRenderer;

	public Color timerReadyColor;
	public Color timerDepletedColor;

	void Start() {
		this.meshRenderer.material.color = this.timerReadyColor;
	}

	public void UpdateScale(float value, float valueMax) {

		Vector3 newScale = this.transform.localScale;
		newScale.y = ValueToPercentage(value, valueMax);
		this.transform.localScale = newScale;

		this.meshRenderer.material.color = Color.Lerp(this.timerDepletedColor, this.timerReadyColor, newScale.y);
	}

	public float ValueToPercentage(float value, float valueMax) {
		return ((((float)value * 100) / (float)valueMax) / 100);
	}
}
