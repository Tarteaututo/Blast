using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SwitchButton : MonoBehaviour {
	public NewPathFollowedPlatform selfPathFP;
	public GameObject goToActive;
	public GameObject goToDeactive;

	public enum ButtonBehaviour {
		PLAY,
		BACKFROMLEVEL,
		STARTTUTORIEL,
		LEVELBUMPERLOADER
	}

	public ButtonBehaviour buttonBehaviour;

	public void Toggle() {
		switch (this.buttonBehaviour) {
			case ButtonBehaviour.PLAY:
				this.ButtonPlay();
				break;
			case ButtonBehaviour.BACKFROMLEVEL:
				this.ButtonBackFromLevel();
				break;
			case ButtonBehaviour.LEVELBUMPERLOADER:
				this.ButtonLevelBumperLoader();
				break;
			case ButtonBehaviour.STARTTUTORIEL:
				this.ButtonStartTutoriel();
				break;
			default:
		break;
		}
	
	}

	IEnumerator SelfDeactiveAfterWait() {
		yield return new WaitForSeconds(0.6f);
		this.selfPathFP.gameObject.SetActive(false);
	}

	IEnumerator DeactiveAfterWait() {
		yield return new WaitForSeconds(0.6f);
		this.goToDeactive.gameObject.SetActive(false);
	}

	void ButtonPlay() {
		this.selfPathFP.Move(true);
		this.goToActive.SetActive(true);

		foreach (NewPathFollowedPlatform element in this.goToActive.GetComponentsInChildren<NewPathFollowedPlatform>()) {
			element.Move(true);
		}

		StartCoroutine(this.SelfDeactiveAfterWait());
	}


	void ButtonBackFromLevel() {
		this.goToActive.SetActive(true);

		foreach (NewPathFollowedPlatform element in this.goToDeactive.GetComponentsInChildren<NewPathFollowedPlatform>()) {
			element.Move(true);
		}

		this.goToActive.GetComponent<NewPathFollowedPlatform>().Move(true);
		StartCoroutine(this.DeactiveAfterWait());

	}

	void ButtonLevelBumperLoader() {
		SceneManager.LoadScene("Game");
		SceneManager.LoadScene("Level - BumperLoader", LoadSceneMode.Additive);
	}

	void ButtonStartTutoriel() {
		SceneManager.LoadScene("Game");
		SceneManager.LoadScene("Level - StartTutoriel", LoadSceneMode.Additive);
	}
}
