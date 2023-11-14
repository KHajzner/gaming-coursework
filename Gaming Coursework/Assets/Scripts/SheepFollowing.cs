using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepFollowing : MonoBehaviour
{
    // Start is called before the first frame update

    public Rigidbody2D player;
    public Rigidbody2D sheep;
    public float distanceBetween;
    public float speed;
    public bool carrot = false;

    void Update()
    {
        if (Vector2.Distance(player.transform.position, sheep.position) < distanceBetween && carrot){
            sheep.position = Vector2.MoveTowards(sheep.position, player.position, speed * Time.deltaTime);
        }
    }

    void OnTriggerStay2D(Collider2D collision) 
    {
        if (collision.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            carrot = true;
        }
    }
}