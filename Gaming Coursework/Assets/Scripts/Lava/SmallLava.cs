using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SmallLava : MonoBehaviour
{
 public Tilemap lavaArea;
    public Tilemap spawnArea;
    public Tilemap midLava;
    public Tilemap bigLava;
    public float middelay;
    public float bigdelay;
        float delayBetween = 0.5f;
    void Start()
    {
        // Get the bounds of the lavaPool Tilemap
        BoundsInt bounds = lavaArea.cellBounds;

        // Loop through each cell in the lavaPool Tilemap
        foreach (Vector3Int cellPosition in bounds.allPositionsWithin)
        {
            // Check if the current cell is a lavaPool tile
            if (lavaArea.HasTile(cellPosition))
            {
                // Remove the tile in the spawnArea at the same position
                Vector3Int spawnAreaPosition = new Vector3Int(cellPosition.x, cellPosition.y, 0);
                spawnArea.SetTile(spawnAreaPosition, null);
            }
        }
        midLava.gameObject.SetActive(false);
        bigLava.gameObject.SetActive(false);
        StartCoroutine(MoveLava(middelay, midLava));
        // bigLava.StartMove(bigdelay);
    }

    IEnumerator MoveLava(float delay, Tilemap lavaPool)
    {
        Collider2D col = lavaPool.GetComponent<Collider2D>();
        if (col != null){
             col.enabled = false;
        }
        yield return new WaitForSeconds(delay);
                lavaPool.gameObject.SetActive(true);
        for (int i = 0; i<3; i++)
        {
            yield return new WaitForSeconds(delayBetween);
                    lavaPool.gameObject.SetActive(false);
            yield return new WaitForSeconds(delayBetween);
                    lavaPool.gameObject.SetActive(true);
        }
        // Get the bounds of the lavaPool Tilemap
        BoundsInt bounds = lavaPool.cellBounds;

        // Loop through each cell in the lavaPool Tilemap
        foreach (Vector3Int cellPosition in bounds.allPositionsWithin)
        {
            // Check if the current cell is a lavaPool tile
            if (lavaPool.HasTile(cellPosition))
            {
                // Remove the tile in the spawnArea at the same position
                Vector3Int spawnAreaPosition = new Vector3Int(cellPosition.x, cellPosition.y, 0);
                spawnArea.SetTile(spawnAreaPosition, null);
            }
        }

        if (col != null){
             col.enabled = true;
        }
    }

}
