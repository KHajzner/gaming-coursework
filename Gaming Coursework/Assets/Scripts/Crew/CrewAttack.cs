using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewAttack : MonoBehaviour
{
    public Animator animator;
    // Update is called once per frame
    void Start()
    {
        StartCoroutine(Attack());
    }
    IEnumerator Attack(){
        while(true){
            var probability = Random.Range(0.0f, 1.0f);
            if (probability <= 0.40){
                animator.SetTrigger("Attack");
            }
            yield return new WaitForSeconds(3f);
        }
    }
}
