using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EndBanditEarly : MonoBehaviour
{

    public GameObject abandon, confirmation;
    public CrewSpawning crewSpawning;

    void Start(){
        confirmation.SetActive(false);
    }
    public void AbandonCrew()
    {        
        abandon.SetActive(false);
        confirmation.SetActive(true);
    }
    
    public void ConfirmationYes()
    {
        GlobalVars.crewScore = GlobalVars.crewScore - crewSpawning.crewOnStart;
        SceneManager.LoadScene("Farmer");
    }

    public void ConfirmationNo()
    {
        confirmation.SetActive(false);
        abandon.SetActive(true);
    }
}
