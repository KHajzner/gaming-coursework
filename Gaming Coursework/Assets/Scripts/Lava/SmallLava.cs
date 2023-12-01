using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SmallLava : MonoBehaviour
{
    public Tilemap spawnArea;
    public Tilemap lavaArea;
    public Vector3 offset;
    void OnEnable()
    {
        transform.position = offset;
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
    

}
