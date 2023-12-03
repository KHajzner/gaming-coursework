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
        EnemyCount.SetText(killedEnemies + "/" + UB.entityOnSpawn);
        if(UB.entityOnSpawn == 0){
            won.enemiesBeaten += 1;
        }
    }
    public void UpdateCounter(){
        killedEnemies += 1;
        EnemyCount.SetText(killedEnemies + "/" + UB.entityOnSpawn);

        if (killedEnemies == UB.entityOnSpawn){
            won.enemiesBeaten += 1;
        }
    }
}
