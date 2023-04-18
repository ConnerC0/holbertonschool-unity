using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneController : MonoBehaviour
{
    public GameObject player;
    public GameObject mainCamera;
    public GameObject timerCanvas;
    public Animator cutsceneCamera;

    public void Update()
    {
        if (cutsceneCamera.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            Debug.Log($"Animation over");
            mainCamera.SetActive(true);
            player.GetComponent<PlayerController>().enabled = true;
            timerCanvas.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
