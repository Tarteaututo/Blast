using UnityEngine;
using System.Collections;

public class PlayerHUDManager: PlayerHUDManagerSingle {
	[HideInInspector] public ScaleImageWithTimer retryFeedback;

	protected override void Awake() {
		this.retryFeedback = this.transform.FindChild("RetryFeedback").GetComponent<ScaleImageWithTimer>();
	}
}
