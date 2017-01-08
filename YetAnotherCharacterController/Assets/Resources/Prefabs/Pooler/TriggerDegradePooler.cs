using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TriggerDegradePooler : MonoBehaviour {
	public LinkedParticlePoolerSettings[] particlePooler;
	bool isPoolerDegraded = false;

	void OnTriggerEnter(Collider other) {
		if (!this.isPoolerDegraded && other.CompareTag("Player")) {
			this.DegradePooler();
			StartCoroutine(this.AfterTriggerEnter());
		}
	}

	void DegradePooler() {
		this.isPoolerDegraded = true;

		for (int i = 0; i < this.particlePooler.Length; i++) {
			this.particlePooler[i].Load(true);
		}
	}

	//TMP
	IEnumerator AfterTriggerEnter() {
		yield return new WaitForSeconds(10f);
		this.LoadStartGameScene();
	}

	void LoadStartGameScene() {
		SceneManager.LoadScene("StartGame");
	}
}
