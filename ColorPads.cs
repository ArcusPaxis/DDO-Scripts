﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ColorPads : MonoBehaviour {

    public Color defaultColor, fancyColor;
    private Renderer shad;
    public float colorSpeed = 60f;
    private float lerp;

    //declaring the access to PatternManager and TextUpdater
    private PatternManager CPPatternManager;
    private TextManager CPTextManager;
    TurnDirector CPTurnDirector;

    void Start()
    { 
        shad = GetComponent<Renderer>();
        lerp = Time.deltaTime * colorSpeed;

        CPPatternManager = GameObject.Find("DanceFloor").GetComponent<PatternManager>();
        CPTextManager = GameObject.Find("Canvas").GetComponent<TextManager>();
        CPTurnDirector = GameObject.Find("TurnDirector").GetComponent<TurnDirector>();
    }

    void TouchDown()
    {
        if (CPTurnDirector.activePlayer != 0)
        {
            CPPatternManager.addTouch(this.gameObject.name, CPPatternManager.me);
        }
    }
    void TouchEnded()
    {
        ColorReset();
        if (CPTurnDirector.activePlayer != 0)
        {
            CPTextManager.PrintToPlayer(CPPatternManager.stringedMainPattern(0, true));
        }
    }
    void TouchStay()
    {
        ColorLoop(lerp);
    }
    void TouchStopped()
    {
        ColorReset();
    }

    float ColorLoop(float t)
    {
        if (shad.material.color != fancyColor)
        {
            shad.material.color = Color.Lerp(defaultColor, fancyColor, t);
            return lerp += 0.3f;
        }
        else
        {
            shad.material.color = defaultColor;
            return lerp = 0f;
        }
    }
    float ColorReset()
    {
        shad.material.color = defaultColor;
        return lerp = 0f;
    }
}

