using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this class was copied from a tutorial on youtube
public class ETouch : MonoBehaviour {
    public Camera view;
    public LayerMask touchInputMask;
    static List<GameObject> touchList = new List<GameObject>();
    static GameObject[] touchesOld;
    private RaycastHit hit;

    /*private PatternManager ETPatternManager;
    //private TurnDirector ETTurnDirector;

    void Start()
    {
        //ETPatternManager = GameObject.Find("DanceFloor").GetComponent<PatternManager>();
        //ETTurnDirector = GameObject.Find("TurnDirector").GetComponent<TurnDirector>();
    }*/

    void Update ()
    {
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
        yield return null;
    }
}