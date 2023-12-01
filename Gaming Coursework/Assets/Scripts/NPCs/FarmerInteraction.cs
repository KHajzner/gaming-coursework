using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmer : MonoBehaviour
{
    public GameObject introduction;
    public GameObject interact;

    public GameObject acceptMsg;
    public GameObject arrow;
    public GameObject nextScene;
    bool canInteract = false;
    bool hasInteracted = false;
    // Start is called before the first frame update
    void Start()
    {
        introduction.gameObject.SetActive(false);
        interact.gameObject.SetActive(false);
        acceptMsg.gameObject.SetActive(false);
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

    public void Accept(){
        introduction.gameObject.SetActive(false);
        acceptMsg.gameObject.SetActive(true);
        arrow.gameObject.SetActive(true);
        nextScene.gameObject.SetActive(true);
    }
}
