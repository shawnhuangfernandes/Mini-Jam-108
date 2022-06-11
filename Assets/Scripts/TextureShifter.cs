// Author:  Shawn Huang Fernandes
// Date:    06/10/22

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Component that translates the texture on a mesh by a particular speed.
/// </summary>
public class TextureShifter : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField]
    [Tooltip("The speed at which the texture translates.")]
    private float translateSpeed = 0f;

    [SerializeField]
    [Tooltip("The direction in which the texture translates.")]
    private Vector3 translateDirection = Vector3.zero;

    [Header("References")]
    [SerializeField]
    [Tooltip("The affected renderer.")]
    private new MeshRenderer renderer;

    private Material material => renderer.sharedMaterial;

    private Vector4 offsetVector
    {
        get => material.GetVector("OffsetVector");
        set => material.SetVector("OffsetVector", value);
    }

    private void Update()
    {
        offsetVector += (Vector4)translateDirection.normalized * translateSpeed * Time.deltaTime;
    }
}
