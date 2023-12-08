using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{
    
    public bool isTreasure = false;
    bool hasInteracted, canInteract;
    public GameObject interact, notTreasure;
    GameObject ship;

    void Start()
    {
        ship = GameObject.Find("Ship");
        interact.SetActive(false);
        notTreasure.SetActive(false);
    }
    
    void FixedUpdate()
    {
        if (canInteract && !hasInteracted){
            if (Input.GetMouseButtonDown(0)){
                interact.gameObject.SetActive(false);
                hasInteracted = true;
                if(isTreasure){
                    TreasureFound();
                }
                else{
                    notTreasure.SetActive(true);
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player"){
            if(!hasInteracted){
                interact.SetActive(true);
            }
            canInteract = true;
       }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player"){
            interact.SetActive(false);
       }
    }
    void TreasureFound()
    {
        ship.GetComponent<ShipMovement>().FinishedGame();
    }
}
