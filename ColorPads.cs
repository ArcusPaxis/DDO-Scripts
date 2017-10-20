using System.Collections;
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
    private TextUpdater CPTextUpdater;

    void Start()
    { 
        shad = GetComponent<Renderer>();
        lerp = Time.deltaTime * colorSpeed;

        CPPatternManager = GameObject.Find("DanceFloor").GetComponent<PatternManager>();
        CPTextUpdater = GameObject.Find("Canvas").GetComponent<TextUpdater>();
    }

    void TouchDown()
    {
        CPPatternManager.addTouch(this.gameObject.name, CPPatternManager.me);
    }
    void TouchEnded()
    {
        StartCoroutine(ColorReset());
        CPTextUpdater.PrintToPlayer(CPPatternManager.stringedMainPattern(0,true));
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

