using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawning : MonoBehaviour
{
    public Rigidbody2D peasant;
    public Rigidbody2D knight;
    public Tilemap spawnArea;
    public List<Vector3> freeSpots;
    public float peasantsOnSpawn;
    public float knightsOnSpawn;
    private Vector3 chosenSpot;

    void Start()
    {
        freeSpots = new List<Vector3>();
        for (int n = spawnArea.cellBounds.xMin; n < spawnArea.cellBounds.xMax; n++)
        {
            for (int p = spawnArea.cellBounds.yMin; p < spawnArea.cellBounds.yMax; p++)
            {
                Vector3Int localPlace = (new Vector3Int(n, p, (int)spawnArea.transform.position.y));
                Vector3 place = spawnArea.CellToWorld(localPlace);
                if (spawnArea.HasTile(localPlace))
                {
                    freeSpots.Add(place);
                }
            }
        }

        for (int i = 0; i < peasantsOnSpawn; i++)
        {
            chosenSpot = freeSpots[Random.Range(0, freeSpots.Count)];
            Instantiate(peasant,chosenSpot,Quaternion.identity);
            freeSpots.Remove(chosenSpot);
        }
        for (int i = 0; i < knightsOnSpawn; i++)
        {
            chosenSpot = freeSpots[Random.Range(0, freeSpots.Count)];
            Instantiate(knight,chosenSpot,Quaternion.identity);
            freeSpots.Remove(chosenSpot);
        }
    }
}
