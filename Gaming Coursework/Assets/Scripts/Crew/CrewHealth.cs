using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewHealth : MonoBehaviour
{    
    public FloatingHealthBar healthBar;
    public CrewFlocking crewFlocking;
    public Animator animator;
    public float health;
    float maxHealth = 10;
    bool startedDying = false;

    
    void Start()
    {
        health = maxHealth;
        healthBar = GetComponentInChildren<FloatingHealthBar>();
        healthBar.UpdateHealthBar(health, maxHealth);
    }

    void Update()
    {
        if (health <= 0 && !startedDying){
            startedDying = true;
            crewFlocking.speed = 0;
            GlobalVars.crewOnBandit -= 1;
            StartCoroutine(Death());
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.UpdateHealthBar(health, maxHealth);
        animator.SetTrigger("Hurt");
    }

    IEnumerator Death()
    {
        animator.ResetTrigger("Hurt");
        animator.SetTrigger("Dead");
        yield return new WaitForSeconds(0.5f);
    	Destroy(gameObject);
    }
}
