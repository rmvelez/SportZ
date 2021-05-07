using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeTennisRival : MonoBehaviour
{
    public GameObject dodgeTennisRacket; // reference to the racket that the rival uses
    public Transform racketSpawn; // where the racket is spawned
    private float delayClock; // the time it takes until the rival throws the dodge tennis ball
    public float startDelayClock; // the inial value of the previous timer

    public GameObject dodgeTennisPlayer; // reference to the player character
    private DodgeTennisControl dodgeTennisControl; // reference to the script attached to the player

    // Start is called before the first frame update
    void Start()
    {
        // gets the script from the player
        dodgeTennisControl = dodgeTennisPlayer.GetComponent<DodgeTennisControl>();
    }

    // Update is called once per frame
    void Update()
    {
        // once this timer hits zero
        if (delayClock <= 0)
        {
            // throw the ball at the player and reset the clock
            Instantiate(dodgeTennisRacket, racketSpawn.position, dodgeTennisRacket.transform.rotation);
            delayClock = startDelayClock;
        }
        // otherwise
        else
        {
            // the timer will count down
            delayClock -= Time.deltaTime;
        }
    }

    // checks for collisions with other game objects
    void OnCollisionEnter2D(Collision2D other)
    {
        // if the rival is hit by the dodge tennis ball
        if(other.gameObject.tag == "DodgeTennisBall")
        {
            // the player has won the microgame
            dodgeTennisControl.hasWon = true;
        }
    }
}
