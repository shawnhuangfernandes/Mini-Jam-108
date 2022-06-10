// Author:  Joseph Crump
// Date:    06/10/22

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Custom physics and input handler for the skipping rock.
/// </summary>
public class RockController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("How rapidly the object accelerates towards the ground.")]
    private Stat gravity = -9.8f;
}
