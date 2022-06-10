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
        var baseValue = property.FindPropertyRelative("_baseValue");
        EditorGUI.PropertyField(position, baseValue, label);
    }
}
