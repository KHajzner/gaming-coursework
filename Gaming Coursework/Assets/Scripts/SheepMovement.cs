using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SheepMovement : MonoBehaviour
{
    public Rigidbody2D player;
    public Rigidbody2D sheep;
    public float distanceBetween = 3f;
    public float speed;
    public bool carrot = false;
    public Tilemap barnMap;
    public List<Vector3> freeSpots;
    private Vector3 chosenSpot;
    public float barnSpeed = 0.5f;
    public bool inBarn = false;
    void Start()
    {
        for (int n = barnMap.cellBounds.xMin; n < barnMap.cellBounds.xMax; n++)
        {
            for (int p = barnMap.cellBounds.yMin; p < barnMap.cellBounds.yMax; p++)
            {
                Vector3Int localPlace = (new Vector3Int(n, p, 0));
                Vector3 place = barnMap.CellToWorld(localPlace);
                if (barnMap.HasTile(localPlace))
                {
                    freeSpots.Add(place);
                }
            }
        } 
        InvokeRepeating("MoveInBarn", 10.0f, 5f);
    }
    void FixedUpdate()
    {
        if (Vector2.Distance(player.transform.position, sheep.position) < distanceBetween && carrot){
            sheep.position = Vector2.MoveTowards(sheep.position, player.position, speed * Time.deltaTime);
        }
        if(inBarn){
            transform.position = Vector3.MoveTowards(transform.position, chosenSpot, barnSpeed * Time.fixedDeltaTime);
        }
    }

    void OnTriggerStay2D(Collider2D collision) {
        if (collision.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            carrot = true;
        }
    }


    void OnTriggerExit2D(Collider2D collision){
        if (collision.tag == "BarnEntrance"){
            carrot = false;
            inBarn = true;
        }
    }
    
    void MoveInBarn(){
            chosenSpot = freeSpots[Random.Range(0, freeSpots.Count)];
    }
}