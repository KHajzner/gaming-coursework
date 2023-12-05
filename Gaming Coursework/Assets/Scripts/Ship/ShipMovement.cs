using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShipMovement : MonoBehaviour
{

    public GameObject ship, finishedScreen;
    Vector2 direction, initialPos;
    float initMass, currentMass;
    public TMP_Text warning;
    public Sprite shipUp, shipDown, shipLeft, shipRight;
    public SpriteRenderer spriteRenderer;
    Rigidbody2D shipRB;
    void Start()
    {
        shipRB = ship.GetComponent<Rigidbody2D>();
        finishedScreen.SetActive(false);
        initialPos = ship.GetComponent<Rigidbody2D>().position;
        warning.gameObject.SetActive(false);
        float currentMass = 20;
        GlobalVars.crewScore = 20;
        shipRB.mass = currentMass/10;
        spriteRenderer = ship.GetComponent<SpriteRenderer>();
        
    }

    void Update()
    {
        direction.x = Input.GetAxisRaw("Horizontal")*5;
        direction.y = Input.GetAxisRaw("Vertical")*5;
        ChangeSprite();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        shipRB.AddForce(direction, ForceMode2D.Force);
    }

    //Decrease ship's mass by decreasing the number of crew on board
    public void ThrowCrewOverboard()
    {
        if(GlobalVars.crewScore > 1){
            GlobalVars.crewScore = GlobalVars.crewScore - 1;
            Debug.Log(GlobalVars.crewScore);
            Debug.Log(currentMass);
            shipRB.mass = (shipRB.mass  - 0.1f);
            Debug.Log(shipRB.mass );
        }
        else{
            warning.gameObject.SetActive(true);
        }
    }

    public void Unstuck()
    {
       shipRB.position = initialPos;
    }

    public void FinishedGame()
    {
        finishedScreen.SetActive(true);
    }

    void ChangeSprite()
    {
        if(direction.x > 0){
        spriteRenderer.sprite = shipRight; 
        }
        else if (direction.x < 0){
            spriteRenderer.sprite = shipLeft; 
        }
        else if(direction.y > 0){
            spriteRenderer.sprite = shipUp; 
        }
        else{
            spriteRenderer.sprite = shipDown; 
        }
    }
}
