using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class SphereScript : MonoBehaviour
{
    [SerializeField] public float radius = 1;
}

[CustomEditor(typeof(SphereScript))] // Allows this editor to be used with the SphereScript class
[CanEditMultipleObjects] // Allow multiple objects to be edited at once
public class SphereEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Create a button to select all cubes
        if (GUILayout.Button("Select All Spheres"))
        {
            var sphereScripts =
                FindObjectsOfType<SphereScript>() // Get all game objects in the scene with a SphereScript component
                    .Select(n => n.gameObject) // Select the game object attached to the SphereScript component
                    .ToArray(); // Convert the IEnumerable to an array

            // Select all the cubes
            Selection.objects = sphereScripts;
        }
        
        // Create another button to clear the selection
        if (GUILayout.Button("Clear Selection"))
        {
            // Reset the selection to an empty array
            Selection.objects = new Object[0];
        }
    }
}