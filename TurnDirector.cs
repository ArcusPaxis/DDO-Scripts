using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnDirector : MonoBehaviour {

    public int activePlayer; //0=nobody, 1=player1, 2=player2.
    private int turn;

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

        StartCoroutine(Turn());
    }
	

	void Update ()
    {
        
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
        //yield return StartCoroutine(RepeatLastTouches()); //Starts and waits for this routine to end

        //Enter new Touch
        yield return StartCoroutine(EnterNewTouch()); //Starts this coroutine and waits for it to end

        //End Turn
        StopCoroutine(TouchControls()); // Stops player being able to input touches.
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
                        //print(target.gameObject.name);
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
            TDPatternManager.addTouch(TDPatternManager.gameObject.name, TDPatternManager.me);
            print("EnterNewTouch ended");
            yield return null;
        }
    }
}
