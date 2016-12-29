using UnityEngine;
using System.Collections;

public class FeedbackSpawner : MonoBehaviour {

	[SerializeField] Material feedbackUndiscoveredMaterial;
	[SerializeField] Material feedbackActiveMaterial;
	[SerializeField] Material feedbackInactiveMaterial;

	MeshRenderer feedbackRenderer;

	ParticleSystem undiscoveredParticles;
	ParticleSystem activeParticles;
	ParticleSystem inactiveParticles;

	void Awake() {
		this.feedbackRenderer = this.GetComponent<MeshRenderer>();

		foreach (ParticleSystem child in this.GetComponentsInChildren<ParticleSystem>()) {
			if (child.name == "undiscovered") {
				this.undiscoveredParticles = child;
			} else if (child.name == "active") {
				this.activeParticles = child;
			} else if (child.name == "inactive") {
				this.inactiveParticles = child;
			}
		}
	}

	public void SetFeedbackColors(bool isActive, bool isDiscovered) {
		if (isActive) {
			if (this.feedbackActiveMaterial) {
				this.feedbackRenderer.material = feedbackActiveMaterial;
			}
			this.activeParticles.Play();
			this.inactiveParticles.Stop();
			this.undiscoveredParticles.Stop();
		} else if (isDiscovered) {
			this.feedbackRenderer.material = feedbackInactiveMaterial;
			this.activeParticles.Stop();
			this.inactiveParticles.Play();
			this.undiscoveredParticles.Stop();
		} else {
			this.feedbackRenderer.material = feedbackUndiscoveredMaterial;
			this.activeParticles.Stop();
			this.inactiveParticles.Stop();
			this.undiscoveredParticles.Play();
		}
	}
}
