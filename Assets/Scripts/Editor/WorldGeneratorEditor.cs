using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WorldGenerator))]
public class WorldGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        serializedObject.Update();

        WorldGenerator modelSwitcher = (WorldGenerator)target;

        if (GUILayout.Button("Generate"))
        {
            modelSwitcher.GenerateWorld();
        }

        serializedObject.ApplyModifiedProperties();
    }
}
