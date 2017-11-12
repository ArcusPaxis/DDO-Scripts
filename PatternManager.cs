using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PatternManager : MonoBehaviour {

    //declaring the TimeManager script.
    private TimeManager PMTimeManager;
    private TextManager PMTextManager;
    private TurnDirector PMTurnDirector;
    private TouchControls PMTouchControls;

    //The following lists will hold the touches made by the players
    public List<PlayerTouch> mainPattern;
    public List<PlayerTouch> repeatPattern;

    //declaring a temporary Player
    public Player me;

    void Start()
    {
        //instantiating a TimeManager object as tM to access to getTimeStamp()
        PMTimeManager = GameObject.Find("Canvas").GetComponent<TimeManager>();
        PMTextManager = GameObject.Find("Canvas").GetComponent<TextManager>();
        PMTurnDirector = GameObject.Find("TurnDirector").GetComponent<TurnDirector>();
        PMTouchControls = GameObject.Find("TouchControls").GetComponent<TouchControls>();

        //initializing the touches lists
        mainPattern = new List<PlayerTouch>();
        repeatPattern = new List<PlayerTouch>();

        //instantiating the temporary player
        me = new Player(1, "Andre`", 2);
    }
    
    //The playerTouch class stores touches made by the players and
    //will make up the List<> mainPattern.
    public class PlayerTouch
    {
        public Player dude;
        public float InstantTouched;
        public string ColorTouched;

        public PlayerTouch(Player guy, float iT, string cT)
        {
            dude = guy;
            InstantTouched = iT;
            ColorTouched = cT;
        }
    }

    public class Player
    {
        public int Id;
        public string Name;
        public int ColorCode;

        public Player(int id, string name, int colorCode)
        {
            Id = id;
            Name = name;
            ColorCode = colorCode;
        }
    }
    /*
    public void addTouch(int touchNr, string colorTouched, Player player = null)
    {
        PlayerTouch lastTouch = new PlayerTouch(me, PMTimeManager.GetTimeStamp(), colorTouched);
        mainPattern.Insert(touchNr, lastTouch);
        PMTextManager.PrintToPlayer(stringedMainPattern(0, true));
        printMainPattern("last");
        PMTurnDirector.GameStep = TurnDirector.GameState.G_EndTurn;
    }

    public void compareTouch(int touchNr, string colorTouched, Player player = null)
    {
        PlayerTouch rTouch = new PlayerTouch(me, PMTimeManager.GetTimeStamp(), colorTouched);
        repeatPattern.Insert(touchNr, rTouch);
        //repeatPattern.Add(rTouch);
        if (repeatPattern[touchNr].ColorTouched == mainPattern[touchNr].ColorTouched &&
            repeatPattern[touchNr].InstantTouched + PMTurnDirector.difficulty >= mainPattern[touchNr].InstantTouched &&
            repeatPattern[touchNr].InstantTouched - PMTurnDirector.difficulty <= mainPattern[touchNr].InstantTouched)
        {
            PMTextManager.PrintToPlayer("Great!");
            touchNr++;
        }
        else
        {
            PMTextManager.PrintToPlayer("Fail!");
            PMTurnDirector.GameStep = TurnDirector.GameState.F_GameOver;
        } 
    }
    */
        public void TouchDown(int touchNr, string colorTouched, Player player = null)
        {
            if (PMTurnDirector.GameStep == TurnDirector.GameState.E_EnterNew)
            {
                PlayerTouch lastTouch = new PlayerTouch(me, PMTimeManager.GetTimeStamp(), colorTouched);
                mainPattern.Add(lastTouch);
                PMTextManager.PrintToPlayer(stringedMainPattern(0, true));
                printMainPattern("last");
                PMTurnDirector.GameStep = TurnDirector.GameState.G_EndTurn;
            }

            if (PMTurnDirector.GameStep == TurnDirector.GameState.D_Repeat)
            {
                PlayerTouch rTouch = new PlayerTouch(me, PMTimeManager.GetTimeStamp(), colorTouched);
                repeatPattern.Add(rTouch);
                if (repeatPattern[touchNr].ColorTouched == mainPattern[touchNr].ColorTouched &&
                    repeatPattern[touchNr].InstantTouched + PMTurnDirector.difficulty >= mainPattern[touchNr].InstantTouched &&
                    repeatPattern[touchNr].InstantTouched - PMTurnDirector.difficulty <= mainPattern[touchNr].InstantTouched)
                {
                    PMTextManager.PrintToPlayer("Great!");
                    print("touchNr++");
                    touchNr++;
                    if (touchNr >= mainPattern.Count)
                    {
                        print("touchNr >= mainPattern.Count");
                        repeatPattern.Clear();
                        PMTurnDirector.GameStep = TurnDirector.GameState.E_EnterNew;
                        return;
                    }
                }
                else
                {
                    PMTextManager.PrintToPlayer("Fail!");
                    PMTurnDirector.GameStep = TurnDirector.GameState.F_GameOver;
                }

            }
        }

    public void printMainPattern( string showMe = "all")
    {
        if (showMe.Contains("first"))
        {
            Debug.LogFormat
               ("Player {0} touched {1} at {2} seconds",
               mainPattern[0].dude.Name,
               mainPattern[0].ColorTouched,
               mainPattern[0].InstantTouched.ToString("0.0"));
        }
        if (showMe.Contains("all"))
        {
            for (int i = 0; i < mainPattern.Count; i++)
            {
                Debug.LogFormat("Player {0} touched {1} at {2} seconds",
                                mainPattern[i].dude.Name,
                                mainPattern[i].ColorTouched,
                                mainPattern[i].InstantTouched.ToString("0.0"));
            }
        }
        if (showMe.Contains("last"))
        {
            Debug.LogFormat
                ("Touch nr {0} , {1} at {2} seconds",
                mainPattern.Count.ToString(),
                mainPattern[mainPattern.Count-1].ColorTouched,
                mainPattern[mainPattern.Count-1].InstantTouched.ToString("0.0"));
        }
    }
    public void printMainPattern(int index)
    {
        Debug.LogFormat
                ("Player {0} touched {1} at {2} seconds",
                mainPattern[index].dude.Name,
                mainPattern[index].ColorTouched,
                mainPattern[index].InstantTouched.ToString("0.0"));
    }//overloaded to accept int for specific index selection
    public string stringedMainPattern(int index = 0,bool justLast = false)
    {
        if (justLast)
        {
            return string.Concat(
                "Player ", mainPattern[mainPattern.Count - 1].dude.Name,
                " touched ", mainPattern[mainPattern.Count - 1].ColorTouched,
                " at ", mainPattern[mainPattern.Count - 1].InstantTouched.ToString("0.0"),
                " seconds.");
        }
        else
        {
            return string.Concat(
                "Player ", mainPattern[index].dude.Name,
                " touched ", mainPattern[index].ColorTouched,
                " at ", mainPattern[index].InstantTouched.ToString("0.0"),
                " seconds.");
        }     
    }
}
