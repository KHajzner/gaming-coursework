using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 5f;
    public GameObject player;
    public Animator animator;
    bool facesRight = false, slides, verctical;
    Vector2 direction, standardVector, initialPos;
    public bool up = true;

    void Start()
    {
        initialPos = player.GetComponent<Rigidbody2D>().position;
        Time.timeScale = 1;
    }

    void Update()
    {   
        direction.x = Input.GetAxisRaw("Horizontal");
        if (up){
            direction.y = Input.GetAxisRaw("Vertical");
        }
        else{
            direction.y = 0;
        }
        animator.SetBool("Moving", (direction.x != 0 || direction.y != 0));
    }

    void FixedUpdate()
    {
        if (slides){
            if (verctical){
                player.GetComponent<Rigidbody2D>().MovePosition(player.GetComponent<Rigidbody2D>().position + standardVector * speed * Time.fixedDeltaTime);
            }
            else {
                player.GetComponent<Rigidbody2D>().MovePosition(player.GetComponent<Rigidbody2D>().position + standardVector * speed * Time.fixedDeltaTime);
            }
        }
        else{
            player.GetComponent<Rigidbody2D>().MovePosition(player.GetComponent<Rigidbody2D>().position + direction * speed * Time.fixedDeltaTime);
        }
        SwitchRotation(direction * speed * Time.fixedDeltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
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

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "MudHorizontal" || collision.tag == "MudVertical" || collision.tag == "Water"){
            speed = 5f;
            slides = false;
        }
    }

    void SwitchRotation(Vector2 direction)
    {
        if ((direction.x > 0 && !facesRight) || (direction.x < 0 && facesRight)){
            facesRight = !facesRight;
            Vector3 face = transform.localScale;
            face.x *= -1;
            transform.localScale = face;
        }
    }
    
    //Move player to the starting position
    public void Unstuck()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        player.GetComponent<Rigidbody2D>().position = initialPos;
    }
}
