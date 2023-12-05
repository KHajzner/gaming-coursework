using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator;
    bool clickedRecently = false;

    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0) && !clickedRecently){
            StartCoroutine(RecentClick());
        }
    }

    //Add cooldown to the attack
    IEnumerator RecentClick()
    {
        clickedRecently = true;
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.6f);
        clickedRecently = false;
    }
}
