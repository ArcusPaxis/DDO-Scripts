using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextUpdater : MonoBehaviour {

    public GameObject player1, player2;
    private TextMeshProUGUI player1Text, player2Text;

    PatternManager TUPatternManager;

	void Start ()
    {
        player1 = new GameObject();
        player1Text = player1.GetComponent<TextMeshProUGUI>();
        player2 = new GameObject();
        player2Text = player2.GetComponent<TextMeshProUGUI>();

        TUPatternManager = GameObject.Find("DanceFloor").GetComponent<PatternManager>();
	}
	
	void Update ()
    {
        //make a text timer updater function
	}

    public void UpdateText(/*string message, int playerNr*/)
    {
        //A// player1Text.text = TUPatternManager.stringedMainPattern(0);
    }
}
