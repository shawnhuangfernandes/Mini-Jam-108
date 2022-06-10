// Author:  Joseph Crump
// Date:    06/10/22

using UnityEngine;
using UnityEditor;

/// <summary>
/// Custom property drawer for a <see cref="Stat"/> that removes the annoying
/// foldout quality in the inspector.
/// </summary>
[CustomPropertyDrawer(typeof(Stat))]
public class StatPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUILayout.BeginVertical();

        var baseValue = property.FindPropertyRelative("_baseValue");
        EditorGUILayout.PropertyField(baseValue, label);

        EditorGUILayout.EndVertical();
    }
}
