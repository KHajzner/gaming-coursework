using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LavaBehaviour : MonoBehaviour
{
    public Tilemap smallPool, spawnArea, middlePool, bigPool, movableArea, obstacles;
    public Tilemap currentLava;
    public float middleDelay, bigDelay;
    public TileBase tileBase;
    float delayBetween = 0.5f;
    // Start is called before the first frame update
    void Awake(){
        currentLava = smallPool;
        removeSpawn(smallPool);
        addObstacles(smallPool);
        changeMovableArea(smallPool);
    }
    void Start()
    {
        middlePool.gameObject.SetActive(false);
        bigPool.gameObject.SetActive(false);
        StartCoroutine(MoveLava(middleDelay, middlePool));
        StartCoroutine(MoveLava(bigDelay, bigPool));
    }

    void removeSpawn(Tilemap lavaPool){
        foreach (var position in lavaPool.cellBounds.allPositionsWithin){
            if(lavaPool.HasTile(position)){
                Vector3Int spawnAreaPosition = new Vector3Int(position.x, position.y, 0);
                spawnArea.SetTile(spawnAreaPosition, null);
            }
        }
    }
    void changeMovableArea(Tilemap lavaPool){
        foreach (var position in lavaPool.cellBounds.allPositionsWithin){
            if(lavaPool.HasTile(position)){
                Vector3Int movableAreaPosition = new Vector3Int(position.x, position.y, 0);
                movableArea.SetTile(movableAreaPosition, tileBase);
            }
        }
    }
    void addObstacles(Tilemap lavaPool){
        foreach (var position in lavaPool.cellBounds.allPositionsWithin){
            if(lavaPool.HasTile(position)){
                Vector3Int obstaclePosition = new Vector3Int(position.x, position.y, 0);
                obstacles.SetTile(obstaclePosition, tileBase);
            }
        }
    }
    IEnumerator MoveLava(float delay, Tilemap lavaPool){
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
        changeMovableArea(smallPool);
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
