using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Won : MonoBehaviour
{
    public GameObject won;
    public int enemiesBeaten = 0;
    int enemiesToBeat = 2;
    // Start is called before the first frame update
    void Start()
    {
        won.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Beaten" + enemiesBeaten);
        if(enemiesBeaten == enemiesToBeat)
        {
            Debug.Log("beaten everyone :o");
            won.gameObject.SetActive(true);
            Time.timeScale = 0;

        }
    }
}
