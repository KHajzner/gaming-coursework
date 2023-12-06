using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawning : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Tilemap spawnArea;
    public List<Vector3> freeSpots;
    public int enemyOnSpawn;
    private Vector3 chosenSpot;
    void Awake()
    {
        //Choose random number of enemies on spawn
        int max;
        if(GlobalVars.difficulty == "Hard"){
            max = 8;
        }
        else{
            max = 5;
        }
        enemyOnSpawn = Random.Range(3, max);
    }

    void Start()
    {
        FindObjectOfType<AudioManager>().Play("BanditBackground");
        //Get all the tiles from the spawnArea tilemap
        freeSpots = new List<Vector3>();
        for (int n = spawnArea.cellBounds.xMin; n < spawnArea.cellBounds.xMax; n++){
            for (int p = spawnArea.cellBounds.yMin; p < spawnArea.cellBounds.yMax; p++){
                Vector3Int localPlace = (new Vector3Int(n, p, (int)spawnArea.transform.position.y));
                Vector3 place = spawnArea.CellToWorld(localPlace);
                if (spawnArea.HasTile(localPlace)){
                    freeSpots.Add(place);
                }
            }
        }

        //Choose a random tile and spawn an enemy on it
        for (int i = 0; i < enemyOnSpawn; i++){
            chosenSpot = freeSpots[Random.Range(0, freeSpots.Count)];
            Instantiate(enemyPrefab,chosenSpot,Quaternion.identity);
            freeSpots.Remove(chosenSpot);
        }
    }
}
