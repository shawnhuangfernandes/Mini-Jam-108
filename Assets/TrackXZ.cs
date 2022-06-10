// Author:  Shawn Huang Fernandes
// Date:    06/10/22
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Component that tracks the XZ position of another transform & maintains
/// its own Y position.
/// </summary>
public class TrackXZ : MonoBehaviour
{
    [SerializeField] private Transform Tracked;
    Vector3 TrackedPos => Tracked.position;

    private void Update()
    {
        transform.position = new Vector3(TrackedPos.x, transform.position.y, TrackedPos.z);
    }
}
