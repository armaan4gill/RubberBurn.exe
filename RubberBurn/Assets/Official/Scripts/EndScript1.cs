using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



/// <summary>
/// Quits the game
/// </summary>
public class NewBehaviourScript : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
    }


    /// <summary>
    /// Changes the current scene to the scene with matching index
    /// </summary>
    /// <param name="sceneIndex">The index of the scene to switch to</param>
    public void SwitchScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
