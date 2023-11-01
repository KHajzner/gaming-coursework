using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 5f;
    public Rigidbody2D rb;
    Vector2 direction;
    // Update is called once per frame
    void Update()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Sand"){
            speed = 3f;
        }

        if(collision.tag == "Ice"){
            speed = 7f;
        }
    }

    void OnTriggerExit2D(Collider2D collision){
        speed = 5f;
    }
}
