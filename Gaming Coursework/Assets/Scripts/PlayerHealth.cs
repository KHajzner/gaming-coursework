using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

	public float health;
    Coroutine damageRoutine = null;
    // Start is called before the first frame update
    void Start()
    {
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (health == 0){
        	Debug.Log("You Lost!");
        }
    }
    
    void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Lava"){
            damageRoutine = StartCoroutine(LavaDamage());
        }
    }
    void OnTriggerExit2D(Collider2D collision){
        if(collision.tag == "Lava"){
            Debug.Log("uwu");
            StopCoroutine(damageRoutine);
        }
    }
    IEnumerator LavaDamage(){
        while (health > 0)
        {
            Debug.Log("Entered Lava");
            health -= 10f;
            yield return new WaitForSeconds(1f);
        }
    }
}
