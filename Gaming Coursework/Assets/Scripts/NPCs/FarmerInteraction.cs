using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmer : MonoBehaviour
{
    public GameObject introduction;
    public GameObject interact;
    bool canInteract = false;
    // Start is called before the first frame update
    void Start()
    {
        introduction.gameObject.SetActive(false);
        interact.gameObject.SetActive(false);
    }

    void FixedUpdate(){
        if (canInteract){
            if (Input.GetMouseButtonDown(0)){
                introduction.gameObject.SetActive(true);
                interact.gameObject.SetActive(false);
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Farmer"){
            interact.gameObject.SetActive(true);
            canInteract = true;
       }
    }
    void OnTriggerExit2D(Collider2D collision){
        if(collision.tag == "Farmer"){
            interact.gameObject.SetActive(false);
       }
    }
}
