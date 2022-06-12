// Author:  Joseph Crump
// Date:    06/12/22

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Component that can be used by buttons to perform scene changes.
/// </summary>
public class SceneLoader : MonoBehaviour
{
    /// <summary>
    /// The current active scene.
    /// </summary>
    public Scene ActiveScene => SceneManager.GetActiveScene();

    /// <summary>
    /// Reload the current scene.
    /// </summary>
    public void ReloadScene()
    {
        SceneManager.LoadScene(ActiveScene.buildIndex);
    }

    /// <summary>
    /// Close the application.
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }
}
