using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeasantAI : MonoBehaviour
{
    public Rigidbody2D player;
    public UniversalBehaviour UB;
    float distanceBetween;
    float hittingDistance = 1f;
    float viewingDistance = 3.5f;
    int attackNum;
    float probability;
    public Animator animator;
    Coroutine chooseAttack = null;
    bool startedAttacking = false;

    void FixedUpdate()
    {
        distanceBetween = Vector2.Distance(player.transform.position, UB.enemy.position);
        if(hittingDistance >= distanceBetween && !startedAttacking){
            startedAttacking = true;
            chooseAttack = StartCoroutine(ChooseAttack());
        }
        if (distanceBetween < viewingDistance){
            UB.enemy.position = Vector2.MoveTowards(UB.enemy.position, player.position, UB.speed * Time.deltaTime);
            animator.SetBool("Moving", true);
        }
        else{
            animator.SetBool("Moving", false);
        }
    }
    
    IEnumerator ChooseAttack(){
        probability = Random.Range(0.0f, 1.0f);
        Debug.Log(probability);
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
}
