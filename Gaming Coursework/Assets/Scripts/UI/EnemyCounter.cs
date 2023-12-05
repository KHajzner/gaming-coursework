using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyCounter : MonoBehaviour
{
    public TMP_Text EnemyCount;
    public int killedEnemies;
    public Won won;
    public int enemyOnStart = 0;
    public string enemyType;

    void Start(){
        Invoke("CreateCounter", 0.1f);
    }
    public void CreateCounter(){
        foreach(GameObject enemyObject in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (enemyObject.name == enemyType + "(Clone)"){
                enemyOnStart++;
            }
        }
        EnemyCount.SetText(killedEnemies + "/" + enemyOnStart);
        if(enemyOnStart == 0){
            won.enemiesBeaten += 1;
        }
    }
    public void UpdateCounter(){
        killedEnemies += 1;
        EnemyCount.SetText(killedEnemies + "/" + enemyOnStart);

        if (killedEnemies == enemyOnStart){
            won.enemiesBeaten += 1;
        }
    }
}
