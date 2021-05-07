using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeTennisControl : MonoBehaviour
{
    public bool hasWon; // determines if the player has won
    public bool hasLost; // determines if the player has lost

    public GameObject playerRacket; // the racket the player uses to hit the dodge ball
    public Transform racketSpawn; // the place where the racket appears
    private float clockBetweenHits; // the time until the racket can be used
    public float startClockBetweenHits; // the initial value of the previous timer

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // when the player presses O after the timer hits zero
        if(Input.GetKeyDown(KeyCode.Space) && clockBetweenHits <= 0)
        {
            // they use the racket and the timer resets
            Instantiate(playerRacket, racketSpawn.position, playerRacket.transform.rotation);
            clockBetweenHits = startClockBetweenHits;
        }
        // otherwise
        else
        {
            // the timer decreases until it hits zero
            clockBetweenHits -= Time.deltaTime;
        }
    }

    // checks for collisions with other game objects
    void OnCollisionEnter2D(Collision2D other)
    {
        // if the player is hit by the dodge tennis ball
        if(other.gameObject.tag == "DodgeTennisBall")
        {
            // disable the player and have him or her lose
            this.gameObject.SetActive(false);
            hasLost = true;
        }
    }
}
