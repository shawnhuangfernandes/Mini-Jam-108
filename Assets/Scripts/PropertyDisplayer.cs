// Author:  Shawn Huang Fernandes
// Date:    06/10/22

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Updates text mesh pro text to match the value on
/// a player property.
/// </summary>

[RequireComponent(typeof (TextMeshProUGUI))]
public class PropertyDisplayer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI ValueDisplay;

    [SerializeField]
    private PlayerProperty Property;

    [SerializeField]
    [Tooltip("Text to show before the property value.")]
    private string prefix = string.Empty;

    [SerializeField]
    [Tooltip("Text to show after the property value.")]
    private string suffix = string.Empty;

    private void OnValidate()
    {
        ValueDisplay ??= GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        Property.Changed += UpdateDisplay;
        UpdateDisplay(Property.Value);
    }

    private void OnDisable()
    {
        Property.Changed -= UpdateDisplay;
    }

    public void UpdateDisplay(float _value)
    {
        ValueDisplay.text = $"{prefix}{Mathf.FloorToInt(_value)}{suffix}";
    }
}
