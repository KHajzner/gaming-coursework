using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

	public float health;
    Coroutine damageRoutine = null;
    Coroutine deathRoutine = null;
    public Animator animator;

    public GameObject lost;
    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        Time.timeScale = 1;
        lost.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (health == 0){
        	Debug.Log("You Lost!");
            deathRoutine = StartCoroutine(Death());
            if (Input.GetKey("space"))
            {       
                Restart();
            }
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
            animator.SetTrigger("Hurt");
            yield return new WaitForSeconds(1f);
        }
    }
    IEnumerator Death(){
        animator.SetTrigger("Dead");
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0;
        lost.gameObject.SetActive(true);
    }

    public void Restart(){
        StopCoroutine(deathRoutine);
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

}
