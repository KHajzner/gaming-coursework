using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewWeapon : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Enemy"){
            collision.gameObject.GetComponent<UniversalBehaviour>().TakeDamage(5);
        }
    }
}
