using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextUpdater : MonoBehaviour {

    public GameObject player1GO, player2GO;
    private TextMeshProUGUI player1Text, player2Text;
    public GameObject TextTimerGO;
    private TextMeshProUGUI textTimer;

    TimeManager TUTimeManager;
    PatternManager TUPatternManager;

	void Start ()
    {
        player1Text = GameObject.Find("Player1TextBody").GetComponent<TextMeshProUGUI>();
        player2Text = player2GO.GetComponent<TextMeshProUGUI>();
        textTimer = TextTimerGO.GetComponent<TextMeshProUGUI>();

        TUPatternManager = GameObject.Find("DanceFloor").GetComponent<PatternManager>();
	}
	
	void Update ()
    {
        textTimer.text = "Timer: " + TUTimeManager.GetTimeStamp().ToString("0.0");
    }

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
