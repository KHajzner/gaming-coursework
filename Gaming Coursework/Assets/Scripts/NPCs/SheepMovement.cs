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
    public Barn barn;
    public GameObject interact;
    Vector3 raycastDirection;
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
        interact.SetActive(false);
        InvokeRepeating("MoveInBarn", 10.0f, 5f);
    }
    void FixedUpdate()
    {
        LayerMask layerMask = LayerMask.GetMask("Player");
        raycastDirection = player.position - sheep.position;
        RaycastHit2D hit = Physics2D.Raycast(sheep.position, raycastDirection);
        if (inBarn){
            sheep.position = Vector2.MoveTowards(transform.position, chosenSpot, barnSpeed * Time.fixedDeltaTime);
        }
        else if (hit.collider.name != "Wall" && carrot){
            sheep.position = Vector2.MoveTowards(sheep.position, player.position, speed * Time.deltaTime);
        }

    }

    void OnTriggerStay2D(Collider2D collision) {
        if (collision.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            carrot = true;
            interact.SetActive(false);
        }
    }
    void OnTriggerExit2D(Collider2D collision){
        if(collision.tag == "Player"){
            interact.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Player" && !carrot){
            interact.SetActive(true);
        }
        if (collision.tag == "BarnEntrance"){
            carrot = false;
            inBarn = true;
            interact.SetActive(false);
            barn.sheepCount += 1;
            barn.UpdateFarmerMessage();      
        }

    }
    void MoveInBarn(){
        chosenSpot = freeSpots[Random.Range(0, freeSpots.Count)];
    }
}