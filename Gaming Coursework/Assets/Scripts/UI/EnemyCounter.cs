using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyCounter : MonoBehaviour
{
    public TMP_Text EnemyCount;
    public GameObject enemyPrefab;
    public int killedEnemies;
    public Won won;
    // Start is called before the first frame update
    void Start()
    {
        EnemyCount.SetText(killedEnemies + "/" + enemyPrefab.GetComponent<UniversalBehaviour>().enemyOnSpawn);
        if(enemyPrefab.GetComponent<UniversalBehaviour>().enemyOnSpawn == 0){
            won.enemiesBeaten += 1;
        }
    }
    public void UpdateCounter(){
        killedEnemies += 1;
        EnemyCount.SetText(killedEnemies + "/" + enemyPrefab.GetComponent<UniversalBehaviour>().enemyOnSpawn);

        if (killedEnemies == enemyPrefab.GetComponent<UniversalBehaviour>().enemyOnSpawn){
            won.enemiesBeaten += 1;
        }
    }
}
