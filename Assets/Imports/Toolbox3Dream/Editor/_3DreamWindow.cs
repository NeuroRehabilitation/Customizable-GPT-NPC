using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if(UNITY_EDITOR)
using UnityEditor;
#endif

public class _3DreamWindow: EditorWindow {

	string myString = "Test 3Dream Window";

	bool groupEnabled;
	bool myBool = true;
	float myFloat = 1.23f;

	Color sectionColor = new Color(0.1f, 0.5f, 0.4f, 0.7f);

    Rect leftSection = new Rect();
    Rect rightSection = new Rect();

    // Add menu named "My Window" to the Window menu
    [MenuItem("Tools/Toolbox 3Dream")]
	public static void Init () {
		// Get existing open window or if none, make a new one:
		_3DreamWindow window = (_3DreamWindow)EditorWindow.GetWindow (typeof (_3DreamWindow));
		window.titleContent = new GUIContent ("3Dream");
		window.Show();
	}

	/// <summary>
	/// Similar to Start() or Awake()
	/// </summary>
	void OnEnable()
	{
		DrawLayouts ();
	}

	/// <summary>
	/// Similar to any Update function,
	/// Not called once per frame. Called 1 or more times per interaction
	/// </summary>
	void OnGUI () {

		GUILayout.BeginArea (leftSection);
		DoControls ();
		GUILayout.EndArea ();

		GUILayout.BeginArea (rightSection);
		DoCanvas ();
		GUILayout.EndHorizontal ();
	}


	void DoControls()
	{
		GUILayout.BeginVertical();

		GUILayout.Label ("Base Settings", EditorStyles.boldLabel);
		myString = EditorGUILayout.TextField ("Text Field", myString);

		groupEnabled = EditorGUILayout.BeginToggleGroup ("Optional Settings", groupEnabled);
		myBool = EditorGUILayout.Toggle ("Toggle", myBool);
		myFloat = EditorGUILayout.Slider ("Slider", myFloat, -3, 3);
		EditorGUILayout.EndToggleGroup ();

		DropAreaGUI ();

		GUILayout.EndVertical ();
	}


	void DoCanvas()
	{
		GUILayout.BeginVertical ();

		GUILayout.Label ("Toolbar", EditorStyles.largeLabel);
		sectionColor = EditorGUILayout.ColorField ("Paint Color", sectionColor);
		GUILayout.Button ("Fill All");

		GUILayout.EndVertical ();
	}

	void DropAreaGUI ()
	{
		var evt = Event.current;
		var dropArea = GUILayoutUtility.GetRect (0.0f, 50.0f, GUILayout.ExpandWidth (true));

		GUI.Box (dropArea, "Add Waypoint");

		switch (evt.type) {
		case EventType.DragUpdated:
		case EventType.DragPerform:
			if (!dropArea.Contains (evt.mousePosition))
				break;

			DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

			if (evt.type == EventType.DragPerform) {
				DragAndDrop.AcceptDrag ();

				foreach (var draggedObject in DragAndDrop.objectReferences) {
					var go = draggedObject as GameObject;
					if (!go)
						continue;

//					var waypoint = go.GetComponent<WayPoint> ();
//					if (!waypoint)
//						continue;
//
//					AddWaypoint ();
				}
			}

			Event.current.Use ();
			break;
		}
	}

	/// <summary>
	/// Defines Rect values and paint textures based on Rects
	/// </summary>
	void DrawLayouts()
	{
		leftSection.position = Vector2.zero;
		leftSection.width = Screen.width * 0.5f;
		leftSection.height = Screen.height;

		leftSection.position = new Vector2(Screen.width * 0.5f, 0.0f);
		leftSection.width = Screen.width * 0.5f;
		leftSection.height = Screen.height;
	}
}