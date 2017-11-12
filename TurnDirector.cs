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

    AudioClip HumanMusic;

    //Declaring access to other scripts
    private PatternManager TDPatternManager;
    private TouchControls TDTouchControls;
    private TimeManager TDTimeManager;
   

    void Start ()
    {
        activePlayer = 0;
        difficulty = 1f;
        turn = 1;
        GameStep = GameState.B_PreTurn;

        //initializing access
        TDPatternManager = GameObject.Find("DanceFloor").GetComponent<PatternManager>();
        TDTouchControls = GameObject.Find("TouchControls").GetComponent<TouchControls>();
        TDTimeManager = GameObject.Find("Canvas").GetComponent<TimeManager>();
    }
	

	void Update ()
    {
        if (startTurn && GameStep != GameState.E_EnterNew)
        {
            FuckingTurn(GameStep);
        }
    }

    public void FuckingTurn (GameState gs)
    {
        switch (gs)
        {
            case GameState.B_PreTurn: //Setting up the turn
                if (turn % 2 == 1) { activePlayer = 1; }
                else if (turn % 2 == 0) { activePlayer = 2; }
                //AudioSource.PlayClipAtPoint(HumanMusic, TDTouchControls.view.transform.position, 1f);
                GameStep = GameState.C_TurnStarted;
                break;
            case GameState.C_TurnStarted: //Checking if player has steps to repeat or (1st turn) enter new step.
                print("Turn number " + turn);
                if (TDPatternManager.mainPattern.Count != 0)
                {
                    print("RepeatLastSteps.");
                    GameStep = GameState.D_Repeat;
                }
                else
                {
                    print("mainPattern is empty. EnterNewStep.");
                    GameStep = GameState.E_EnterNew;
                }
                break;
            case GameState.D_Repeat:
                if (TDTimeManager.ActiveTurn >= TDPatternManager.mainPattern[TDTouchControls.touchNr].InstantTouched + difficulty)
                {
                    //GameOver();
                }
                break;
            case GameState.F_GameOver:
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
        turn++;
        TDPatternManager.repeatPattern.Clear();
        GameStep = GameState.B_PreTurn;
    }

    void GameOver()
    {
        print("Game Over");
        turn = 1;
        TDTouchControls.touchNr = 0;
        startTurn = false;
        endTurn = false;
        TDPatternManager.mainPattern.Clear();
        TDPatternManager.repeatPattern.Clear();
        GameStep = GameState.B_PreTurn;
    }
}
