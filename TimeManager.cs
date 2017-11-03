using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    static float absoluteTime, activeTurn, betweenTurns;
    public float ActiveTurn { get { return activeTurn; } set { activeTurn = value; } }

    public IEnumerator GameTime()
    {
        activeTurn += Time.deltaTime;
        yield return activeTurn;
    }

    public float GetTimeStamp() //this function is called when a touch is made
    {
        return activeTurn;
    }

    private class Rhythm //Lets try to keep this class here instead of having a new script
    {
        private float beat;
        private int bpm;
        public void setBeat()
        {
            
        }
    }
}
