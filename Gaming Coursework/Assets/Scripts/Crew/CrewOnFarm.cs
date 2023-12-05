using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewOnFarm : MonoBehaviour
{
    public Rigidbody2D crew, player;
    public Animator animator;
    public float beNear;
    float distanceBetween;
    float speed = 2f;
    bool facesRight = false;
    
    void Update()
    {
        //Follow the player
        distanceBetween = Vector2.Distance(player.transform.position, crew.position);
        SwitchRotation();
        if (distanceBetween > beNear){
            crew.position = Vector2.MoveTowards(crew.position, player.position, speed * Time.deltaTime);
            animator.SetBool("Moving", true);
        }
        else{
            animator.SetBool("Moving", false);
        }
    }

    //Switch sprite to face the driection of movement
    void SwitchRotation()
    {
        if ((player.position.x < crew.position.x && facesRight) || (player.position.x > crew.position.x  && !facesRight)){
            facesRight = !facesRight;
            Vector3 face = transform.localScale;
            face.x *= -1;
            transform.localScale = face;
        }
    }
}
