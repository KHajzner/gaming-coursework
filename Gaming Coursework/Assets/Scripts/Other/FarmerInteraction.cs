using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FarmerInteraction : MonoBehaviour
{
    public GameObject introduction, interact, afterClick, arrow, nextScene;
    public NextScene nextSceneName;
    public TMP_Text farmerChat, nextArea;
    bool canInteract = false, hasInteracted = false;

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("FarmBackground");
        introduction.gameObject.SetActive(false);
        interact.gameObject.SetActive(false);
        afterClick.gameObject.SetActive(false);
        arrow.gameObject.SetActive(false);
        nextScene.gameObject.SetActive(false);
    }

    void FixedUpdate()
    {
        if (canInteract && !hasInteracted){
            if (Input.GetMouseButtonDown(0)){
                FindObjectOfType<AudioManager>().Play("Click");
                introduction.gameObject.SetActive(true);
                interact.gameObject.SetActive(false);
                hasInteracted = true;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Farmer"){
            if(!hasInteracted){
                interact.gameObject.SetActive(true);
            }
            canInteract = true;
       }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Farmer"){
            interact.gameObject.SetActive(false);
       }
    }

    public void AfterClick()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        introduction.gameObject.SetActive(false);
        afterClick.gameObject.SetActive(true);
        arrow.gameObject.SetActive(true);
        nextScene.gameObject.SetActive(true);
    }
    public void Accept()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        string dif = GlobalVars.difficulty;
        if (dif == null){
            dif = "Easy";
        }
        nextSceneName.nextSceneName="Maze"+dif;
    }
    
    public void Deny()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        if(GlobalVars.difficulty == "Easy"){
            GlobalVars.crewScore -= 3;
        }
        else{
            GlobalVars.crewScore -= 6;
        }
        nextArea.SetText("Continue");
        farmerChat.SetText("That's a shame. Best of luck!");
        nextSceneName.nextSceneName="Ocean";
    }
}
