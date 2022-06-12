// Author:  Shawn Huang Fernandes
// Date:    06/11/22

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

/// <summary>
/// Text that "flies" away on spawning
/// </summary>

    [RequireComponent(typeof (TextMeshPro))]
public class FlyAwayText : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The displacement of the text after it spawns")]
    private Vector3 LocalDisplacement;

    [SerializeField]
    [Tooltip("The duration of the flight")]
    private float DisplacementDuration;

    [SerializeField]
    [Tooltip("The duration of the text FadeIn")]
    private float FadeInDuration;

    private TextMeshPro _tmp;
    private TextMeshPro tmp
    {
        get
        {
            if (_tmp == null)
                _tmp = GetComponent<TextMeshPro>();

            return _tmp;
        }
    }

    public void Start()
    {
        tmp.DOFade(1, FadeInDuration)
            .SetEase(Ease.InQuad);

        transform.DOScale(1, FadeInDuration)
            .SetEase(Ease.InOutBack);

        transform.DOMove(transform.position + LocalDisplacement, DisplacementDuration)
            .SetEase(Ease.InOutBack)
            .OnComplete(() => Destroy(gameObject));
    }
}
