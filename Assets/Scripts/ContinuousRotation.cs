// Author:  Shawn Huang Fernandes
// Date:    06/10/22

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Rotates the object at a steady rate
/// </summary>
/// 
public class ContinuousRotation : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The speed and direction at which to rotate the object")]
    private Vector3 Rotation;

    public void Update()
    {
        transform.Rotate(Rotation * Time.deltaTime);
    }
}
