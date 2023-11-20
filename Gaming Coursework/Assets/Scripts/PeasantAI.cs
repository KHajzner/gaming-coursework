using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeasantAI : MonoBehaviour
{
    public Rigidbody2D player;
    public Rigidbody2D peasant;
    public float speed;
    public float distanceBetween = 5f;

  void FixedUpdate()
    {
        if (Vector2.Distance(player.transform.position, peasant.position) < distanceBetween){
            peasant.position = Vector2.MoveTowards(peasant.position, player.position, speed * Time.deltaTime);
        }   
    }
}
