using System.Collections;
using System.Collections.Generic;
using UnityEngine;
  
public class HealthPickUpBehaviour : MonoBehaviour
{
    public GameObject grid, player;
    void Start(){
        grid = GameObject.FindWithTag("Grid");
        player = GameObject.FindWithTag("Player");
    }
    void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Player"){
            if(player.GetComponent<PlayerHealth>().health < 10){
                collision.gameObject.GetComponent<PlayerHealth>().health += 1f;
                collision.gameObject.GetComponent<PlayerHealth>().ClampHealth();
                grid.GetComponent<HealthPickUpSpawn>().currentHeartNumber --;
                player.GetComponent<PlayerHealth>().UpdateHeartsHUD();
                Destroy(gameObject);
            }
        }
    }
}
