using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BlastButton : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Blast")) {
			StartCoroutine(OnBlast());
		}
	}

	IEnumerator OnBlast() {
		yield return new WaitForSeconds(15f);
		SceneManager.LoadScene("StartGame");
	}
}
