using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Lava : MonoBehaviour
{
    public Tilemap spawnArea;
    public Tilemap lavaArea;
    public float delay;
    float delayBetween = 0.5f;
    public Vector3 offset;
    void Start()
    {
        Collider2D col = GetComponent<Collider2D>();
        if (col != null){
             col.enabled = false;
        }
        StartCoroutine(MoveLava(delay));
    }
    void Update()
    {
        for (int n = lavaArea.cellBounds.xMin; n < lavaArea.cellBounds.xMax; n++)
        {
            for (int p = lavaArea.cellBounds.yMin; p < lavaArea.cellBounds.yMax; p++)
            {
               Vector3Int localPlace = (new Vector3Int(n, p, (int)lavaArea.transform.position.y));
                if (lavaArea.HasTile(localPlace))
                {
                    Vector3 cellPosition = lavaArea.CellToWorld(localPlace);
                    var emptyTheCell = spawnArea.WorldToCell(cellPosition);
                    spawnArea.SetTile(emptyTheCell, null);
                }
            }
        }
    }
    IEnumerator MoveLava(float delay)
    {
        yield return new WaitForSeconds(delay);
        transform.position = offset;
        for (int i = 0; i<3; i++)
        {
            yield return new WaitForSeconds(delayBetween);
            transform.position = new Vector3(0, 0, 0);
            yield return new WaitForSeconds(delayBetween);
            transform.position = offset;
        }
        
        for (int n = lavaArea.cellBounds.xMin; n < lavaArea.cellBounds.xMax; n++)
        {
            for (int p = lavaArea.cellBounds.yMin; p < lavaArea.cellBounds.yMax; p++)
            {
                Vector3Int localPlace = (new Vector3Int(n, p, (int)lavaArea.transform.position.y));
                if (spawnArea.HasTile(localPlace))
                {
                    var tilePos = spawnArea.WorldToCell(transform.position);
                    spawnArea.SetTile(tilePos, null);
                }
            }
        }

        Collider2D col = GetComponent<Collider2D>();
        if (col != null){
             col.enabled = true;
        }
    }
}