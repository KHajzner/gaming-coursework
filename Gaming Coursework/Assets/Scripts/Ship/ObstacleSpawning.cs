using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawning : MonoBehaviour
{
    int numObstacle1;
    int numObstacle2;
    int numObstacle3;
    public GameObject obstacle1;
    public GameObject obstacle2;
    public GameObject obstacle3;
    public List<ObstacleBehaviour> obstaclesInScene;
    void Start()
    {
        numObstacle1 = Random.Range(2,6);
        numObstacle2 = Random.Range(2,6);
        numObstacle3 = Random.Range(2,6);

        for(int i = 0; i < numObstacle1; i++){
            Vector3 chosenSpot = new Vector3(Random.Range(-26,26), Random.Range(-24,22), 0);
            Instantiate(obstacle1,chosenSpot, Quaternion.identity);
        }
        for(int i = 0; i < numObstacle2; i++){
            Vector3 chosenSpot = new Vector3(Random.Range(-26,26), Random.Range(-24,22), 0);
            Instantiate(obstacle2,chosenSpot, Quaternion.identity);
        }
        for(int i = 0; i < numObstacle3; i++){
            Vector3 chosenSpot = new Vector3(Random.Range(-26,26), Random.Range(-24,22), 0);
            Instantiate(obstacle3,chosenSpot, Quaternion.identity);
        }
        foreach(GameObject obstacles in GameObject.FindGameObjectsWithTag("Obstacles")){
            var obstacle = obstacles.GetComponent<ObstacleBehaviour>();
            obstaclesInScene.Add(obstacle);
        }
        SetTreasure();
    }

    void SetTreasure(){
        int length = obstaclesInScene.Count;
        int randomObstacle = Random.Range(0, length);
        obstaclesInScene[randomObstacle].isTreasure = true;
    }
}
