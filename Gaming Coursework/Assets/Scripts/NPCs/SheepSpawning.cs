using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SheepSpawning : MonoBehaviour
{

    public Rigidbody2D sheep;
    public Tilemap tilemap;
    public List<Vector3> freeSpots;
    public float sheepPerSpawn;
    private Vector3 chosenSpot;

    void Start()
    { 
        FindObjectOfType<AudioManager>().Play("SheepBackground");
        //Ranomly choose number of sheep
        if(GlobalVars.difficulty == "Easy"){
            sheepPerSpawn = Random.Range(2, 5);
        }
        else{
            sheepPerSpawn = Random.Range(5, 8);
        }

        //Get all the possible spawning spots
        freeSpots = new List<Vector3>();

        for (int n = tilemap.cellBounds.xMin; n < tilemap.cellBounds.xMax; n++)
        {
            for (int p = tilemap.cellBounds.yMin; p < tilemap.cellBounds.yMax; p++)
            {
                Vector3Int localPlace = (new Vector3Int(n, p, (int)tilemap.transform.position.y));
                Vector3 place = tilemap.CellToWorld(localPlace);
                if (tilemap.HasTile(localPlace))
                {
                    freeSpots.Add(place);
                }
            }
        }

        //Spawn sheep
        for (int i = 0; i < sheepPerSpawn; i++)
        {
            chosenSpot = freeSpots[Random.Range(0, freeSpots.Count)];
            Instantiate(sheep,chosenSpot,Quaternion.identity);
            freeSpots.Remove(chosenSpot);
        }
    }
}
