using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyCounter : MonoBehaviour
{
    public TMP_Text EnemyCount;
    public UniversalBehaviour UB;
    public int killedEnemies;
    // Start is called before the first frame update
    void Start()
    {
        EnemyCount.SetText(killedEnemies + "/" + UB.enemyOnSpawn);
    }
    public void UpdateCounter(){
        EnemyCount.SetText(killedEnemies + "/" + UB.enemyOnSpawn);
    }
}
