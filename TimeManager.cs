using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    static float activeTurn;
    public float ActiveTurn { get { return activeTurn; } set { activeTurn = value; } }

    TextManager TMTextManager;
    TurnDirector TMTurnDirector;
    private void Start()
    {
        TMTextManager = GameObject.Find("Canvas").GetComponent<TextManager>();
        TMTurnDirector = GameObject.Find("TurnDirector").GetComponent<TurnDirector>();
    }

    private void FixedUpdate()
    {
        GameTime();
    }

    public void GameTime()
    {
        if (TMTurnDirector.GameStep == TurnDirector.GameState.C_TurnStarted ||
            TMTurnDirector.GameStep == TurnDirector.GameState.D_Repeat ||
            TMTurnDirector.GameStep == TurnDirector.GameState.E_EnterNew)
        {
            activeTurn += Time.deltaTime;
            TMTextManager.UpdateTimerText(GetTimeStamp());
        }
        else if (TMTurnDirector.GameStep == TurnDirector.GameState.G_EndTurn ||
            TMTurnDirector.GameStep == TurnDirector.GameState.F_GameOver)
        {
            activeTurn = 0f;
        }
    }

    public float GetTimeStamp() //this function is called when a touch is made
    { return activeTurn; }

    private class Rhythm //Lets try to keep this class here instead of having a new script
    {
        private float beat;
        private int bpm;
        public void setBeat()
        {
            
        }
    }
}
