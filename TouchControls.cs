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
    
    //declaring access to other scripts
    private TimeManager TCTimeManager;
    private PatternManager TCPatternManager;
    private TurnDirector TCTurnDirector;
    private ColorPads TCColorPads;

    void Start()
    {
        touchNr = 0;

        //declaring access to other scripts
        TCTimeManager = GameObject.Find("Canvas").GetComponent<TimeManager>();
        TCPatternManager = GameObject.Find("DanceFloor").GetComponent<PatternManager>();
        TCTurnDirector = GameObject.Find("TurnDirector").GetComponent<TurnDirector>();
        TCColorPads = GameObject.Find("DanceFloor").GetComponent<ColorPads>();
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
                        TCPatternManager.TouchDown(touchNr, target.name);
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