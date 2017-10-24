using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private float absoluteTime;
    private float playingTurn;
    private float betweenTurns;

    void Update()
    {
        absoluteTime += Time.deltaTime;
    }

    public float GetTimeStamp()
    {
        return absoluteTime;
    }
    private class Rhythm //Lets try to keep this class here instead of having a new script
    {
        private float beat;
        private int bpm;
        public void Beat()
        {
            
        }
    }
}
