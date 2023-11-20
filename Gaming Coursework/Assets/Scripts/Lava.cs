using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Lava : MonoBehaviour
{

    public float delay;
    float delayBetween = 0.5f;

    void Start()
    {
        StartCoroutine(MoveLava(20, delay));
        StartCoroutine(MoveLava(0, delay+50));
    }
    IEnumerator MoveLava(float moveBy, float delay)
    {
        yield return new WaitForSeconds(delay);
        transform.position = new Vector3(moveBy, 0, 0);
        for (int i = 0; i<5; i++)
        {
            yield return new WaitForSeconds(delayBetween);
            transform.position = new Vector3(0, 0, 0);
            yield return new WaitForSeconds(delayBetween);
            transform.position = new Vector3(moveBy, 0, 0);
        }
    }
}