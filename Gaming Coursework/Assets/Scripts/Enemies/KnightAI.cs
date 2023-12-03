using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightAI : MonoBehaviour
{
    public UniversalBehaviour UB;
    float distanceBetween;
    float hittingDistance = 1.5f;
    float waitTime;
    int attackNum;
    float probability;
    public Animator animator;
    Coroutine chooseAttack = null;
    bool startedAttacking = false;

    void FixedUpdate()
    {
        distanceBetween = Vector2.Distance(UB.player.transform.position, UB.enemy.position);
        if(hittingDistance >= distanceBetween && !startedAttacking){
            startedAttacking = true;
            chooseAttack = StartCoroutine(ChooseAttack());
        }
    }
    IEnumerator ChooseAttack(){
        probability = Random.Range(0.0f, 1.0f);
        if (0.00 < probability && probability <= 0.44){
            attackNum=1;
            waitTime=2f;
        }
        else{
            attackNum=2;
            waitTime=1f; 
        }
        animator.SetInteger("Attack", attackNum);
        animator.SetTrigger("Attacking");
        yield return new WaitForSeconds(waitTime);
        startedAttacking = false;
    }
}
