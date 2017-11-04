using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnDirector : MonoBehaviour {

    private int activePlayer, turn; //0=nobody, 1=player1, 2=player2.
	
	//let's see if this works
	public Touch recentTouch;

    //Connecting to PatternManager
    private PatternManager TDPatternManager;

    //TouchControls
    public Camera view;
    public LayerMask touchInputMask;
    private List<GameObject> touchList = new List<GameObject>();
    private GameObject[] touchesOld;
    private RaycastHit hit;

    void Start ()
    {
        activePlayer = 0;
        turn = 1;

        touchList = new List<GameObject>();
        TDPatternManager = GameObject.Find("DanceFloor").GetComponent<PatternManager>();
    }

	void Update ()
    {
        StartCoroutine(Turn());
	}

    IEnumerator Turn()  //This is were the main gameplay happens
    {
        //Making a player active. Using module now to check if player's ID is odd or even.
        //Could use a bool but this makes implementing more players in the future easier.
        if (turn%2 == 1)
            activePlayer = 1;
        else if (turn%2 == 0)
            activePlayer = 2;
        Coroutine TouchControl = StartCoroutine(TouchControls()); //Starts TouchControls assynchronously (parallel)

        //Repeat Last Steps
        yield return StartCoroutine(RepeatLastTouches()); //Starts and waits for this routine to end

        //End Turn
        StopCoroutine(TouchControls());
        turn++;
        yield return null; //Now: turns just end. Future: connect to a new turn.
    }

    IEnumerator TouchControls()
    {
        if (Input.touchCount > 0)
        {
            touchesOld = new GameObject[touchList.Count];
            touchList.CopyTo(touchesOld);
            touchList.Clear();
            foreach (Touch touch in Input.touches)
            {
                Ray ray = view.ScreenPointToRay(touch.position);
                if (Physics.Raycast(ray, out hit, touchInputMask))
                {
                    GameObject target = hit.transform.gameObject;
                    touchList.Add(target);
                    if (touch.phase == TouchPhase.Began)
                    {
                        //TouchDown
                    }
                    if (touch.phase == TouchPhase.Ended)
                    {
                        //TouchEnded
                    }
                    if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
                    {
                        //TouchStay
                    }
                    if (touch.phase == TouchPhase.Canceled)
                    {
                        //TouchStopped
                    }
                }
            }
            foreach (GameObject g in touchesOld)
            {
                if (!touchList.Contains(g))
                {
                    //TouchStopped
                }
            }
        }
    yield return null;
    }

    IEnumerator RepeatLastTouches()
    {
		print("RepeatLastTouches started");
		bool success, windowOpen;
		int touchNr;
        if (TDPatternManager.mainPattern.Count != 0) 
        {    //Here we lock the user input while the number of elements in the mainPattern list is greater than the touch number
	    	success = False;
			
			//The next if statement is a rudimentary pseudo-code to get started
			//the 0.05 will be swapped by a "public float difficulty" variable.
			/*if (TurnTimer-0.05 <= mainPattern[touchNr].time)
			{
				recentTouch.clear();
				if(TurnTimer+0.05 >= mainPattern[touchNr].time)
				{
					//reset recentTouch
					window = True;
				}
			}
			else
			{
				window = False;
			}
			if(window)
			{
				if(recentTouch.color == TDPatternManager.mainPattern[touchNr].color)
				{
					success = True;
				}
			}
			else if (!window && !recentTouch.isEmpty ) //or recentTouch.time != 0
			{
				success = False;
			}
			*/
			
//thinking out loud:
//I need to compare any touch made (color and time) during a small window of time with what is in mainPattern
//touchNr could be substituted by a "Touch RecentTouch" class that represents the last touch made by the active player
//when the window opens, reset the RecentTouch, and an active check can be made to compare the next one until the window closes
//if successful, the window closes until the next touch's window opens.
//automatic failure when:
	//a player touches the wrong color when the window is open
	//a player touches out of the window-time
	     
             /*for (int touchNr = 0; touchNr <= TDPatternManager.mainPattern.Count;)
             {
	    
             }*/
        }
	else
	{	//since the mainPattern is empty, jump to coroutine EnterNewTouch
	     yield return StartCoroutine(EnterNewTouch());
	}
        print("RepeatLastTouches ended");
	//create a success || failure path
	/*if(success && touchNr == TDPatternManager.mainPattern.Count)
	{
	     print("Success");
	     yield return StartCoroutine(EnterNewTouch()); 
	}
	else
	{
	     Print("Failed");
	     yield return StartCoroutine(GameOver());
	}*/
    }

    IEnumerator EnterNewTouch() //isolate all other statements adding touches to mainPattern list to check if this works
    {
        print("EnterNewTouch started");
        TDPatternManager.addTouch(TDPatternManager.gameObject.name, TDPatternManager.me);
        print("EnterNewTouch ended");
        yield return null;
    }
}
