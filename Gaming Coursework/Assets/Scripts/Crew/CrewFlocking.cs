using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class CrewFlocking : MonoBehaviour
{    
    public float speed;
    public Rigidbody2D crew;
    public Animator animator;
    bool bounced = false;
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

    void Start()
    {
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
    void Update()
    {
        AlignWithOthers();
        MoveToCenter();
        AvoidEnemies();
        if(!bounced){
            transform.Translate(direction * (speed * Time.deltaTime));
        }
        SwitchRotation(direction * speed * Time.fixedDeltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Obstacles"){
            crew.AddForce(collision.contacts[0].normal * 50f);
            bounced = true;
            Invoke("RemoveBounce", 1f);
        }
    }
    void RemoveBounce()
    {
        bounced = false;
    }

    //Move the crew to the center of their group
    void MoveToCenter()
    {

        Vector2 positionSum = transform.position;
        int count = 0;
        foreach (CrewFlocking boid in boidsInScene){
            float distance = Vector2.Distance(boid.transform.position, transform.position);
            if (distance <= localBoidsDistance){
                positionSum += (Vector2)boid.transform.position;
                count++;
            }
        }

        if (count == 0){
            return;
        }
        Vector2 positionAverage = (positionSum / count);
        positionAverage = positionAverage.normalized;
        Vector2 faceDirection = (positionAverage - (Vector2) transform.position).normalized;
        float deltaTimeStrength = moveToCenterStrength * Time.deltaTime;
        direction=direction+deltaTimeStrength*faceDirection/(deltaTimeStrength+1);
        direction = direction.normalized;
    }

    //Code to make crew avoid nearby enemies
    void AvoidEnemies()
    {
        Vector2 faceAwayDirection = Vector2.zero;
        foreach (UniversalBehaviour enemy in enemiesInScene){
            if(enemy != null){
                float distance = Vector2.Distance(enemy.transform.position, transform.position);
                if (distance <= collisionAvoidCheckDistance){
                    faceAwayDirection = faceAwayDirection+ (Vector2)(transform.position - enemy.transform.position);
                }
            }
        }
        faceAwayDirection = faceAwayDirection.normalized;
        direction=direction+avoidOtherStrength*faceAwayDirection/(avoidOtherStrength +1);
        direction = direction.normalized;
    }

    //Code to align crew with other crew around them
    void AlignWithOthers()
    {
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
        float deltaTimeStrength = alignWithOthersStrength * Time.deltaTime;
        direction=direction+deltaTimeStrength*directionAverage/(deltaTimeStrength+1);
        direction = direction.normalized;
    }

    //Rotate sprite so it faces the direction of movement
    void SwitchRotation(Vector2 direction)
    {
        if ((direction.x > 0 && !facesRight) || (direction.x < 0 && facesRight)){
            facesRight = !facesRight;
            Vector3 face = transform.localScale;
            face.x *= -1;
            transform.localScale = face;
        }
    }
}