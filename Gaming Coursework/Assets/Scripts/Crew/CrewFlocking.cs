using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class CrewFlocking : MonoBehaviour
{    
    public float speed;
    bool bounced = false;
    public Rigidbody2D crew;
    public Animator animator;
    bool facesRight = false;
    Vector2 direction;

    //Flocking algorithms
    public List<CrewFlocking> boidsInScene;
    public List<UniversalBehaviour> enemiesInScene;
    public float moveToCenterStrength;//factor by which boid will try toward center Higher it is, higher the turn rate to move to the center
    public float localBoidsDistance;//effective distance to calculate the center
    public float avoidOtherStrength;//factor by which boid will try to avoid each other. Higher it is, higher the turn rate to avoid other.
    public float collisionAvoidCheckDistance;//distance of nearby boids to avoid collision
    public float alignWithOthersStrength;//factor determining turn rate to align with other boids
    public float alignmentCheckDistance;//distance up to which alignment of boids will be checked. Boids with greater distance than this will be ignored

    void Start(){
        foreach(GameObject crew in GameObject.FindGameObjectsWithTag("Crew")){
            var flocking = crew.GetComponent<CrewFlocking>();
            boidsInScene.Add(flocking);
        }
        foreach(GameObject enemies in GameObject.FindGameObjectsWithTag("Enemy")){
            var enemy = enemies.GetComponent<UniversalBehaviour>();
            enemiesInScene.Add(enemy);
        }
        animator.SetBool("Moving", true);
    }
    void Update(){
        AlignWithOthers();
        MoveToCenter();
        AvoidEnemies();
        if(!bounced){
            transform.Translate(direction * (speed * Time.deltaTime));
        }
        SwitchRotation(direction * speed * Time.fixedDeltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision){
        if(collision.collider.tag == "Obstacles"){
            crew.AddForce(collision.contacts[0].normal * 50f);
            bounced = true;
            Invoke("RemoveBounce", 1f);
        }
    }
    void RemoveBounce(){
        bounced = false;
    }
    void MoveToCenter(){

        Vector2 positionSum = transform.position;//calculate sum of position of nearby boids and get count of boid
        int count = 0;

        foreach (CrewFlocking boid in boidsInScene)
        {
            float distance = Vector2.Distance(boid.transform.position, transform.position);
            if (distance <= localBoidsDistance){
                positionSum += (Vector2)boid.transform.position;
                count++;
            }
        }

        if (count == 0){
            return;
        }

        //get average position of boids
        Vector2 positionAverage = positionSum / count;
        positionAverage = positionAverage.normalized;
        Vector2 faceDirection = (positionAverage - (Vector2) transform.position).normalized;

        //move boid toward center
        float deltaTimeStrength = moveToCenterStrength * Time.deltaTime;
        direction=direction+deltaTimeStrength*faceDirection/(deltaTimeStrength+1);
        direction = direction.normalized;
    }

    void AvoidEnemies(){

        Vector2 faceAwayDirection = Vector2.zero;//this is a vector that will hold direction away from near boid so we can steer to it to avoid the collision.

        //we need to iterate through all boid
        foreach (UniversalBehaviour enemy in enemiesInScene){
            float distance = Vector2.Distance(enemy.transform.position, transform.position);
            //if the distance is within range calculate away vector from it and subtract from away direction.
            if (distance <= collisionAvoidCheckDistance){
                faceAwayDirection = faceAwayDirection+ (Vector2)(transform.position - enemy.transform.position);
            }
        }

        faceAwayDirection = faceAwayDirection.normalized;//we need to normalize it so we are only getting direction

        direction=direction+avoidOtherStrength*faceAwayDirection/(avoidOtherStrength +1);
        direction = direction.normalized;
    }
    void AlignWithOthers(){
        //we will need to find average direction of all nearby boids
        Vector2 directionSum = Vector3.zero;
        int count = 0;

        foreach (CrewFlocking boid in boidsInScene){
            float distance = Vector2.Distance(boid.transform.position, transform.position);
            if (distance <= alignmentCheckDistance){
                directionSum += boid.direction;
                count++;
            }
        }

        Vector2 directionAverage = directionSum / count;
        directionAverage = directionAverage.normalized;

        //now add this direction to direction vector to steer towards it
        float deltaTimeStrength = alignWithOthersStrength * Time.deltaTime;
        direction=direction+deltaTimeStrength*directionAverage/(deltaTimeStrength+1);
        direction = direction.normalized;
    }

    void SwitchRotation(Vector2 direction){
        if ((direction.x > 0 && !facesRight) || (direction.x < 0 && facesRight)){
            facesRight = !facesRight;
            Vector3 face = transform.localScale;
            face.x *= -1;
            transform.localScale = face;
        }
    }
}