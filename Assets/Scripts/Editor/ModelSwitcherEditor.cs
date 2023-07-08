using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ModelSwitcher))]
public class ModelSwitcherEditor : Editor
{    private int _previousSelectedIndex = -1;
    private int _selectedIndex = -1;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        serializedObject.Update();

        ModelSwitcher modelSwitcher = (ModelSwitcher)target;

        AddSelectionList(modelSwitcher);
        AddRotationButtons(modelSwitcher);

        serializedObject.ApplyModifiedProperties();
    }
    private void AddSelectionList(ModelSwitcher modelSwitcher)
    {
        if (modelSwitcher.CurrentModelInWorld is null)
        {
            modelSwitcher.LoadModel(0);
            _selectedIndex = 0;
        }
        else if (_selectedIndex == -1)
        {
            _selectedIndex = modelSwitcher.models.ToList().IndexOf(modelSwitcher.CurrentModelInWorld);
        }

        _selectedIndex = EditorGUILayout.Popup("Model", _selectedIndex, modelSwitcher.models.Select(s => s.name).ToArray());

        if (_previousSelectedIndex != _selectedIndex)
        {
            modelSwitcher.LoadModel(_selectedIndex);
            _previousSelectedIndex = _selectedIndex;
        }
    }

    private void AddRotationButtons(ModelSwitcher modelSwitcher) 
    {
        EditorGUILayout.BeginHorizontal();

        // Display a button to rotate the model clockwise
        if (GUILayout.Button("Rotate Clockwise"))
        {
            modelSwitcher.RotateClockwise();
        }

        // Display a button to rotate the model counter-clockwise
        if (GUILayout.Button("Rotate Counter-Clockwise"))
        {
            modelSwitcher.RotateCounterClockwise();
        }

        EditorGUILayout.EndHorizontal();
    }
}
