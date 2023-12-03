using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Won : MonoBehaviour
{
    public GameObject won;
    public TMP_Text winningText;
    public int enemiesBeaten = 0;
    int enemiesToBeat = 2;
    // Start is called before the first frame update
    void Start()
    {
        won.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(enemiesBeaten == enemiesToBeat)
        {
            UpdateWinningText();
            won.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }
    void UpdateWinningText(){
        if(GlobalVars.crewOnBandit == 0){
            winningText.SetText("You didn't manage to save any crew :(! Continue looking for the treasure despite this setback.");
        }
        else{
            winningText.SetText("You've saved " + GlobalVars.crewOnBandit + " of your crew! Continue looking for the treasure!");
        }
    }
}
