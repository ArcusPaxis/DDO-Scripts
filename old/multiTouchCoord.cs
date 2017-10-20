using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class multiTouchCoord : MonoBehaviour {

    List<string> touchInfos = new List<string>();

	void Update ()
    {
        touchInfos.Clear();

        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);
            string tmp = "Touch #" + i + 1 + " at x:" + touch.position.x + ", y:" + touch.position.y + ".";
            touchInfos.Add(tmp);
        }
	}
    void OnGUI()
    {
        foreach(string s in touchInfos)
        {
            GUILayout.Label(s);
        }
    }
}
