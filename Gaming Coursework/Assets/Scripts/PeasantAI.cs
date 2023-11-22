using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeasantAI : MonoBehaviour
{
    public Rigidbody2D player;
    public Rigidbody2D peasant;
    public float speed;
    public float distanceBetween = 5f;
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
        	Destroy(gameObject);
        }
    }
    
    void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Lava"){
            damageRoutine = StartCoroutine(LavaDamage());
        }
    }
    void OnTriggerExit2D(Collider2D collision){
        if(collision.tag == "Lava"){
            StopCoroutine(damageRoutine);
        }
    }
    void FixedUpdate()
    {
        if (Vector2.Distance(player.transform.position, peasant.position) < distanceBetween){
            peasant.position = Vector2.MoveTowards(peasant.position, player.position, speed * Time.deltaTime);
        }   
    }

    IEnumerator LavaDamage(){
        while (health > 0)
        {
            health -= 10f;
            yield return new WaitForSeconds(1f);
        }
    }
}
