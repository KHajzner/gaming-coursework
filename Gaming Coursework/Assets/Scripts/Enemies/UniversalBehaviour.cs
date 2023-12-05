using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalBehaviour : MonoBehaviour
{
    public float health;
    float maxHealth = 100;
    public float armour = 1f;
    public float speed;
    bool startedDying = false;
    public string enemyType;
    
    //Entities
    public Rigidbody2D player;
    public Rigidbody2D enemy;

    //Routines
    Coroutine damageRoutine = null;
    Coroutine flashRedRoutine = null;

    //Animations
    public Animator animator;
    bool facesRight = true;

    //UI
    public EnemyCounter enemyCounter;
    [SerializeField] FloatingHealthBar healthBar;
    
    void Awake(){
        player = GameObject.Find("Player").GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        enemyCounter = GameObject.Find(enemyType + "Counter").GetComponent<EnemyCounter>();
        health = maxHealth;
        healthBar = GetComponentInChildren<FloatingHealthBar>();
        healthBar.UpdateHealthBar(health, maxHealth);
    }

    void Update()
    {
        if (health <= 0 && !startedDying){
            startedDying = true;
            speed = 0;
            StartCoroutine(Death());
            enemyCounter.UpdateCounter();
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
        healthBar.UpdateHealthBar(health, maxHealth);
        animator.SetTrigger("Hurt");
        flashRedRoutine = StartCoroutine(FlashRed());
    }
    IEnumerator LavaDamage(){
        while (health > 0)
        {
            health -= 10f;
                    healthBar.UpdateHealthBar(health, maxHealth);
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
        yield return new WaitForSeconds(0.5f);
    	Destroy(gameObject);
        startedDying = false;
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
