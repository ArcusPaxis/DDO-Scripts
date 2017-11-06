using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextManager : MonoBehaviour {

    private TextMeshProUGUI player1Text, player2Text;
    private TextMeshProUGUI textTimer;

    TimeManager TUTimeManager;
    PatternManager TUPatternManager;

	void Start ()
    {
        player1Text = GameObject.Find("Player1TextBody").GetComponent<TextMeshProUGUI>();
        player2Text = GameObject.Find("Player2TextBody").GetComponent<TextMeshProUGUI>();
        textTimer = GameObject.Find("TimerText").GetComponent<TextMeshProUGUI>();

        TUTimeManager = this.GetComponent<TimeManager>();
        TUPatternManager = GameObject.Find("DanceFloor").GetComponent<PatternManager>();
	}
	
	void Update ()
    { textTimer.text = "Timer: " + TUTimeManager.GetTimeStamp().ToString("0.0"); }

    public void PrintToPlayer(string message, int playerNr = 0)
    {
        if (playerNr == 0)
        {
            player1Text.text = message;
            player2Text.text = message;
        }
        if (playerNr == 1)
        {
            player1Text.text = message;
        }
        if (playerNr == 2)
        {
            player2Text.text = message;
        }
    }
}
