using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MapGenerator))]
public class MapGenEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MapGenerator mapGenerator = (MapGenerator)target;

        if (DrawDefaultInspector())
        {
            if (mapGenerator.AutoUpdate)
            {
                mapGenerator.DisplayMap();
            }
        }

        if (GUILayout.Button("Generate")) mapGenerator.DisplayMap();
    }
}
