using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FarmerInMaze : MonoBehaviour
{
    public GameObject initialMenu, endEarlyConfirmation, continuationMenu;
    public TMP_Text endMessage;
    bool menuOn = true;
    void Start()
    {
        endEarlyConfirmation.gameObject.SetActive(false);
        continuationMenu.gameObject.SetActive(false);
    }
    public void SwitchMenu()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        menuOn = !menuOn;
        initialMenu.gameObject.SetActive(menuOn);
        endEarlyConfirmation.gameObject.SetActive(!menuOn);
    }
    public void ConfirmationYes()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        continuationMenu.gameObject.SetActive(true);
        endEarlyConfirmation.gameObject.SetActive(false);
        endMessage.SetText("Oh that's too bad. Good luck!");
    }
    public void ConfirmationNo(){
        SwitchMenu();
    }
}
