using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeasantAI : MonoBehaviour
{
    public Rigidbody2D player;
    public Rigidbody2D peasant;
    public float speed;
    float distanceBetween;
    float hittingDistance = 1f;
    float viewingDistance = 3.5f;
    bool near = false;
    int attackNum;
	public float health;
    public Animator animator;
    Coroutine damageRoutine = null;
    Coroutine flashRedRoutine = null;
    Coroutine chooseAttack = null;

    bool facesRight = true;
    bool startedAttacking = false;
    // Start is called before the first frame update
    void Start()
    {
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0){
            speed = 0;
        }
    }
    void FixedUpdate()
    {
        distanceBetween = Vector2.Distance(player.transform.position, peasant.position);
        if(hittingDistance >= distanceBetween && !startedAttacking){
            Debug.Log("I'm withing hitting distance");
            startedAttacking = true;
            chooseAttack = StartCoroutine(ChooseAttack());
        }
        if (distanceBetween < viewingDistance){
            peasant.position = Vector2.MoveTowards(peasant.position, player.position, speed * Time.deltaTime);
            animator.SetBool("Moving", true);
        }
        else{
            animator.SetBool("Moving", false);
        }
        SwitchRotation();

    }
    
    void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Lava"){
            damageRoutine = StartCoroutine(LavaDamage());
        }
        if(collision.tag == "Sword"){
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

    public void TakeDamage(float damage){
        health -= damage;
        animator.SetTrigger("Hurt");
        flashRedRoutine = StartCoroutine(FlashRed());
    }

    IEnumerator ChooseAttack(){
        attackNum = Random.Range(1,4);
        Debug.Log("Attacking with" + attackNum);
        animator.SetInteger("Attack", attackNum);
        animator.SetTrigger("Attacking");
        yield return new WaitForSeconds(1f);
        startedAttacking = false;
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
    void SwitchRotation(){
        Debug.Log(player.position.x);
        Debug.Log(peasant.position.x);
        if ((player.position.x < peasant.position.x && facesRight) || (player.position.x > peasant.position.x  && !facesRight)){
            facesRight = !facesRight;
            Vector3 face = transform.localScale;
            face.x *= -1;
            transform.localScale = face;
        }
    }
}
