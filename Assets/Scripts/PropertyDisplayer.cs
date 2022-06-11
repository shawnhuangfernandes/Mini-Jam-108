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

    private void OnValidate()
    {
        ValueDisplay ??= GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        Property.Changed += UpdateDisplay;
    }

    private void OnDisable()
    {
        Property.Changed -= UpdateDisplay;
    }

    public void UpdateDisplay(float _value)
    {
        ValueDisplay.text = $"{Mathf.FloorToInt(_value)}";
    }
}
