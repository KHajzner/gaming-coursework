using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeasantAI : MonoBehaviour
{
    public UniversalBehaviour UB;
    public Animator animator;
    public LayerMask layerMask;
    Coroutine chooseAttack = null;
    float distanceBetween, viewingDistance = 4f, hittingDistance = 1.5f;
    bool startedAttacking = false, attackCrew = false, startedReset = false, facesRight = true;
    Collider2D[] nearCrew;
    int attackNum;

    void FixedUpdate()
    {    
        //Start scanning for crew every few seconds
        if(!startedReset){
            startedReset = true;
            Invoke("ResetScan", 4f);
        }
        //Attack crew
        if(attackCrew){
            if(!startedAttacking){
                startedAttacking = true;
                chooseAttack = StartCoroutine(ChooseAttack());
            }
            UB.enemy.position = Vector2.MoveTowards(UB.enemy.position, nearCrew[0].transform.position, UB.speed * Time.deltaTime);
            animator.SetBool("Moving", true);
            SwitchRotation(nearCrew[0].transform.position);
        }
        //Attack player
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
            SwitchRotation(UB.player.transform.position);

        }
    }
    
    //Choose a random attack with different probabilities
    IEnumerator ChooseAttack()
    {
        float probability = Random.Range(0.0f, 1.0f);
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

    //Scans for nearby crew
    void ResetScan()
    {
        float radius = 3f;
        nearCrew = Physics2D.OverlapCircleAll(UB.enemy.position, radius, layerMask);
        if(nearCrew.Length == 0){
            attackCrew = false;
        }
        else{
            int rnd = Random.Range(1, 3);
            if (rnd == 1){
                attackCrew = true;
            }
            else{
                attackCrew = false;
            }
        }
        startedReset = false;
    }
    void SwitchRotation(Vector3 target)
    {
        if ((target.x < UB.enemy.position.x && facesRight) || (target.x > UB.enemy.position.x  && !facesRight)){
            facesRight = !facesRight;
            Vector3 face = transform.localScale;
            face.x *= -1;
            transform.localScale = face;
        }
    }
}
