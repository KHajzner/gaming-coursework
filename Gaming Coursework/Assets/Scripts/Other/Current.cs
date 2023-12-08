using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Current : MonoBehaviour
{
    public Vector2 currentDirection;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player"){
            FindObjectOfType<AudioManager>().Play("Slash");
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player"){
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(currentDirection, ForceMode2D.Force);
        }
    }
}
