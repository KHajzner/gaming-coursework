using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{

    public GameObject ship;
    Vector2 direction;
    float initMass;
    float currentMass;
    // Start is called before the first frame update
    void Start()
    {
        float currentMass = 20;
        GlobalVars.crewScore = 20;
        ship.GetComponent<Rigidbody2D>().mass = currentMass/10;
    }

    void Update(){
        direction.x = Input.GetAxisRaw("Horizontal")*3;
        direction.y = Input.GetAxisRaw("Vertical")*3;

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        ship.GetComponent<Rigidbody2D>().AddForce(direction, ForceMode2D.Force);
    }
    public void ThrowCrewOverboard(){
        GlobalVars.crewScore = GlobalVars.crewScore - 1;
        Debug.Log(GlobalVars.crewScore);
        Debug.Log(currentMass);
        ship.GetComponent<Rigidbody2D>().mass = (ship.GetComponent<Rigidbody2D>().mass  - 0.1f);
        Debug.Log(ship.GetComponent<Rigidbody2D>().mass );
    }
}
