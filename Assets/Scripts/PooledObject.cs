using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject : MonoBehaviour
{
    public ObjectPool Pool;

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        Pool.SetAvailable(this);
    }
}
