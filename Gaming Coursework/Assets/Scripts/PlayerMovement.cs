using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 5f;
    public Rigidbody2D rb;
    bool slides;
    bool verctical;
    Vector2 direction;
    Vector2 standardVector;
    // Update is called once per frame
    void Update()
    {   
            direction.x = Input.GetAxisRaw("Horizontal");
            direction.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        if (slides){
            if (verctical){
                rb.MovePosition(rb.position + standardVector * speed * Time.fixedDeltaTime);
                }
            else {
                rb.MovePosition(rb.position + standardVector * speed * Time.fixedDeltaTime);
                }
        }
        else{
            rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Water"){
            speed = 3f;
        }

        if(collision.tag == "MudVertical"){
            speed = 7f;
            standardVector = new Vector2(0.0f, direction.y);
            slides = true;
            verctical = true;
        }
        
        if(collision.tag == "MudHorizontal"){
            speed = 7f;
            standardVector = new Vector2(direction.x, 0.0f);
            slides = true;
            verctical = false;
        }
    }

    void OnTriggerExit2D(Collider2D collision){
        if (collision.tag == "MudHorizontal" || collision.tag == "MudVertical" || collision.tag == "Water"){
            speed = 5f;
            slides = false;
        }
    }
}
