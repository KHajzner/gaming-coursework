using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LavaBehaviour : MonoBehaviour
{
    public Tilemap smallPool, spawnArea, middlePool, bigPool, movableArea, obstacles, currentLava;
    public float middleDelay, bigDelay;
    public TileBase tileBase;
    float delayBetween = 0.5f;

    void Awake()
    {
        currentLava = smallPool;
        addObstacles(smallPool);
        removeSpawn(smallPool);
        changeMovableArea(smallPool);
    }

    void Start()
    {
        middlePool.gameObject.SetActive(false);
        bigPool.gameObject.SetActive(false);
        StartCoroutine(MoveLava(middleDelay, middlePool));
        StartCoroutine(MoveLava(bigDelay, bigPool));
    }

    //Remove tiles from the spawn area where lava is
    void removeSpawn(Tilemap lavaPool)
    {
        foreach (var position in lavaPool.cellBounds.allPositionsWithin){
            if(lavaPool.HasTile(position)){
                Vector3Int spawnAreaPosition = new Vector3Int(position.x, position.y, 0);
                spawnArea.SetTile(spawnAreaPosition, null);
            }
        }
    }

    //Remove tiles from the movable area tilemap
    void changeMovableArea(Tilemap lavaPool)
    {
        foreach (var position in lavaPool.cellBounds.allPositionsWithin){
            if(lavaPool.HasTile(position)){
                Vector3Int movableAreaPosition = new Vector3Int(position.x, position.y, 0);
                movableArea.SetTile(movableAreaPosition, tileBase);
            }
        }
    }

    //Add tiles to the obstacles tilemap where lava is
    void addObstacles(Tilemap lavaPool)
    {
        foreach (var position in lavaPool.cellBounds.allPositionsWithin){
            if(lavaPool.HasTile(position)){
                Vector3Int obstaclePosition = new Vector3Int(position.x, position.y, 0);
                obstacles.SetTile(obstaclePosition, tileBase);
            }
        }
    }

    //Expand lava pools after specified time
    IEnumerator MoveLava(float delay, Tilemap lavaPool)
    {
        Collider2D[] movingColliders = lavaPool.GetComponents<Collider2D>();
        foreach(var collider in movingColliders){
            collider.enabled = false;
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
        removeSpawn(lavaPool);
        addObstacles(lavaPool);
        changeMovableArea(lavaPool);
        Collider2D[] colliders = currentLava.GetComponents<Collider2D>();
        foreach(var collider in colliders){
            collider.enabled = false;
        }
        foreach(var collider in movingColliders){
            collider.enabled = true;
        }
        currentLava = lavaPool;
    }
}
