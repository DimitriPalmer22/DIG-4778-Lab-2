using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class CubeScript : MonoBehaviour
{
    [SerializeField] public float size = 1;
}


[CustomEditor(typeof(CubeScript))] // Allows this editor to be used with the CubeScript class
[CanEditMultipleObjects] // Allow multiple objects to be edited at once
public class CubeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Create a button to select all cubes
        if (GUILayout.Button("Select All Cubes"))
        {
            var cubeScripts =
                FindObjectsOfType<CubeScript>() // Get all game objects in the scene with a CubeScript component
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
    }
}