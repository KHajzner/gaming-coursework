using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeasantAI : MonoBehaviour
{
    public UniversalBehaviour UB;
    float distanceBetween;
    float hittingDistance = 1.5f;
    float viewingDistance = 4f;
    int attackNum;
    float probability;
    public Animator animator;
    Coroutine chooseAttack = null;
    bool startedAttacking = false;
    bool attackCrew = false;
    public LayerMask layerMask;
    Collider2D[] nearCrew;
    bool startedReset = false;
    void FixedUpdate()
    {    
        if(!startedReset){
            startedReset = true;
            Invoke("ResetScan", 4f);
        }
        if(attackCrew){
            if(!startedAttacking){
                startedAttacking = true;
                chooseAttack = StartCoroutine(ChooseAttack());
            }
            UB.enemy.position = Vector2.MoveTowards(UB.enemy.position, nearCrew[0].transform.position, UB.speed * Time.deltaTime);
            animator.SetBool("Moving", true);
        }
        else{
            distanceBetween = Vector2.Distance(UB.player.transform.position, UB.enemy.position);
            if(hittingDistance >= distanceBetween && !startedAttacking){
                startedAttacking = true;
                chooseAttack = StartCoroutine(ChooseAttack());
            }
            if (distanceBetween < viewingDistance){
                UB.enemy.position = Vector2.MoveTowards(UB.enemy.position, UB.player.position, UB.speed * Time.deltaTime);
                animator.SetBool("Moving", true);
            }
            else{
                animator.SetBool("Moving", false);
            }

        }
    }
    
    IEnumerator ChooseAttack(){
        probability = Random.Range(0.0f, 1.0f);
        if (0.00 < probability && probability <= 0.40){
            attackNum=1;
        }
        else if(0.41 < probability && probability <= 0.50){
            attackNum=2;
        }
        else if(0.51 < probability && probability <= 0.95){
            attackNum=3;
        }
        else{
            attackNum=4;
        }
        animator.SetInteger("Attack", attackNum);
        animator.SetTrigger("Attacking");
        yield return new WaitForSeconds(2f);
        startedAttacking = false;
    }
    void ResetScan(){
        Debug.Log("hallo");
        float radius = 3f;
        nearCrew = Physics2D.OverlapCircleAll(UB.enemy.position, radius, layerMask);
        if(nearCrew.Length == 0){
            attackCrew = false;
        }
        else{
            int rnd = Random.Range(1, 3);
            Debug.Log(rnd);
            if (rnd == 1){
                attackCrew = true;
            }
            else{
                attackCrew = false;
            }
        }
        startedReset = false;
    }
}
