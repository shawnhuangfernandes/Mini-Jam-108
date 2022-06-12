// Author:  Shawn Huang Fernandes
// Date:    06/10/22

using UnityEngine;

/// <summary>
/// Component that translates the texture on a mesh by a particular speed.
/// </summary>
public class TextureShifter : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField]
    [Tooltip("The speed at which the texture translates.")] 
    private Stat translateSpeed;

    [Tooltip("The modifier to speed/slow the scroll speed")]
    [SerializeField]
    private float scrollModifier = 0.5F;

    [SerializeField]
    [Tooltip("The speed at which the texture translates.")]
    private float defaultScrollSpeed = 0F;

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
        offsetVector += (Vector4)translateDirection.normalized * (translateSpeed * scrollModifier + defaultScrollSpeed) * Time.deltaTime;
    }

    public void SetSpeed(Stat _playerStat)
    {
        translateSpeed = _playerStat;
    }
}
