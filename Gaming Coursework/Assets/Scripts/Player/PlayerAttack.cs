using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator;
    void Update(){
        if (Input.GetMouseButtonDown(0)){
              Debug.Log("attack :3!");
        animator.SetTrigger("Attack");
        }
    }
}
