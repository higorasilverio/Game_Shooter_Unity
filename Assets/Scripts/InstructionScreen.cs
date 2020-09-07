using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionScreen : MonoBehaviour
{
    /// <summary>
    /// The Instruction Screen is used just to give explanation in game.
    /// The only action made at this point is the loading of the Initial Screen, which is performed on LoadScene below
    /// </summary>
    /// <param name="sceneName"> Initial Scene call</param>
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
