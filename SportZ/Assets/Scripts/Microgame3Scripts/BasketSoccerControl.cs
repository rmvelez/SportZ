using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketSoccerControl : MonoBehaviour
{
    public bool hasWon; // checks to see if the player has won
    public bool hasLost; // checks to see if the player has lost
    private float horizontal; // used for horizontal movement
    public float basketSpeed; // determines the speed of the player
    private Rigidbody2D myRB; // reference to teh rigidbody component of the player
    public Transform kickSpawn; // used to spawn the kick
    public GameObject kickBox; // the hitbox for the kick

    public float startClockBetweenKicks; // the initial value of the time before the player can kick again
    private float clockBetweenKicks; // the amount of time left until the player can kick again

    public GameObject mainCamera;
    private StateManager stateManager;

    private AudioSource basSocSound;
    public AudioClip kickSound;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>(); // gets the rigidbody component from the player
        stateManager = mainCamera.GetComponent<StateManager>();

        basSocSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // if the player presses the K key and they are able to kick
        if(Input.GetKeyDown(KeyCode.Space) && clockBetweenKicks <= 0)
        {
            // allow the player to kick, but reset the timer
            Instantiate(kickBox, kickSpawn.position, kickBox.transform.rotation);
            clockBetweenKicks = startClockBetweenKicks;
            basSocSound.PlayOneShot(kickSound, 1f);
        }
        // otherwise
        else
        {
            // keep the player from kicking until the timer hits zero
            clockBetweenKicks -= Time.deltaTime;
        }

        // if nothing happens before the timer runs out
        if(stateManager.gamePeriod <= 0 && hasWon == false && hasLost == false)
        {
            hasLost = true; // the player will lose
        }
    }

    // FixedUpdate is called at a fixed rate
    void FixedUpdate()
    {
        // gets the horzontal input from the input manager
        horizontal = Input.GetAxis("Horizontal");

        // the player can move with either the arrow keys or the A & D keys
        myRB.velocity = new Vector2(horizontal * basketSpeed, myRB.velocity.y);
        
    }
}
