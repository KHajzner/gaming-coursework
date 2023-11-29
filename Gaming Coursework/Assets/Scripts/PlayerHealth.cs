using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

	public float health;
    Coroutine damageRoutine = null;
    Coroutine flashRedRoutine = null;

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
            Time.timeScale = 0;
            lost.gameObject.SetActive(true);
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
            StopCoroutine(flashRedRoutine);
        }
    }
    IEnumerator LavaDamage(){
        while (health > 0)
        {
            Debug.Log("Entered Lava");
            health -= 10f;
            flashRedRoutine = StartCoroutine(FlashRed());
            yield return new WaitForSeconds(1f);
        }
    }
    public void Restart(){
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    IEnumerator  FlashRed(){
        GetComponent<SpriteRenderer> ().color = Color.red;
        yield return new WaitForSeconds(0.3f); 
        GetComponent<SpriteRenderer> ().color = Color.white;     
        yield return new WaitForSeconds(0.3f); 
    }
}
