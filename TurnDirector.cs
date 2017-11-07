using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnDirector : MonoBehaviour {
    public bool startTurn, endTurn; //used to manually start and end turns

    public int activePlayer; //0=nobody, 1=player1, 2=player2.
    public float difficulty;
    private int turn; //used to check the turn number and to assign the right player.

    //These are the game steps (states). The initial capital letter helps keeping flow easy to follow
    public enum GameState { A_Null, B_PreTurn, C_TurnStarted, D_Repeat, E_EnterNew, F_GameOver, G_EndTurn };
    public GameState GameStep;

    //Declaring access to other scripts
    private PatternManager TDPatternManager;
    private TouchControls TDTouchControls;
    private TimeManager TDTimeManager;
   

    void Start ()
    {
        activePlayer = 0;
        difficulty = 0.05f;
        turn = 1;
        GameStep = GameState.B_PreTurn;

        //initializing access
        TDPatternManager = GameObject.Find("DanceFloor").GetComponent<PatternManager>();
        TDTouchControls = GameObject.Find("TouchControls").GetComponent<TouchControls>();
        TDTimeManager = GameObject.Find("Canvas").GetComponent<TimeManager>();
    }
	

	void Update ()
    {
        if (startTurn)
        {
            FuckingTurn(GameStep);
        }
    }

    public void FuckingTurn (GameState gs)
    {
        switch (gs)
        {
            case GameState.B_PreTurn: //Setting up the turn
                print("Pre turn started");
                //Making a player active. Using module now to check if player's ID is odd or even.
                //Could use a bool but this makes implementing more players in the future easier.
                if (turn % 2 == 1) { activePlayer = 1; }
                else if (turn % 2 == 0) { activePlayer = 2; }
                GameStep = GameState.C_TurnStarted;
                break;
            case GameState.C_TurnStarted: //Checking if player has steps to repeat or (1st turn) enter new step.
                print("Started turn number " + turn + " and checking next step.");
                if (TDPatternManager.mainPattern.Count != 0)
                {
                    print("mainPattern not empty. RepeatLastSteps.");
                    GameStep = GameState.D_Repeat;
                }
                else
                {
                    print("mainPattern is empty. EnterNewStep.");
                    GameStep = GameState.E_EnterNew;
                }
                break;
            case GameState.D_Repeat: //Repeat Last Steps
                break;
            case GameState.E_EnterNew: //Enter new Touch
                break;
            case GameState.F_GameOver: //GameOver
                GameOver();
                break;
            case GameState.G_EndTurn:
                EndTurn();
                break;
        }
    }
    void EndTurn()
    {
        print("Ended turn number " + turn);
        TDTouchControls.touchNr = 0;
        startTurn = false;
        endTurn = false;
        GameStep = GameState.B_PreTurn;
    }

    void GameOver()
    {
        print("Game Over");
        TDPatternManager.mainPattern.Clear();
        GameStep = GameState.G_EndTurn;
    }

    /*    public void Turn()  //This is were the main gameplay happens
        {
            print("coroutine Turn running");
            switch (GameStep)
            {
                case GameState.B_PreTurn: //Setting up the turn
                    print("Pre turn started");
                    //TDTimeManager.StartCoroutine(TDTimeManager.GameTime());
                    //TDTouchControls.StartCoroutine(TDTouchControls.Touching());

                    //Making a player active. Using module now to check if player's ID is odd or even.
                    //Could use a bool but this makes implementing more players in the future easier.
                    if (turn % 2 == 1) { activePlayer = 1; }
                    else if (turn % 2 == 0) { activePlayer = 2; }
                    GameStep = GameState.C_TurnStarted;
                    break;
                case GameState.C_TurnStarted: //Checking if player has steps to repeat or (1st turn) enter new step.
                    print("Started turn number " + turn + " and checking next step.");
                    if (TDPatternManager.mainPattern.Count != 0)
                    {
                        print("mainPattern not empty. RepeatLastSteps.");
                        GameStep = GameState.D_Repeat;
                    }
                    else
                    {
                        print("mainPattern is empty. EnterNewStep.");
                        GameStep = GameState.E_EnterNew;
                    }
                    break;
                case GameState.D_Repeat: //Repeat Last Steps
                    //yield return TDTouchControls.StartCoroutine(TDTouchControls.RepeatLastTouches()); //Starts and waits for this routine to end
                    break;
                case GameState.E_EnterNew: //Enter new Touch
                    print("EnterNew coroutine started.");
                    TDTouchControls.StartCoroutine(TDTouchControls.EnterNewTouch());
                    break;
                case GameState.F_GameOver: //GameOver
                    yield return StartCoroutine(GameOver());
                    break;
                case GameState.G_EndTurn:
                    yield return StartCoroutine(EndTurn());
                    break;
            }
        }

        public IEnumerator EndTurn()
        {
            print("Ended turn number " + turn);
            TDTouchControls.StopCoroutine(TDTouchControls.Touching()); //Stop Touch Controls
            TDTimeManager.StopCoroutine(TDTimeManager.GameTime()); // Stop Turn Timer
            TDTimeManager.ActiveTurn = 0; //resets the turn timer
            TDTouchControls.touchNr = 0;
            startTurn = false;
            endTurn = false;
            GameStep = GameState.B_PreTurn;
            yield return null;
        }

        public IEnumerator GameOver()
        {
            print("Game Over");
            GameStep = GameState.G_EndTurn;
            yield return null;
        }*/
}
