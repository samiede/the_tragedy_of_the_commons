using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GridGenerator))]
public class MapEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GridGenerator map = target as GridGenerator;

        if (GUILayout.Button("Generate Map"))
        {
            map.GenerateMap();
        }
    }
}