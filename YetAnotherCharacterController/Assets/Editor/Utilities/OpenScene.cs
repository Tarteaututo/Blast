﻿using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Collections;

public class OpenScene : Editor {
	static string scene_Game = "Game";
	static string scene_LevelTest1 = "LevelTest - 1";
	static string scene_LevelTest2 = "LevelTest - 2";
	static string scene_LevelTest3 = "Level - BumperLoader"; 
	static string scene_LevelTest4 = "Level - Progression-Orient"; 

	static string scenesFolderPath = "Assets/Resources/Scenes/";
	static string sceneExtensiosn = ".unity";

	[MenuItem("OpenScene/LevelTest - 1", false, 11)]
	public static void OpenLevelTest1() {
		Open(scene_LevelTest1);
	}


	[MenuItem("OpenScene/LevelTest - 2", false, 12)]
	public static void OpenLevelTest2() {
		Open(scene_LevelTest2);
	}

	[MenuItem("OpenScene/Level - BumperLoader", false, 12)]
	public static void OpenLevelTest3() {
		Open(scene_LevelTest3);
	}

	[MenuItem("OpenScene/Level - Progression-Orient", false, 12)]
	public static void OpenLevelTest4() {
		Open(scene_LevelTest4);
	}


	static void Open(string scene) {
		if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo()) {
			EditorSceneManager.OpenScene(scenesFolderPath + scene_Game + sceneExtensiosn);
			EditorSceneManager.OpenScene(scenesFolderPath + scene + sceneExtensiosn, OpenSceneMode.Additive);
		}
	}
}
