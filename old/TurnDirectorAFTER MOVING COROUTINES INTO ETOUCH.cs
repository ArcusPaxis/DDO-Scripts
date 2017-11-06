using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnDirectorAFTER : MonoBehaviour {
    public bool startTurn, endTurn; //used to manually start and end turns
    bool repeatLastTouches, enterNewTouch; //used to check the stage of the turn. Currently not being used
    bool[] justOnce = new bool[5]; //used to make certain events only happen once during the turn

    public int activePlayer; //0=nobody, 1=player1, 2=player2.
    public float difficulty;
    bool success;
    private int turn; //used to check the turn number and to assign the right player.

    //Declaring access to other scripts
    private PatternManager TDPatternManager;
    private ETouch TDTouchControls;
    private TimeManager TDTimeManager;
   

    void Start ()
    {
        activePlayer = 0;
        difficulty = 0.05f;
        turn = 1;


        //making justOnce ready for the first use
        for (int a = 0; a <= justOnce.Length; a++)
        {
            justOnce[a] = true;
        }

        //initializing access
        TDPatternManager = GameObject.Find("DanceFloor").GetComponent<PatternManager>();
        TDTouchControls = GameObject.Find("TouchControls").GetComponent<ETouch>();
        TDTimeManager = GameObject.Find("Canvas").GetComponent<TimeManager>();
    }
	

	void Update ()
    {
        if (startTurn)
        {
            StartCoroutine(Turn());
        }
    }

    public IEnumerator Turn()  //This is were the main gameplay happens
    {
        if (justOnce[0])
        {
            print("Started turn number " + turn);
            TDTimeManager.StartCoroutine(TDTimeManager.GameTime());

            //Making a player active. Using module now to check if player's ID is odd or even.
            //Could use a bool but this makes implementing more players in the future easier.
            if (turn%2 == 1)
                activePlayer = 1;
            else if (turn%2 == 0)
                activePlayer = 2;
            justOnce[0] = false;
        }


        if (Input.touchCount != 0) //I don't know if this works. If it does, it'll save resources
        {
            TDTouchControls.StartCoroutine(TDTouchControls.Touching()); //Starts TouchControls assynchronously (parallel)
        }
        else
        {
            TDTouchControls.StopCoroutine(TDTouchControls.Touching());
        }

        if (TDTouchControls.touchNr == TDPatternManager.mainPattern.Count)
        {   //Repeat Last Steps
            yield return TDTouchControls.StartCoroutine(TDTouchControls.RepeatLastTouches()); //Starts and waits for this routine to end
        }
        else
        {   //Enter new Touch
            yield return TDTouchControls.StartCoroutine(TDTouchControls.EnterNewTouch()); //Starts this coroutine and waits for it to end
        }

        if (endTurn)
        {   //Ends the turn
            yield return StartCoroutine(EndTurn()); //Now: turns just end. Future: connect to a new turn.
        }
    }

    public IEnumerator EndTurn()
    {
        print("Ended turn number " + turn);
        TDTouchControls.StopCoroutine(TDTouchControls.Touching()); //Stop Touch Controls
        TDTimeManager.StopCoroutine(TDTimeManager.GameTime()); // Stop Turn Timer
        TDTimeManager.ActiveTurn = 0; //resets the turn timer
        for (int a = 0; a <= justOnce.Length; a++)
        {
            justOnce[a] = true;
        }
        startTurn = false;
        endTurn = false;
        yield return null;
    }

    public IEnumerator GameOver()
    {
        print("Game Over");
        yield return null;
    }
}
