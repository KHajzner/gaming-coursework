using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class HealthPickUpSpawn : MonoBehaviour
{
    public GameObject heartPrefab;
    public int currentHeartNumber = 0;
    public Tilemap tilemap;
    public List<Vector3> freeSpots;
    private Vector3 chosenSpot;
    public TileBase tileBase;
    bool spawnHearts;
    void Start()
    {
        freeSpots = new List<Vector3>();

        for (int n = tilemap.cellBounds.xMin; n < tilemap.cellBounds.xMax; n++)
        {
            for (int p = tilemap.cellBounds.yMin; p < tilemap.cellBounds.yMax; p++)
            {
                Vector3Int localPlace = (new Vector3Int(n, p, (int)tilemap.transform.position.y));
                Vector3 place = tilemap.CellToWorld(localPlace);
                if (tilemap.HasTile(localPlace) && tilemap.GetTile(localPlace) == tileBase)
                {
                    freeSpots.Add(place);
                }
            }
        }
        StartCoroutine(SpawnHeartPickup());
    }
    
    IEnumerator SpawnHeartPickup()
    {
        while(true){
            if(currentHeartNumber < 2){
                yield return new WaitForSeconds(15f);
                FindObjectOfType<AudioManager>().Play("Event");
                chosenSpot = freeSpots[Random.Range(0, freeSpots.Count)];
                Instantiate(heartPrefab,chosenSpot,Quaternion.identity);
                currentHeartNumber ++;
            }
        }
    }
}
