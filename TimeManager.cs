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

    void Start()
    {
        
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
    }

    public float GetTimeStamp()
    {
        return elapsedTime;
    }
}