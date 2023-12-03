using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyCounter : MonoBehaviour
{
    public TMP_Text EnemyCount;
    public UniversalBehaviour UB;
    public int killedEnemies;
    public Won won;
    // Start is called before the first frame update
    void Start()
    {
        EnemyCount.SetText(killedEnemies + "/" + UB.enemyOnSpawn);
        if(UB.enemyOnSpawn == 0){
            won.enemiesBeaten += 1;
        }
    }
    public void UpdateCounter(){
        killedEnemies += 1;
        EnemyCount.SetText(killedEnemies + "/" + UB.enemyOnSpawn);

        if (killedEnemies == UB.enemyOnSpawn){
            won.enemiesBeaten += 1;
        }
    }
}
