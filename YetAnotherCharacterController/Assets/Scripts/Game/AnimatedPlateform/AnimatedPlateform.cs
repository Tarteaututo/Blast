using UnityEngine;
using System.Collections;

public abstract class AnimatedPlateform : MonoBehaviour {
	public bool isActiveAtStart;

	[System.Serializable]
	public class Settings {
		public bool hasTimer;
		[Range(0f, 20f)] public float timer = 3;
	}

	public Settings settings = new Settings();

	protected Animator animator;

	protected virtual void Awake() {
		this.animator = this.GetComponentInChildren<Animator>();

	}

	protected virtual void Start() {

		if (this.settings.hasTimer) {
			StartCoroutine(OnTimer());
		}
	}

	IEnumerator OnTimer() {
		yield return new WaitForSeconds(this.settings.timer);

		this.SwitchState();
		yield return OnTimer();
	}

	public abstract void SwitchState();

}
