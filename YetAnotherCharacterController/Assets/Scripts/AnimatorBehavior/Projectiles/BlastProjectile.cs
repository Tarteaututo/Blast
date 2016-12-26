using UnityEngine;
using System.Collections;

public class BlastProjectile : MonoBehaviour {
	[SerializeField] ParticleSystem onHitParticulesEffectPrefab;

	void OnCollisionEnter(Collision other) {
		this.DestroyProjectile();
	}

	public void DestroyProjectile() {
		Instantiate(this.onHitParticulesEffectPrefab, this.transform.position, this.transform.rotation);	

		Destroy(this.gameObject);
	}
}
