using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* SetParent(dynamicsFolder) */
public class ObjectPool : MonoBehaviour {
	
	PooledObject prefab;
	List<PooledObject> availableObjects = new List<PooledObject>();

	public PooledObject GetObject() {
		PooledObject obj;
		int lastAvailableIndex = this.availableObjects.Count - 1;

		if (lastAvailableIndex >= 0) {
			obj = this.availableObjects[lastAvailableIndex];
			availableObjects.RemoveAt(lastAvailableIndex);
			obj.gameObject.SetActive(true);
		} else {
			obj = Instantiate<PooledObject>(prefab);
			obj.transform.SetParent(this.transform, false);
			obj.Pool = this;
		}
		return obj;
	}

	public void AddObject(PooledObject obj) {
		obj.gameObject.SetActive(false);
		availableObjects.Add(obj);
	}

	public static ObjectPool GetPool (PooledObject prefab, Transform parent) {
		GameObject obj;
		ObjectPool pool;
		
		foreach (Transform child in parent) {
			if (child.name == prefab.name + " Pool") {
				obj = child.gameObject;
				if (obj) {
					pool = obj.GetComponent<ObjectPool>();
					if (pool) {
						return pool;
					}
				}
			}
		}
		
		obj = new GameObject(prefab.name + " Pool");
		DontDestroyOnLoad(obj);
		pool = obj.AddComponent<ObjectPool>();
		pool.prefab = prefab;
		return pool;
	}
}
