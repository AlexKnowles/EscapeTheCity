using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(WorldTileTypeModel))]
public class WorldTileTypeModelDrawer : PropertyDrawer
{
    private const int gridSize = 3;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        SerializedProperty modelProperty = property.FindPropertyRelative("Model");
        position.height = EditorGUI.GetPropertyHeight(modelProperty);

        Rect gridRect = EditorGUI.PrefixLabel(position, label);
        gridRect.y += position.height + EditorGUIUtility.standardVerticalSpacing;

        EditorGUIUtility.labelWidth = 60f;

        float gridItemWidth = gridRect.width / gridSize;
        float gridItemHeight = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

        Rect tileRect = new Rect(gridRect.x, gridRect.y, gridItemWidth, gridItemHeight);

        DrawTileField(tileRect, property.FindPropertyRelative("NorthWestComparitor"));
        tileRect.x += gridItemWidth;

        DrawTileField(tileRect, property.FindPropertyRelative("NorthComparitor"));
        tileRect.x += gridItemWidth;

        DrawTileField(tileRect, property.FindPropertyRelative("NorthEastComparitor"));
        tileRect.y += gridItemHeight;
        tileRect.x = gridRect.x;

        DrawTileField(tileRect, property.FindPropertyRelative("WestComparitor"));
        tileRect.x += gridItemWidth;

        DrawTileField(tileRect, property.FindPropertyRelative("Model"));
        tileRect.x += gridItemWidth;

        DrawTileField(tileRect, property.FindPropertyRelative("EastComparitor"));
        tileRect.y += gridItemHeight;
        tileRect.x = gridRect.x;

        DrawTileField(tileRect, property.FindPropertyRelative("SouthWestComparitor"));
        tileRect.x += gridItemWidth;

        DrawTileField(tileRect, property.FindPropertyRelative("SouthComparitor"));
        tileRect.x += gridItemWidth;

        DrawTileField(tileRect, property.FindPropertyRelative("SouthEastComparitor"));
        tileRect.y += gridItemHeight;
        tileRect.x = gridRect.x;

        EditorGUILayout.Space();

        SerializedProperty rotationProperty = property.FindPropertyRelative("ModelRotation");
        Rect sliderRect = EditorGUILayout.GetControlRect();
        rotationProperty.intValue = EditorGUI.IntSlider(sliderRect, "Rotation", rotationProperty.intValue, 0, 3);

        EditorGUI.EndProperty();
    }

    private void DrawTileField(Rect position, SerializedProperty property)
    {
        EditorGUI.PropertyField(position, property, GUIContent.none);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float modelHeight = EditorGUI.GetPropertyHeight(property.FindPropertyRelative("Model"));
        float gridHeight = EditorGUIUtility.singleLineHeight * 4 + EditorGUIUtility.standardVerticalSpacing;
        return modelHeight + gridHeight;
    }
}
