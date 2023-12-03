using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public float dmg;
    void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Player"){
            playerHealth.Damage(dmg);
        }
        if(collision.tag == "Crew"){
            collision.gameObject.GetComponent<CrewHealth>().TakeDamage(dmg);
        }
    }
}
