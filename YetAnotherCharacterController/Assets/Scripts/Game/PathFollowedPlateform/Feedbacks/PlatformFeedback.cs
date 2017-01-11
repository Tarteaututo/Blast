using UnityEngine;
using System.Collections;

public class PlatformFeedback : MonoBehaviour {
	[SerializeField] Material activeMaterial;
	[SerializeField] Material unactiveMaterial;

	MeshRenderer feedbackRenderer;
	bool isActive = false;

	void Awake() {
		foreach (MeshRenderer meshRenderer in this.transform.FindChild("Mesh").GetComponentsInChildren<MeshRenderer>()) {
			if (meshRenderer.name == "feedback")
				feedbackRenderer = meshRenderer;
		}
	}

	public void Init (bool isActiveAtStart) {
		this.isActive = isActiveAtStart;
		this.SetMaterials();
	}

	public void SwitchState() {
		this.isActive = !this.isActive;
		this.SetMaterials();
	}

	public void SetMaterials() {
		if (this.isActive) {
			feedbackRenderer.material = this.activeMaterial;
		} else {
			feedbackRenderer.material = this.unactiveMaterial;
		}
	}
}
