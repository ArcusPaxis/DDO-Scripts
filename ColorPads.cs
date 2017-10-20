using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ColorPads : MonoBehaviour {

    public Color defaultColor;
    public Color fancyColor;

    private Renderer shad;
    public float colorSpeed = 60f;
    private float lerp;

    //declaring access to TimeManager
    //public GameObject timerText;
    //private TimeManager tM;

    //declaring the access to PatternManager
    public GameObject patternGO;
    private PatternManager PMColorPads;

    void Start()
    { 
        shad = GetComponent<Renderer>();
        lerp = Time.deltaTime * colorSpeed;

        //timerText = new GameObject();
        //tM = GameObject.Find("TimerText").GetComponent<TimeManager>();
        patternGO = new GameObject();
        PMColorPads = GameObject.Find("DanceFloor").GetComponent<PatternManager>();
    }

    void TouchDown()
    {
        PMColorPads.addTouch(this.gameObject.name, PMColorPads.me);
    }
    void TouchEnded()
    {
        StartCoroutine(ColorReset());
        //PMColorPads.TUPatternManager.UpdateText();
    }
    void TouchStay()
    {
        StartCoroutine(ColorLoop(lerp));
    }
    void TouchStopped()
    {
        StartCoroutine(ColorReset());
    }

    IEnumerator ColorLoop(float t)
    {

        if (shad.material.color != fancyColor)
        {
            shad.material.color = Color.Lerp(defaultColor, fancyColor, t);
            yield return lerp += 0.2f;
        }
        else
        {
            shad.material.color = defaultColor;
            yield return lerp = 0f;
        }
    }
    IEnumerator ColorReset()
    {
        shad.material.color = defaultColor;
        yield return lerp = 0f;
    }
}

