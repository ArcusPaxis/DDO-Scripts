using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnDirector : MonoBehaviour {
    public bool startTurn, endTurn; //used to manually start and end turns
    bool repeatLastTouches, enterNewTouch; //used to check the stage of the turn.
    bool[] justOnce = new bool[5]; //used to make certain events only happen once during the turn

    public int activePlayer; //0=nobody, 1=player1, 2=player2.
    private int turn; //used to check the turn number and to assign the right player.

    //Declaring access to other scripts
    private PatternManager TDPatternManager;
    private ETouch TDTouchControls;
    private TimeManager TDTimeManager;

    void Start ()
    {
        activePlayer = 0;
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

    IEnumerator Turn()  //This is were the main gameplay happens
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


        if (Input.touchCount != 0)
        {
            TDTouchControls.StartCoroutine(TDTouchControls.Touching()); //Starts TouchControls assynchronously (parallel)
        }
        else
        {
            TDTouchControls.StopCoroutine(TDTouchControls.Touching());
        }

        /*if (touchNumber = mainPattern.Count)
        {
            //Repeat Last Steps
            //yield return StartCoroutine(RepeatLastTouches()); //Starts and waits for this routine to end
        }*/
        //else
        {
            //Enter new Touch
            yield return StartCoroutine(EnterNewTouch()); //Starts this coroutine and waits for it to end
        }

        if (endTurn)
        {
            yield return StartCoroutine(EndTurn()); //Now: turns just end. Future: connect to a new turn.
        }
    }

    IEnumerator EndTurn()
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

    IEnumerator RepeatLastTouches()
    {
        print("RepeatLastTouches started");

        
        if (TDPatternManager.mainPattern.Count != 0)
        {
            int touchNr = 0;
            for (; touchNr <= TDPatternManager.mainPattern.Count;)
            {

            }
                    //TDPatternManager.addTouch(TDPatternManager.gameObject.name, TDPatternManager.me);
        }
        print("RepeatLastTouches ended");
        yield return null;
    }

    IEnumerator EnterNewTouch()
    {
        print("EnterNewTouch started");
        if (Input.touchCount == 1)
        {
            
            //TDPatternManager.addTouch(TDPatternManager.gameObject.name, TDPatternManager.me);
            print("EnterNewTouch ended");
            yield return null;
        }
    }
}
