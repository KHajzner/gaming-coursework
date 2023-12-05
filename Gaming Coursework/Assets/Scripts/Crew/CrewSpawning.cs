using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CrewSpawning : MonoBehaviour
{
    public GameObject crewPrefab;
    public Tilemap spawnArea;
    public List<Vector3> freeSpots;
    public int crewOnStart;
    private Vector3 chosenSpot;

    void Start()
    {
        //Get all tiles from the spawnArea Tilemap
        crewOnStart = Random.Range(4,7);
        GlobalVars.crewOnBandit = crewOnStart;
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

        //Choose a random spot and spawn a crewPrefab on it
        for (int i = 0; i < crewOnStart; i++)
        {
            chosenSpot = freeSpots[Random.Range(0, freeSpots.Count)];
            Instantiate(crewPrefab,chosenSpot,Quaternion.identity);
            freeSpots.Remove(chosenSpot);
        }
    }
}
