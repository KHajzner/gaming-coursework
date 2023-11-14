using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barn : MonoBehaviour
{

    public int sheepCount;
    // Start is called before the first frame update
    void Start()
    {
        sheepCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (sheepCount == 3){
            Debug.Log("You Won!");
        }
    }
}
