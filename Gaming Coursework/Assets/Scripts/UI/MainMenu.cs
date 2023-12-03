using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu, difficultyMenu;
    bool menuOn = true;
    void Start(){
        difficultyMenu.gameObject.SetActive(false);
    }
    public void SwitchMenu(){
        menuOn = !menuOn;
        mainMenu.gameObject.SetActive(menuOn);
        difficultyMenu.gameObject.SetActive(!menuOn);
    }
    public void Difficulty(string dif){
        GlobalVars.difficulty = dif;
        GlobalVars.crewScore = 20;
        if(GlobalVars.difficulty == "Easy"){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else{
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        }
    }
    public void Quit(){
        Debug.Log("Quit!");
        Application.Quit();
    }
}
