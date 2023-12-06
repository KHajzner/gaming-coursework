using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu, difficultyMenu;
    bool menuOn = true;

    void Start()
    {
        FindObjectOfType<AudioManager>().Play("MainMenu");
        difficultyMenu.gameObject.SetActive(false);
    }
    public void SwitchMenu()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        menuOn = !menuOn;
        mainMenu.gameObject.SetActive(menuOn);
        difficultyMenu.gameObject.SetActive(!menuOn);
    }
    public void Difficulty(string dif)
    {
        FindObjectOfType<AudioManager>().Play("Click");
        GlobalVars.difficulty = dif;
        GlobalVars.crewScore = 20;
        if(GlobalVars.difficulty == "Easy"){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else{
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        }
    }

    public void Quit()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        Debug.Log("Quit!");
        Application.Quit();
    }
}
