using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using Rotorz.ReorderableList;

[CustomEditor(typeof(PathFollowedPlateform))]
[CanEditMultipleObjects]
 //[ExecuteInEditMode] // en ai-je besoin  ?
public class PathFollowedPlateformEditor : Editor {
	//SerializedProperty _pathProperty;

	List<Transform> myList = new List<Transform>();

	void OnEnable() {
		//this._pathProperty = serializedObject.FindProperty("path");
		
	}

	public override void OnInspectorGUI() {

		DrawDefaultInspector();
		
		//this.VisualPath();
	}

	// Reorderable list todo
	void VisualPath() {
		ReorderableListGUI.Title("Paths");
		ReorderableListGUI.ListField(myList, CustomListItem, DrawEmpty);

	}

	Transform CustomListItem(Rect position, Transform itemValue) {
		//EditorGUI.ObjectField(position, itemValue, typeof(Transform));
		return itemValue;
	}

	void DrawEmpty() {
		GUILayout.Label("No path", EditorStyles.miniLabel);
	}
	//
}
