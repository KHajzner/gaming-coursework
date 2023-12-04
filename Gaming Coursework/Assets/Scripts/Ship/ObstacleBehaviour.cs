using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{
    
    public bool isTreasure = false;
    bool hasInteracted;
    bool canInteract;
    public GameObject interact;
    void Start(){
        interact.SetActive(false);
    }
    void FixedUpdate(){
        if (canInteract && !hasInteracted){
            if (Input.GetMouseButtonDown(0)){
                interact.gameObject.SetActive(false);
                hasInteracted = true;
                if(isTreasure){
                    Debug.Log("Found treasure!");
                }
                else{
                    Debug.Log("Not treausre");
                }
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision){
        Debug.Log("hallo");
        if(collision.tag == "Player"){
            if(!hasInteracted){
                interact.SetActive(true);
            }
            canInteract = true;
       }
    }
    void OnTriggerExit2D(Collider2D collision){
        if(collision.tag == "Player"){
            interact.SetActive(false);
       }
    }
}
