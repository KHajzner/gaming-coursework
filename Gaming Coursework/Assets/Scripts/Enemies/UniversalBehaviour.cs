using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalBehaviour : MonoBehaviour
{
    public float health, maxHealth = 100, speed, armour = 1f;

    //Entities
    public Rigidbody2D player, enemy;
    public string enemyType;

    //Routines
    Coroutine damageRoutine = null;
    Coroutine flashRedRoutine = null;

    //Animations
    public Animator animator;
    bool startedDying = false;

    //UI
    public EnemyCounter enemyCounter;
    [SerializeField] FloatingHealthBar healthBar;
    
    void Awake()
    {
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Lava"){
            damageRoutine = StartCoroutine(LavaDamage());
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Lava"){
            StopCoroutine(damageRoutine);
            StopCoroutine(flashRedRoutine);
            GetComponent<SpriteRenderer> ().color = Color.white;   
        }
    }
    public void TakeDamage(float damage)
    {
        FindObjectOfType<AudioManager>().Play("EnemyHit");
        health -= (damage/armour);
        healthBar.UpdateHealthBar(health, maxHealth);
        animator.SetTrigger("Hurt");
        flashRedRoutine = StartCoroutine(FlashRed());
    }
    IEnumerator LavaDamage()
    {
        while (health > 0)
        {
            FindObjectOfType<AudioManager>().Play("EnemyHit");
            health -= 10f;
            healthBar.UpdateHealthBar(health, maxHealth);
            animator.SetTrigger("Hurt");
            flashRedRoutine = StartCoroutine(FlashRed());
            yield return new WaitForSeconds(1f);
        }
    }
    IEnumerator FlashRed()
    {
        speed = 0;
        GetComponent<SpriteRenderer> ().color = Color.red;
        yield return new WaitForSeconds(0.3f); 
        speed = 2;
        GetComponent<SpriteRenderer> ().color = Color.white;     
        yield return new WaitForSeconds(0.3f); 
    }
    IEnumerator Death()
    {
        FindObjectOfType<AudioManager>().Play("Death");
        animator.ResetTrigger("Hurt");
        animator.SetTrigger("Dead");
        yield return new WaitForSeconds(0.5f);
    	Destroy(gameObject);
        startedDying = false;
    }
}
