using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu, optionsMenu;
    void Start()
    {
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(optionsMenu.activeInHierarchy){
                pauseMenu.SetActive(true);
                optionsMenu.SetActive(false);
            }
            else{
                if(Time.timeScale == 1){
                    pauseMenu.SetActive(true);
                    Time.timeScale = 0;
                }
                else{
                    pauseMenu.SetActive(false);
                    Time.timeScale = 1;
                }
            }
        }
    }
    public void Resume()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void Options()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void Quit()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        Application.Quit();
    }
    
    public void Back()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        pauseMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }
}
