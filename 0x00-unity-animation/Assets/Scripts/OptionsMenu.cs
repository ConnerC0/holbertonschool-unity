using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    public GameObject sceneTracker;
    static int sceneHistory;

    void Start(){
        SceneHistory();
    }

    public void Back()
    {
        SceneManager.LoadScene(sceneHistory);
    }

    public void SceneHistory()
    {
        DontDestroyOnLoad(sceneTracker);
        if (SceneManager.GetActiveScene().buildIndex != sceneHistory)
        {
            if (SceneManager.GetActiveScene().buildIndex != 4)
            {
                sceneHistory = SceneManager.GetActiveScene().buildIndex;
                print(sceneHistory);
            }
        }
    }
}
