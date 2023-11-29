using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeasantAI : MonoBehaviour
{
    public Rigidbody2D player;
    public Rigidbody2D peasant;
    public float speed;
    public float distanceBetween = 3f;
	public float health;
    public Animator animator;
    Coroutine damageRoutine = null;
    Coroutine flashRedRoutine = null;
    // Start is called before the first frame update
    void Start()
    {
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (health == 0){
            speed = 0;
            StartCoroutine(Death());
        }
    }
    
    void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Lava"){
            damageRoutine = StartCoroutine(LavaDamage());
        }
        if(collision.tag == "Player"){
            Debug.Log("Dealt dmg!");
            TakeDamage(5f);
        }
    }
    void OnTriggerExit2D(Collider2D collision){
        if(collision.tag == "Lava"){
            StopCoroutine(damageRoutine);
            StopCoroutine(flashRedRoutine);
            GetComponent<SpriteRenderer> ().color = Color.white;   
        }
    }
    void FixedUpdate()
    {
        if (Vector2.Distance(player.transform.position, peasant.position) < distanceBetween){
            peasant.position = Vector2.MoveTowards(peasant.position, player.position, speed * Time.deltaTime);
            animator.SetBool("Moving", true);
        }
        else{
            animator.SetBool("Moving", false);
        }
    }

    public void TakeDamage(float damage){
        health -= damage;
        animator.SetTrigger("Hurt");
        flashRedRoutine = StartCoroutine(FlashRed());
    }

    IEnumerator LavaDamage(){
        while (health > 0)
        {
            health -= 10f;
            animator.SetTrigger("Hurt");
            flashRedRoutine = StartCoroutine(FlashRed());
            yield return new WaitForSeconds(1f);
        }
    }
    IEnumerator Death(){
        animator.SetTrigger("Dead");
        yield return new WaitForSeconds(0.5f);
    	Destroy(gameObject);
    }

    IEnumerator FlashRed(){
        speed = 0;
        GetComponent<SpriteRenderer> ().color = Color.red;
        yield return new WaitForSeconds(0.3f); 
        speed = 2;
        GetComponent<SpriteRenderer> ().color = Color.white;     
        yield return new WaitForSeconds(0.3f); 
    }
}
