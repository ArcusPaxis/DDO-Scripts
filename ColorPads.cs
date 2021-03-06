﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ColorPads : MonoBehaviour {

    public Color defaultColor, fancyColor;
    private Renderer shad;

    //declaring the access to PatternManager and TextUpdater
    private PatternManager CPPatternManager;
    private TextManager CPTextManager;
    TurnDirector CPTurnDirector;

    void Start()
    { 
        shad = GetComponent<Renderer>();

        CPPatternManager = GameObject.Find("DanceFloor").GetComponent<PatternManager>();
        CPTextManager = GameObject.Find("Canvas").GetComponent<TextManager>();
        CPTurnDirector = GameObject.Find("TurnDirector").GetComponent<TurnDirector>();
    }

    public void TouchDown()
    {
        if (shad.material.color != fancyColor)
        { ColorChange(); }
    }
    void TouchEnded() { ColorReset(); }
    void TouchStopped() { ColorReset(); }
    void TouchStay()
    {
        if (shad.material.color != fancyColor)
        { ColorChange(); }
    }

    void ColorChange()
    { shad.material.color = fancyColor; }

    void ColorReset()
    { shad.material.color = defaultColor; }

    /*    float ColorLoop(float t) //old blinking function
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
    }*/
}

