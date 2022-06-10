// Author:  Joseph Crump
// Date:    06/10/22

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Object that can be used to run coroutines globally.
/// </summary>
public class CoroutineRunner : MonoBehaviour
{
    private static CoroutineRunner _instance;
    private static CoroutineRunner instance
    {
        get
        {
            if (_instance == null)
                _instance = FindOrCreateInstance();

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null)
            Destroy(gameObject);

        if (_instance == null)
            _instance = this;
    }

    /// <summary>
    /// Starts a coroutine named methodName.
    /// </summary>
    public static Coroutine RunCoroutine(string methodName) 
        => instance.StartCoroutine(methodName);

    /// <summary>
    /// Starts a coroutine named methodName.
    /// </summary>
    public static Coroutine RunCoroutine(string methodName, object value) 
        => instance.StartCoroutine(methodName, value);

    /// <summary>
    /// Starts a coroutine.
    /// </summary>
    public static Coroutine RunCoroutine(IEnumerator routine) 
        => instance.StartCoroutine(routine);

    private static CoroutineRunner FindOrCreateInstance()
    {
        var instance = FindInstance();

        instance ??= CreateInstance();

        return instance;
    }

    private static CoroutineRunner FindInstance()
    {
        var runners = FindObjectsOfType<CoroutineRunner>();
        CoroutineRunner instance = null;

        if (runners.Length >= 1)
            instance = runners[0];

        if (runners.Length > 1)
            Debug.LogWarning($"There are {runners.Length} instances of the" +
                $" singleton {nameof(CoroutineRunner)}");

        return instance;
    }

    private static CoroutineRunner CreateInstance()
    {
        var obj = Instantiate(new GameObject(nameof(CoroutineRunner)));
        var coroutineRunner = obj.AddComponent<CoroutineRunner>();

        return coroutineRunner;
    }
}
