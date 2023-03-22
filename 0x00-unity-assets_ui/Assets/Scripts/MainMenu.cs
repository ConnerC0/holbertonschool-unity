using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LevelSelect(int level) // level variable == scene in array
    {
        SceneManager.LoadScene(level);
    }

    public void Exit()
    {
        Debug.Log("Exited");
        Application.Quit();
    }
}
