using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

[ExecuteInEditMode]

public class SphereScript : MonoBehaviour
{
    [SerializeField] public float radius = 1;

    private void OnValidate()
    {
        // scale increases proportional to radius
        transform.localScale = Vector3.one * radius;
    }
}

[CustomEditor(typeof(SphereScript))] // Allows this editor to be used with the SphereScript class
[CanEditMultipleObjects] // Allow multiple objects to be edited at once
public class SphereEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // reference to SphereScript and objects containing it 
        SphereScript Sphere = (SphereScript)target;
        
        // checks if radius is less than one 
        if (Sphere.radius < 1)
        {
            // if so show warning in Inspector
            EditorGUILayout.HelpBox("The spheres' radius cannot be smaller than 1!", MessageType.Warning);
        }

        // makes sure radius appears in inspector
        DrawDefaultInspector();

        // Make the buttons appear side by side
        EditorGUILayout.BeginHorizontal();

        // Create a button to select all cubes
        if (GUILayout.Button("Select All Spheres"))
        {
            var sphereScripts =
                FindObjectsOfType<SphereScript>(true) // Get all game objects in the scene with a SphereScript component
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
        
        // End the horizontal layout
        EditorGUILayout.EndHorizontal();
        
        // Temporarily store the current background color so we can restore it later
        var originalBackgroundColor = GUI.backgroundColor;

        // Set the background color to red if the cube is active, otherwise set it to green
        if ((target as SphereScript).gameObject.activeSelf)
            GUI.backgroundColor = Color.red;
        // Set the background color to green
        else
            GUI.backgroundColor = Color.green;
        
        // Create a button to enable / disable all cubes
        if (GUILayout.Button("Disable / Enable all spheres", GUILayout.Height(40)))
        {
            var sphereScripts = FindObjectsOfType<SphereScript>(true); // Get all game objects in the scene with a SphereScript component

            // Loop through each cube script
            foreach (var sphereScript in sphereScripts)
            {
                // Toggle the enabled state of the cube
                sphereScript.gameObject.SetActive(!sphereScript.gameObject.activeSelf);
            }
        }
        
        // Reset the background color to the original color
        GUI.backgroundColor = originalBackgroundColor;
    }
    
}