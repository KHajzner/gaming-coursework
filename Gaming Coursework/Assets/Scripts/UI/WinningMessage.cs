using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinningMessage : MonoBehaviour
{
    public GameObject won, player;
    public TMP_Text winningText;
    public int enemiesBeaten = 0, enemiesToBeat = 2;
    public CrewSpawning crewSpawning;

    void Start()
    {
        won.gameObject.SetActive(false);
    }

    void Update()
    {
        if(enemiesBeaten == enemiesToBeat)
        {
            UpdateWinningText();
            won.gameObject.SetActive(true);
            player.GetComponent<PlayerMovement>().ableToMove = false;
        }
    }
    void UpdateWinningText(){
        if(GlobalVars.crewOnBandit == 0){
            winningText.SetText("You didn't manage to save any crew :(! Continue looking for the treasure despite this setback.");
        }
        else{
            winningText.SetText("You've saved " + GlobalVars.crewOnBandit + " of your crew! Continue looking for the treasure!");
        }
        GlobalVars.crewScore = GlobalVars.crewScore - (crewSpawning.crewOnStart - GlobalVars.crewOnBandit);
    }
}
