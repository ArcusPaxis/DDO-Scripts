using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PatternManager : MonoBehaviour {

    //declaring the TimeManager script.
    public GameObject timer;
    public TimeManager TMPatternManager;
    public TextUpdater TUPatternManager;

    //The following list will hold the touches made by the players
    public List<PlayerTouch> mainPattern;

    //declaring a temporary Player
    public Player me;

    void Start()
    {
        //instantiating a TimeManager object as tM to access to getTimeStamp()
        TMPatternManager = timer.GetComponent<TimeManager>();
        TUPatternManager = this.GetComponent<TextUpdater>();

        //initializing the mainPattern list
        mainPattern = new List<PlayerTouch>();

        //instantiating the temporary player
        me = new Player(1, "André", 2);
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

    //Just saving place
    public void followPattern() { }

    public void addTouch(string colorTouched,Player player = null)
    {
        PlayerTouch lastTouch = new PlayerTouch(me, TMPatternManager.GetTimeStamp(), colorTouched);
        mainPattern.Add(lastTouch);
        printMainPattern("last");
    }
    public void printMainPattern( string showMe = "all")
    {
        if (showMe.Contains("first"))
        {
            Debug.LogFormat
               ("Player {0} touched color {1} at {2} seconds",
               mainPattern[0].dude.Name,
               mainPattern[0].ColorTouched,
               mainPattern[0].InstantTouched);
        }
        if (showMe.Contains("all"))
        {
            for (int i = 0; i < mainPattern.Count; i++)
            {
                Debug.LogFormat("Player {0} touched color {1} at {2} seconds", mainPattern[i].dude.Name, mainPattern[i].ColorTouched, mainPattern[i].InstantTouched);
            }
        }
        if (showMe.Contains("last"))
        {
            Debug.LogFormat
                ("Player {0} touched color {1} at {2} seconds",
                mainPattern[mainPattern.Count-1].dude.Name,
                mainPattern[mainPattern.Count-1].ColorTouched,
                mainPattern[mainPattern.Count-1].InstantTouched);
        }
    }
    public void printMainPattern(int index)
    {
        Debug.LogFormat
                ("Player {0} touched color {1} at {2} seconds",
                mainPattern[index].dude.Name,
                mainPattern[index].ColorTouched,
                mainPattern[index].InstantTouched);
    }
    public string stringedMainPattern(int index)
    {
        return string.Concat("Player {0} touched color {1} at {2} seconds",
               mainPattern[index].dude.Name,
               mainPattern[index].ColorTouched,
               mainPattern[index].InstantTouched);
               
               
               
    }
}
