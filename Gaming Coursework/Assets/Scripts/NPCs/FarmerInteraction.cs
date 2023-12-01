using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FarmerInteraction : MonoBehaviour
{
    public GameObject introduction;
    public GameObject interact;

    public GameObject afterClick;
    public GameObject arrow;
    public GameObject nextScene;
    public NextScene nextSceneName;

    public TMP_Text farmerChat;
    public TMP_Text nextArea;
    bool canInteract = false;
    bool hasInteracted = false;
    // Start is called before the first frame update
    void Start()
    {
        introduction.gameObject.SetActive(false);
        interact.gameObject.SetActive(false);
        afterClick.gameObject.SetActive(false);
        arrow.gameObject.SetActive(false);
        nextScene.gameObject.SetActive(false);
    }

    void FixedUpdate(){
        if (canInteract && !hasInteracted){
            if (Input.GetMouseButtonDown(0)){
                introduction.gameObject.SetActive(true);
                interact.gameObject.SetActive(false);
                hasInteracted = true;
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Farmer"){
            if(!hasInteracted){
                interact.gameObject.SetActive(true);
            }
            canInteract = true;
       }
    }
    void OnTriggerExit2D(Collider2D collision){
        if(collision.tag == "Farmer"){
            interact.gameObject.SetActive(false);
       }
    }

    public void AfterClick(){
        introduction.gameObject.SetActive(false);
        afterClick.gameObject.SetActive(true);
        arrow.gameObject.SetActive(true);
        nextScene.gameObject.SetActive(true);
    }
    public void Accept(){
        nextSceneName.nextSceneName="Maze";
    }
    public void Deny(){
        nextArea.SetText("Continue");
        farmerChat.SetText("That's a shame. Best of luck!");
        nextSceneName.nextSceneName="BanditsHard";
    }
}
