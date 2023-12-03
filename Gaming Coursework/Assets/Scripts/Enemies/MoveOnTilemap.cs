using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Aoiti.Pathfinding;


public class MoveOnTilemap : MonoBehaviour
{

    public UniversalBehaviour UB;
    Vector3Int[] directions=new Vector3Int[4] {Vector3Int.left,Vector3Int.right,Vector3Int.up,Vector3Int.down };

    public Tilemap tilemap;
    public TileAndMovementCost[] tiles;
    Pathfinder<Vector3Int> pathfinder;

    [System.Serializable]
    public struct TileAndMovementCost
    {
        public Tile tile;
        public bool movable;
        public float movementCost;
    }

    public List<Vector3Int> path;
    [Range(0.001f,1f)]
    public float stepTime;
    float distanceBetween;
    bool startedMoving = false;


    public float DistanceFunc(Vector3Int a, Vector3Int b)
    {
        return (a-b).sqrMagnitude;
    }


    public Dictionary<Vector3Int,float> connectionsAndCosts(Vector3Int a)
    {
        Dictionary<Vector3Int, float> result= new Dictionary<Vector3Int, float>();
        foreach (Vector3Int dir in directions)
        {
            foreach (TileAndMovementCost tmc in tiles)
            {
                if (tilemap.GetTile(a+dir)==tmc.tile)
                {
                    if (tmc.movable) result.Add(a + dir, tmc.movementCost);

                }
            }
                
        }
        return result;
    }

    void Awake(){
        tilemap = GameObject.Find("MovableArea").GetComponent<Tilemap>();
    }
    // Start is called before the first frame update
    void Start()
    {
        pathfinder = new Pathfinder<Vector3Int>(DistanceFunc, connectionsAndCosts);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        distanceBetween = Vector2.Distance(UB.player.transform.position, UB.enemy.position);
        if (distanceBetween < 5f){
            UB.animator.SetBool("Moving", true);
            if(!startedMoving){
                var currentCellPos=tilemap.WorldToCell(transform.position);
                var target = tilemap.WorldToCell(UB.player.transform.position);
                target.z = 0;
                pathfinder.GenerateAstarPath(currentCellPos, target, out path);

                if(path.Count == 0){
                    StopAllCoroutines();
                    startedMoving = false;
                    UB.animator.SetBool("Moving", false);
                }
                else{
                    StartCoroutine(Move());
                }
            }
        }
        else{
            UB.animator.SetBool("Moving", false);
        }
    }

    IEnumerator Move()
    {
        startedMoving = true;
        for(int i = 0; i < 3; i++)
        {
            transform.position = tilemap.CellToWorld(path[0]);
            path.RemoveAt(0);
            yield return new WaitForSeconds(stepTime);
        }
        startedMoving = false;
    }
}

