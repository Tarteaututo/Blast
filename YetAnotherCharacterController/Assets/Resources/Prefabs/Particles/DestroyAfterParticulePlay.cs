using UnityEngine;
using System.Collections;

public class DestroyAfterParticulePlay : MonoBehaviour {
	ParticleSystem particleSystem;

	void Start () {
		this.particleSystem = this.GetComponent<ParticleSystem>();
	}
	
	void Update () {
		if (this.particleSystem.isStopped) {
			Destroy(this.gameObject);
		}
	}
}
