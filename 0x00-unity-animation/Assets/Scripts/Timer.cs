using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    TimeSpan timePlaying;
    public float timeStart;
    public TMP_Text timeText;

    void Start()
    {
        string StartTime = "00:00.00";
        timeText.text = StartTime;
    }

    void Update()
    {
        timeStart += Time.deltaTime;
        timePlaying = TimeSpan.FromSeconds(timeStart);
        string timerStr = timePlaying.ToString("mm':'ss'.'ff");
        timeText.text = timerStr;
    }

    public void Win()
    {
        //See the WinTrigger script instead.
    }
}
