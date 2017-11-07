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
    private void FixedUpdate()
    {
        if (TCTurnDirector.GameStep == TurnDirector.GameState.D_Repeat || TCTurnDirector.GameStep == TurnDirector.GameState.E_EnterNew)
        {
            Touching();
        }
    }

    public void Touching()
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
                                TCPatternManager.TouchDown(target);
                                print("added touch");
                                TCTurnDirector.GameStep = TurnDirector.GameState.G_EndTurn;
                                break;
                            case TurnDirector.GameState.D_Repeat:
                                print("repeating touches (unfinished).");
                                TCPatternManager.TouchDown(target);
                                TCTurnDirector.GameStep = TurnDirector.GameState.G_EndTurn;
                                break;
                        }
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
}