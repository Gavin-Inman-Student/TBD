using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    // Adds a load scene function to buttons

    public void LoadGameScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    
}
