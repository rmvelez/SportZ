using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoccerBall : MonoBehaviour
{
    public float soccerBallSpeed; // determines how fast the ball moves

    // the potential targets that the ball can move towards
    // this ensures that the ball can hit the goal at multiple different points
    private Transform targetA;
    private Transform targetB;
    private Transform targetC;

    private float ranTarg; // determines which target the ball moves towards

    // Start is called before the first frame update
    void Start()
    {
        ranTarg = Random.Range(1, 3); // sets this var to a random value

        // sets the targets to three different game objects with the three spot tags
        targetA = GameObject.FindGameObjectWithTag("Spot").GetComponent<Transform>();
        targetB = GameObject.FindGameObjectWithTag("SpotB").GetComponent<Transform>();
        targetC = GameObject.FindGameObjectWithTag("SpotC").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // if the value of this var is 1
        if(ranTarg == 1)
        {
            // the soccer ball will move towards the first target
           transform.position = Vector2.MoveTowards(transform.position, targetA.position, soccerBallSpeed * Time.deltaTime);
        }
        // or if the value is 2
        else if(ranTarg == 2)
        {
            // it moves towards the second target
            transform.position = Vector2.MoveTowards(transform.position, targetB.position, soccerBallSpeed * Time.deltaTime);
        }
        // or if its three
        else if (ranTarg == 3)
        {
            // move towards the third target
            transform.position = Vector2.MoveTowards(transform.position, targetC.position, soccerBallSpeed * Time.deltaTime);
        }
        
    }

    // checks for colliders on other game objects
    void OnCollisionEnter2D(Collision2D other)
    {
        // when the soccer ball hits the goal
        if(other.gameObject.tag == "Goal")
        {
            // its now inactive
            this.gameObject.SetActive(false);
        }
    }
}
