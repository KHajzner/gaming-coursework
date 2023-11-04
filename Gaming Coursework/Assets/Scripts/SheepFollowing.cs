using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepFollowing : MonoBehaviour
{
    // Start is called before the first frame update

    public Rigidbody2D player;
    public Rigidbody2D sheep;
    public float distanceBetween;
    public float speed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(player.transform.position, sheep.position) < distanceBetween){
            sheep.position = Vector2.MoveTowards(sheep.position, player.position, speed * Time.deltaTime);
        }
    }
}
