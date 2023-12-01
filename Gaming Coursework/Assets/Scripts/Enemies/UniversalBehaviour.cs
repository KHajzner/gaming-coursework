using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalBehaviour : MonoBehaviour
{
    public float health;
    public float armour = 1f;
    public float speed;

    //Entities
    public Rigidbody2D player;
    public Rigidbody2D enemy;

    //Routines
    Coroutine damageRoutine = null;
    Coroutine flashRedRoutine = null;

    //Animations
    public Animator animator;
    bool facesRight = true;
   void Start()
    {
        health = 100;
    }

    void Update()
    {
        if (health <= 0){
            speed = 0;
            StartCoroutine(Death());
        }
    }
    void FixedUpdate(){
        SwitchRotation();
    }
    void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Lava"){
            damageRoutine = StartCoroutine(LavaDamage());
        }
    }
    void OnTriggerExit2D(Collider2D collision){
        if(collision.tag == "Lava"){
            StopCoroutine(damageRoutine);
            StopCoroutine(flashRedRoutine);
            GetComponent<SpriteRenderer> ().color = Color.white;   
        }
    }
    public void TakeDamage(float damage){
        health -= (damage/armour);
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
    IEnumerator FlashRed(){
        speed = 0;
        GetComponent<SpriteRenderer> ().color = Color.red;
        yield return new WaitForSeconds(0.3f); 
        speed = 2;
        GetComponent<SpriteRenderer> ().color = Color.white;     
        yield return new WaitForSeconds(0.3f); 
    }
    IEnumerator Death(){
        animator.ResetTrigger("Hurt");
        animator.SetTrigger("Dead");
        yield return new WaitForSeconds(1f);
    	Destroy(gameObject);
    }
    
    void SwitchRotation(){
        if ((player.position.x < enemy.position.x && facesRight) || (player.position.x > enemy.position.x  && !facesRight)){
            facesRight = !facesRight;
            Vector3 face = transform.localScale;
            face.x *= -1;
            transform.localScale = face;
        }
    }
}
