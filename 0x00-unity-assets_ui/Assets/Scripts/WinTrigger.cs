using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class WinTrigger : MonoBehaviour
{
    public TMP_Text finalTime;
    public GameObject timerCanvas;
    public GameObject timer;
    public GameObject finalTimer;
    public Collider winFlagCollider;

    void OnTriggerEnter (Collider other)
    {
        if (other.tag == "Player")
        {
            timer.SetActive(false);
            finalTimer.SetActive(true);
            finalTime.text = timerCanvas.transform.Find("TimerText").GetComponent<TMP_Text>().text;
            winFlagCollider.enabled = false;
        }
    }
}
