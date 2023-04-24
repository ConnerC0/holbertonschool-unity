using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class WinTrigger : MonoBehaviour
{
    public Text finalTime;
    public GameObject timerCanvas;
    public GameObject timer;
    public GameObject winCanvas;
    public GameObject CheeryMonday;
    public Collider winFlagCollider;
    public AudioSource win;

    void OnTriggerEnter (Collider other)
    {
        if (other.tag == "Player")
        {
            timer.SetActive(false);
            winCanvas.SetActive(true);
            CheeryMonday.SetActive(false);
            finalTime.text = timerCanvas.transform.Find("TimerText").GetComponent<TMP_Text>().text;
            winFlagCollider.enabled = false;
            win.Play();
        }
    }
}
