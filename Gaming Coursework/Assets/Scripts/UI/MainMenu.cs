using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu, difficultyMenu, mapMenu, optionsMenu;

    void Start()
    {
        FindObjectOfType<AudioManager>().Play("MainMenu");
        difficultyMenu.gameObject.SetActive(false);
        mapMenu.SetActive(false);
        optionsMenu.SetActive(false);
    }

    public void Back()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        mainMenu.gameObject.SetActive(true);
        difficultyMenu.gameObject.SetActive(false);
        optionsMenu.gameObject.SetActive(false);
    }

    public void Difficulty(string dif)
    {
        FindObjectOfType<AudioManager>().Play("Click");
        GlobalVars.difficulty = dif;
        GlobalVars.crewScore = 20;
        difficultyMenu.SetActive(false);
        mapMenu.SetActive(true);
    }

    public void DifficultyMenu(){
        mainMenu.SetActive(false);
        difficultyMenu.SetActive(true);
    }

    public void OptionsMenu(){
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }
    
    public void Quit()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        Application.Quit();
    }
}
