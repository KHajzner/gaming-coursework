using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SheepMovement : MonoBehaviour
{
    public Rigidbody2D player, sheep;
    public float speed,  barnSpeed = 0.5f;
    public bool carrot = false, inBarn = false;
    public Tilemap barnMap;
    public List<Vector3> freeSpots;
    public Barn barn;
    public GameObject interact;
    private Vector3 chosenSpot;
    Vector3 raycastDirection;

    void Start()
    {
        //Get all the spots in the barn
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
        interact.SetActive(false);
        InvokeRepeating("MoveInBarn", 10.0f, 5f);
    }
    void FixedUpdate()
    {
        //Check if player is in line of sight
        LayerMask layerMask = LayerMask.GetMask("Player");
        raycastDirection = player.position - sheep.position;
        RaycastHit2D hit = Physics2D.Raycast(sheep.position, raycastDirection);
        
        //Walk to a randomly chosen spot if in the barn
        if (inBarn){
            sheep.position = Vector2.MoveTowards(transform.position, chosenSpot, barnSpeed * Time.fixedDeltaTime);
        }

        //Follow the player if in line of sight
        else if (hit.collider.name != "Wall" && carrot){
            sheep.position = Vector2.MoveTowards(sheep.position, player.position, speed * Time.deltaTime);
        }
    }

    //Start following player if they give you the carrot
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            carrot = true;
            interact.SetActive(false);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player"){
            interact.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !carrot){
            interact.SetActive(true);
        }
        if (collision.tag == "BarnEntrance" && !inBarn){
            carrot = false;
            inBarn = true;
            interact.SetActive(false);
            barn.sheepCount += 1;
            barn.UpdateFarmerMessage();      
        }

    }

    //Choose a random spawn in the barn
    void MoveInBarn(){
        chosenSpot = freeSpots[Random.Range(0, freeSpots.Count)];
    }
}