// Author:  Shawn Huang Fernandes
// Date:    06/12/22

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObject : MonoBehaviour
{

    private RockController _player;
    private RockController player
    {
        get
        {
            if (_player == null)
                _player = FindObjectOfType<RockController>();

            return _player;
        }
    }
    private void Update()
    {
        transform.position -= Vector3.forward * player.LateralSpeed * Time.deltaTime;

        if (transform.position.z < player.transform.position.z - 4F)
            gameObject.SetActive(false);
    }
}
