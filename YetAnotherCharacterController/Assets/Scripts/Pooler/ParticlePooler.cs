using UnityEngine;
using System.Collections;

public class ParticlePooler : MonoBehaviour {
	Transform poolFolder;

	public Transform PoolFolder {
		get {
			if (!this.poolFolder) {
				this.poolFolder = this.transform.GetComponentInChildren<ObjectPool>().transform;
			}
			return this.poolFolder;
		}
		set {
			this.poolFolder = value;
		}
	}

	public MeshPooled[] objectPooledPrefabs;
	
	public bool isCooldownRandom = false;
	public float cooldown = 0.1f;
	public float velocity = 15;

	public Material objectMaterial;

	float timeSinceLastSpawn;

	[System.Serializable]
	public struct FloatRange {
		public float min;
		public float max;

		public float RandomInRange {
			get {
				return Random.Range(this.min, this.max);
			}
		}
	}
	public FloatRange rangeCooldown, scaleRange, velocityRange;

	[System.Serializable]
	public struct ActiveRandom {
		public bool cooldown;
		public bool scale;
		public bool rotation;
		public bool velocity;
	}

	public ActiveRandom activeRandom;


	void FixedUpdate() {
		this.timeSinceLastSpawn += Time.deltaTime;
		float currentCD = this.Cooldown();
		if (this.timeSinceLastSpawn >= currentCD) {
			this.timeSinceLastSpawn -= currentCD;
			this.SpawnObject();
		}
	}

	float Cooldown() {
		if (!this.activeRandom.cooldown) {
			return cooldown;
		} else {
			return rangeCooldown.RandomInRange;
		}
	}

	void SpawnObject() {
		MeshPooled prefab = objectPooledPrefabs[Random.Range(0, this.objectPooledPrefabs.Length)];
		MeshPooled spawn = prefab.GetPooledInstance<MeshPooled>(this.PoolFolder);
		spawn.transform.localPosition = this.transform.position;

		this.RandomHandler(spawn);

		spawn.SetMaterials(this.objectMaterial);
	}

	void RandomHandler(MeshPooled spawn) {
		if (this.activeRandom.scale)
			spawn.transform.localScale = Vector3.one * this.scaleRange.RandomInRange;
		if (this.activeRandom.rotation)
			spawn.transform.localRotation = Random.rotation;
		if (this.activeRandom.velocity) {
			spawn.rb.velocity = this.transform.up * velocity + Random.onUnitSphere * this.velocityRange.RandomInRange;
		} else {
			spawn.rb.velocity = this.transform.up * velocity;
		}
	}

	// Loader handler
	public void SetPoolerAble(bool activation) {
		this.enabled = activation;
	}
}
