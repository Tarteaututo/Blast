using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoolerRing : MonoBehaviour {
	Transform poolFolder;

	public List<ParticlePooler> poolerList = new List<ParticlePooler>();

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

	public int numberOfSpawners = 20;
	public float radius = 25;
	public float tiltAngle = -20;
	public ParticlePooler poolerPrefab;

	public Material[] objectMaterials;

	void Awake() {
		this.PoolFolder = this.transform; // debug
		for (int i = 0; i < this.numberOfSpawners; i++) {
			this.CreateSpawner(i);
		}
	}

	void CreateSpawner(int index) {
		Transform rotater = new GameObject("Rotater").transform;
		rotater.SetParent(transform, false);
		rotater.localRotation = Quaternion.Euler(0, index * 360 / this.numberOfSpawners, 0);

		poolerList.Add(Instantiate<ParticlePooler>(this.poolerPrefab));
		poolerList[poolerList.Count - 1].transform.SetParent(rotater, false);
		poolerList[poolerList.Count - 1].PoolFolder = this.PoolFolder;

		poolerList[poolerList.Count - 1].transform.localPosition = new Vector3(0, 0, this.radius);
		poolerList[poolerList.Count - 1].transform.localRotation = Quaternion.Euler(this.tiltAngle, 0, 0);
		poolerList[poolerList.Count - 1].objectMaterial = objectMaterials[index % objectMaterials.Length];
	}
}
