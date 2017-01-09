using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MainMenuCanvas : MonoBehaviour {
	Transform mainMenuPanel;	
	Button playButton;
	Transform selectLevelPanel;
	string gameSceneName = "Game";
	string tutorielBumperLoaderSceneName = "Level - BumperLoader";


	void Awake() {
		this.mainMenuPanel = this.transform.FindChild("MainMenuPanel");
		this.selectLevelPanel = this.transform.FindChild("SelectLevelPanel");
		this.playButton = this.GetComponentInChildren<Button>();

		this.selectLevelPanel.gameObject.SetActive(false);
		this.mainMenuPanel.gameObject.SetActive(true);
	}

	public void ButtonPlay() {
		this.ToggleSelectLevelPanel();
		this.ToggleMainMenuPanel();
	}

	public void ButtonBack() {
		this.ToggleSelectLevelPanel();
		this.ToggleMainMenuPanel();
	}

	public void ButtonTutorielBumperLoader() {
		SceneManager.LoadScene(this.gameSceneName);
		SceneManager.LoadScene(this.tutorielBumperLoaderSceneName, LoadSceneMode.Additive);

	}

	void ToggleSelectLevelPanel() {
		this.selectLevelPanel.gameObject.SetActive(!this.selectLevelPanel.gameObject.activeSelf);
	}

	void ToggleMainMenuPanel() {
		this.mainMenuPanel.gameObject.SetActive(!this.mainMenuPanel.gameObject.activeSelf);
	}

}
