using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class MeshPooled : PooledObject {
	[HideInInspector] public Rigidbody rb;
	
	MeshRenderer[] meshRenderers;

	void Awake() {
		this.rb = this.GetComponent<Rigidbody>();
		this.meshRenderers = this.GetComponentsInChildren<MeshRenderer>();
		SceneManager.sceneLoaded += delegate { this.ReturnToPool();};
	}

	public void SetMaterials(Material m) {
		for (int i = 0; i < this.meshRenderers.Length; i++) {
			this.meshRenderers[i].material = m;
		}
	}

	void OnTriggerEnter(Collider enteredCollider) {
		if (enteredCollider.CompareTag("PoolKillzone")) {
			this.ReturnToPool();
		}
	}
}
