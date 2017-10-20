using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeManager : MonoBehaviour
{

    public float timerSpeed = 3;
    private float elapsedTime;
    public GameObject TextTimerGO;
    private TextMeshProUGUI textTimer;

    void Start()
    {
        textTimer = TextTimerGO.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        textTimer.text = "Timer: " + elapsedTime.ToString("0.0");
    }
    public float GetTimeStamp()
    {
        return elapsedTime;
    }
}