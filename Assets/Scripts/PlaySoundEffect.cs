// Author:  Shawn Huang Fernandes
// Date:    06/10/22

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JC.Audio2D;

/// <summary>
/// Component that plays a sound effect from 
/// Audio Manager
/// </summary>

public class PlaySoundEffect : MonoBehaviour
{
    public void PlaySound(SoundEffect _sound)
    {
        _sound.Play();
    }
}
