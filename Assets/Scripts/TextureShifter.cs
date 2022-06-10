// Author:  Shawn Huang Fernandes
// Date:    06/10/22

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Component that translates the texture on a mesh by a particular speed
/// </summary>

public class TextureShifter : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField]
    [Tooltip("The speed at which the texture translates")]
    private float TranslateSpeed = 0f;

    [SerializeField]
    [Tooltip("The direction in which the texture translates")]
    private Vector3 TranslateDirection = Vector3.zero;

    [SerializeField]
    [Tooltip("The material to apply a translation (offset)")]
    public Material Material;

    private void Update()
    {
        Material.SetVector("OffsetVector", Material.GetVector("OffsetVector") + (Vector4) TranslateDirection.normalized * Time.deltaTime * TranslateSpeed);
    }
}
