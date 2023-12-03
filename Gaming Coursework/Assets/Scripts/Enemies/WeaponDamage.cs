using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    public float dmg;
    void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Player"){
            collision.gameObject.GetComponent<PlayerHealth>().Damage(dmg);
        }
        if(collision.tag == "Crew"){
            collision.gameObject.GetComponent<CrewHealth>().TakeDamage(dmg);
        }
    }
}
