using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{
    
    public bool isTreasure = false;
    bool hasInteracted;
    bool canInteract;
    public GameObject interact;
    GameObject ship;
    void Start(){
        ship = GameObject.Find("Ship");
        interact.SetActive(false);
    }
    
    void FixedUpdate(){
        if (canInteract && !hasInteracted){
            if (Input.GetMouseButtonDown(0)){
                interact.gameObject.SetActive(false);
                hasInteracted = true;
                if(isTreasure){
                    TreasureFound();
                }
                else{
                    Debug.Log("Not treausre");
                }
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision){
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
    void TreasureFound(){
        ship.GetComponent<ShipMovement>().FinishedGame();
        Debug.Log("Found treasure!");
    }
}
