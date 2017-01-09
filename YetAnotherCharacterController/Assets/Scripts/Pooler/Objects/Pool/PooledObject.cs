using UnityEngine;
using System.Collections;

public class PooledObject : MonoBehaviour {
	public ObjectPool Pool {get; set;}

	[System.NonSerialized]
	ObjectPool poolInstanceForPrefab;

	public void ReturnToPool() {
		if (Pool) {
			Pool.AddObject(this);
		} else {
			if (this != null)
				Destroy(this.gameObject);
		}
	}

	public T GetPooledInstance<T>(Transform dynamicsFolder) where T : PooledObject {
		if (!poolInstanceForPrefab) {
			poolInstanceForPrefab = ObjectPool.GetPool(this, dynamicsFolder);
			poolInstanceForPrefab.transform.SetParent(dynamicsFolder);
		}
		return (T)poolInstanceForPrefab.GetObject();
	}

}
