using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Lava : MonoBehaviour
{

    public float delay;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveLava(20, delay));
        StartCoroutine(MoveLava(-20, delay+20));
    }
    IEnumerator MoveLava(float moveBy, float delay)
    {
        yield return new WaitForSeconds(delay);
        transform.position = new Vector3(moveBy, 0, 0);
    }
}