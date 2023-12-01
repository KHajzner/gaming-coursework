using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    public float damage;
    void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Enemy"){
            collision.gameObject.GetComponent<UniversalBehaviour>().TakeDamage(damage);
        }
    }
}
