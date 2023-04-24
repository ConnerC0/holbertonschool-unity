using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseCanvas;
    private bool pauseCheck = false;
    public AudioMixerSnapshot dampen;
    public AudioMixerSnapshot undampen;

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pauseCheck)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pauseCheck = true;
        pauseCanvas.SetActive(true);
        dampen.TransitionTo(0f);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pauseCheck = false;
        pauseCanvas.SetActive(false);
        undampen.TransitionTo(.1f);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
        undampen.TransitionTo(.1f);
    }

    public void Options()
    {
        SceneManager.LoadScene(4);
        Time.timeScale = 1;
    }
}
