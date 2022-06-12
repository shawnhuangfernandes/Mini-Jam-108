// Author:  Shawn Huang Fernandes
// Date:    06/10/22

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Component that handles visuals of rock skipping
/// This includes the water movement, spawning VFX
/// </summary>

[CreateAssetMenu(fileName = "PlayerStats", menuName = "PlayerStats")]
public class PlayerProperty : ScriptableObject
{
    private static PlayerProperty _points;
    public static PlayerProperty Points
    {
        get
        {
            if (_points == null)
                _points = Resources.Load<PlayerProperty>("Stats/Points");

            return _points;
        }
    }

    private static PlayerProperty _distance;
    public static PlayerProperty Distance
    {
        get
        {
            if (_distance == null)
                _distance = Resources.Load<PlayerProperty>("Stats/Distance");

            return _distance;
        }
    }

    private static PlayerProperty _skips;
    public static PlayerProperty Skips
    {
        get
        {
            if (_skips == null)
                _skips = Resources.Load<PlayerProperty>("Stats/Skips");

            return _skips;
        }
    }

    private static PlayerProperty _rounds;
    public static PlayerProperty Rounds
    {
        get
        {
            if (_rounds == null)
                _rounds = Resources.Load<PlayerProperty>("Stats/Rounds");

            return _rounds;
        }
    }

    /// <summary>
    /// The value of the property
    /// </summary>
    public float Value
    {
        get => _Value;
        set
        {
            if (_Value == value)
                return;

            _Value = value;
            Changed?.Invoke(_Value);
        }
    }

    private float _Value;

    public delegate void ValueChangeEvent(float _value);
    public event ValueChangeEvent Changed;

    public void Add(float _value)
    {
        Value += _value;
    }

    public void Subtract(float _value)
    {
        Add(-_value);
    }

    public static implicit operator float(PlayerProperty property)
    {
        return property.Value;
    }
}
