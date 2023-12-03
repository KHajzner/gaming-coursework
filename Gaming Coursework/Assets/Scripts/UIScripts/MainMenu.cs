using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu, difficultyMenu;
    void Start(){
        difficultyMenu.gameObject.SetActive(false);
    }
    public void ClickPlay(){
        mainMenu.gameObject.SetActive(false);
        difficultyMenu.gameObject.SetActive(true);
    }
    public void Difficulty(string dif){
        GlobalVars.difficulty = dif;
        Debug.Log(GlobalVars.difficulty);
        if(GlobalVars.difficulty == "Hard"){
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
