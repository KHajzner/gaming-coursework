using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SheepSpawning : MonoBehaviour
{

    public Rigidbody2D sheep;
    public Tilemap tilemap;
    public List<Vector3> freeSpots;
    public float sheepPerSpawn = 3f;
    private Vector3 chosenSpot;

    // Start is called before the first frame update
    void Start()
    { 
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

        for (int i = 0; i < sheepPerSpawn; i++)
        {
            chosenSpot = freeSpots[Random.Range(0, freeSpots.Count)];
            Instantiate(sheep,chosenSpot,Quaternion.identity);
            freeSpots.Remove(chosenSpot);
        }
    }
}
