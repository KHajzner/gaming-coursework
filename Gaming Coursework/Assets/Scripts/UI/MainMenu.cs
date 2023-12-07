using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu, difficultyMenu, mapMenu;
    bool menuOn = true;

    void Start()
    {
        FindObjectOfType<AudioManager>().Play("MainMenu");
        difficultyMenu.gameObject.SetActive(false);
        mapMenu.SetActive(false);
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
        difficultyMenu.SetActive(false);
        mapMenu.SetActive(true);
    }
    
    public void Quit()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        Debug.Log("Quit!");
        Application.Quit();
    }
}
