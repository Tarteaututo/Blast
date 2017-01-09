using UnityEngine;
using System.Collections;

public abstract class ScaleWithTimer : MonoBehaviour {
	[HideInInspector] public bool isOnTimer = false;

	public Color timerReadyColor;
	public Color timerDepletedColor;

	protected abstract void Awake();
	public abstract void UpdateScale(float value, float valueMax);

	public float ValueToPercentage(float value, float valueMax) {
		return ((((float)value * 100) / (float)valueMax) / 100);
	}
}
