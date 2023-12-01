using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    float maxHealth = 10;
	public float health;
    Coroutine damageRoutine = null;
    Coroutine deathRoutine = null;
    public Animator animator;
    public GameObject lost;

    // Health UI by ariel oliveira
    private GameObject[] heartContainers;
    private Image[] heartFills;
    public Transform heartsParent;
    public GameObject heartContainerPrefab;
    public delegate void OnHealthChangedDelegate();
    public OnHealthChangedDelegate onHealthChangedCallback;
    //End of Health UI

    void Start()
    {
        health = 10;
        Time.timeScale = 1;
        lost.gameObject.SetActive(false);

        // Health UI by ariel oliveira
            heartContainers = new GameObject[(int)maxHealth];
            heartFills = new Image[(int)maxHealth];

            onHealthChangedCallback += UpdateHeartsHUD;
            InstantiateHeartContainers();
            UpdateHeartsHUD();
        // End of Health UI
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0){
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
            damageRoutine = StartCoroutine(RoutineDamage(1f));
        }
    }
    void OnTriggerExit2D(Collider2D collision){
        if(collision.tag == "Lava"){
            StopCoroutine(damageRoutine);
        }
    }
    public void Damage(float damageTaken){
            health -= damageTaken;
            animator.SetTrigger("Hurt");
            ClampHealth();
            
    }

    IEnumerator RoutineDamage(float damageTaken){
        while(health >= 0){
            health -= damageTaken;
            ClampHealth();
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
    
    // Health UI by ariel oliveira
    public void UpdateHeartsHUD()
    {
        SetHeartContainers();
        SetFilledHearts();
    }

    void SetHeartContainers()
    {
        for (int i = 0; i < heartContainers.Length; i++)
        {
            if (i < maxHealth)
            {
                heartContainers[i].SetActive(true);
            }
            else
            {
                heartContainers[i].SetActive(false);
            }
        }
    }

    void SetFilledHearts()
    {
        for (int i = 0; i < heartFills.Length; i++)
        {
            if (i < health)
            {
                heartFills[i].fillAmount = 1;
            }
            else
            {
                heartFills[i].fillAmount = 0;
            }
        }

        if (health % 1 != 0)
        {
            int lastPos = Mathf.FloorToInt(health);
            heartFills[lastPos].fillAmount = health % 1;
        }
    }
    void InstantiateHeartContainers()
    {
        for (int i = 0; i < maxHealth; i++)
        {
            GameObject temp = Instantiate(heartContainerPrefab);
            temp.transform.SetParent(heartsParent, false);
            heartContainers[i] = temp;
            heartFills[i] = temp.transform.Find("HeartFill").GetComponent<Image>();
        }
    }
    void ClampHealth()
    {
        health = Mathf.Clamp(health, 0, maxHealth);

        if (onHealthChangedCallback != null)
            onHealthChangedCallback.Invoke();
    }
    // End of Health UI
}
