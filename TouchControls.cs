using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchControls : MonoBehaviour {
    public Camera view;
    public LayerMask touchInputMask;
    static List<GameObject> touchList = new List<GameObject>();
    static GameObject[] touchesOld;
    private RaycastHit hit;

    public int touchNr;
    private enum RepeatingTouches { Checking, WindowOpen, Success, Failure}
    private RepeatingTouches RLT = RepeatingTouches.Checking;
    
    //declaring access to other scripts
    private TimeManager TCTimeManager;
    private PatternManager TCPatternManager;
    private TurnDirector TCTurnDirector;

    void Start()
    {
        touchNr = 0;

        //declaring access to other scripts
        TCTimeManager = GameObject.Find("Canvas").GetComponent<TimeManager>();
        TCPatternManager = GameObject.Find("DanceFloor").GetComponent<PatternManager>();
        TCTurnDirector = GameObject.Find("TurnDirector").GetComponent<TurnDirector>();
    }

    public IEnumerator Touching()
    {
        print("Touching coroutine started");
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
                        switch (TCTurnDirector.GameStep) {
                            case TurnDirector.GameState.E_EnterNew:
                                TCPatternManager.addTouch(target.name);
                                print("added touch");
                                TCTurnDirector.GameStep = TurnDirector.GameState.G_EndTurn;
                                yield return null;
                                break;
                            case TurnDirector.GameState.D_Repeat:
                                print("repeating touches");
                                break;
                        }
                        target.SendMessage("TouchDown", hit.point, SendMessageOptions.DontRequireReceiver);
                    }
                    if (touch.phase == TouchPhase.Ended)
                    {
                        target.SendMessage("TouchEnded", hit.point, SendMessageOptions.DontRequireReceiver);
                    }
                    if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
                    {
                        target.SendMessage("TouchStay", hit.point, SendMessageOptions.DontRequireReceiver);
                    }
                    if (touch.phase == TouchPhase.Canceled)
                    {
                        target.SendMessage("TouchStopped", hit.point, SendMessageOptions.DontRequireReceiver);
                    }
                }
            }
            foreach (GameObject g in touchesOld)
            {
                if (!touchList.Contains(g))
                {
                    g.SendMessage("TouchStopped", hit.point, SendMessageOptions.DontRequireReceiver);
                }
            }
        }
    }

    public IEnumerator RepeatLastTouches()
    {
        print("RepeatLastTouches started");
        if (TCPatternManager.mainPattern.Count != 0)
        {
            //The next if statement is a rudimentary pseudo-code to get started
            if ((TCTimeManager.ActiveTurn + TCTurnDirector.difficulty) <= TCPatternManager.mainPattern[touchNr].InstantTouched)//    TurnTimer -0.05 <= mainPattern[touchNr].time)
            {
                //reset player touchrecentTouch.;
                if ((TCTimeManager.ActiveTurn - TCTurnDirector.difficulty) >= TCPatternManager.mainPattern[touchNr].InstantTouched)
                {
                    //Add reset recentTouch
                    RLT = RepeatingTouches.WindowOpen;
                }
            }
            else
            {
                RLT = RepeatingTouches.Checking;
            }
            /*if (windowOpen)
            {
                if (recentTouch.color == ETPatternManager.mainPattern[touchNr].ColorTouched)
                {
                    success = true;
                }
            }
            else if (!windowOpen && !recentTouch.isEmpty) //or recentTouch.time != 0
            {
                success = false;
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
        {   //since the mainPattern is empty, jump to coroutine EnterNewTouch
            //yield return StartCoroutine(EnterNewTouch());
        }
        print("RepeatLastTouches ended");
        //create a success || failure path
        switch (RLT)
        {
            case RepeatingTouches.Success:
                if (touchNr == TCPatternManager.mainPattern.Count)
                {
                    print("Success");
                    TCTurnDirector.GameStep = TurnDirector.GameState.E_EnterNew;
                    yield return null;
                }
                else
                { RLT = RepeatingTouches.Checking; }
                break;
            case RepeatingTouches.Failure:
                print("Failed");
                TCTurnDirector.GameStep = TurnDirector.GameState.F_GameOver;
                break;
        }
    }

/*    public IEnumerator EnterNewTouch()
    {
        print("EnterNewTouch started");
        if (Input.touchCount == 1)
        {
            print("EnterNewTouch ended");
            TCTurnDirector.GameStep = TurnDirector.GameState.G_EndTurn;
            yield return null;
        }
    }*/
}