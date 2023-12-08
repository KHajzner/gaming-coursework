using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Barn : MonoBehaviour
{

    public int sheepCount;
    public SheepSpawning sheepSpawning; 
    public TMP_Text sheepMessage;
    public FarmerInMaze farmerInMaze;
    public GameObject continuationMenu;
    bool called = false;

    void Start()
    {
        sheepCount = 0;
        sheepMessage.SetText("There are " + (sheepSpawning.sheepPerSpawn - sheepCount) + " sheep left.");
    }

    void Update()
    {
        if (sheepCount == sheepSpawning.sheepPerSpawn && !called){
            called = true;
            farmerInMaze.continuationMenu.SetActive(true);
            farmerInMaze.initialMenu.SetActive(false);
            farmerInMaze.endEarlyConfirmation.SetActive(false);
            farmerInMaze.endMessage.SetText("Nice! You got all the sheep. Thanks. Here are the oranges.");
        }
    }

    public void UpdateFarmerMessage(){
        FindObjectOfType<AudioManager>().Play("Completed");
        sheepMessage.SetText("There are " + (sheepSpawning.sheepPerSpawn - sheepCount) + " sheep left.");
    }
}
