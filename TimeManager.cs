using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeManager : MonoBehaviour
{
    private float absoluteTime;
    private float playingTurn;
    private float betweenTurns;
    
    void Start()
    {
        
    }

    void Update()
    {
        absoluteTime += Time.deltaTime;
    }

    public float GetTimeStamp()
    {
        return absoluteTime;
    }
    private class RhythmManager //Lets try to keep this class here instead of having a new script
    {
        private float beat;
        private int bpm;
        public void Beat()
        {
    
        }
    }
}
