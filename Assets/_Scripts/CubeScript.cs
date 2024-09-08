using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class CubeScript : MonoBehaviour
{
    [SerializeField] public float size = 1;
    private void OnValidate()
    {
        // scale increases proportional to size
        transform.localScale = Vector3.one * size;
    }
}


[CustomEditor(typeof(CubeScript))] // Allows this editor to be used with the CubeScript class
[CanEditMultipleObjects] // Allow multiple objects to be edited at once
public class CubeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // reference to CubeScript and objects containing it 
        CubeScript cube = (CubeScript)target;
        
        //checks if cube size is larger than 2 
        if (cube.size > 2)
        {
            // if so show warning in Inspector
            EditorGUILayout.HelpBox("The cubes' sizes cannot be bigger than 2!", MessageType.Warning);
        }
        
        //makes size appear in inspector
        DrawDefaultInspector();
        
        // Make the buttons appear side by side
        EditorGUILayout.BeginHorizontal();

        // Create a button to select all cubes
        if (GUILayout.Button("Select All Cubes"))
        {
            var cubeScripts =
                FindObjectsOfType<CubeScript>(true) // Get all game objects in the scene with a CubeScript component
                    .Select(n => n.gameObject) // Select the game object attached to the CubeScript component
                    .ToArray(); // Convert the IEnumerable to an array

            // Select all the cubes
            Selection.objects = cubeScripts;
        }

        // Create another button to clear the selection
        if (GUILayout.Button("Clear Selection"))
        {
            // Reset the selection to an empty array
            Selection.objects = new Object[0];
        }

        // End the horizontal layout
        EditorGUILayout.EndHorizontal();

        // Temporarily store the current background color so we can restore it later
        var originalBackgroundColor = GUI.backgroundColor;

        // Set the background color to red if the cube is active, otherwise set it to green
        if ((target as CubeScript).gameObject.activeSelf)
            GUI.backgroundColor = Color.red;
        // Set the background color to green
        else
            GUI.backgroundColor = Color.green;
        
        // Create a button to enable / disable all cubes
        if (GUILayout.Button("Disable / Enable all cubes", GUILayout.Height(40)))
        {
            var cubeScripts = FindObjectsOfType<CubeScript>(true); // Get all game objects in the scene with a CubeScript component

            // Loop through each cube script
            foreach (var cubeScript in cubeScripts)
            {
                // Toggle the enabled state of the cube
                cubeScript.gameObject.SetActive(!cubeScript.gameObject.activeSelf);
            }
        }
        
        // Reset the background color to the original color
        GUI.backgroundColor = originalBackgroundColor;
    }
}