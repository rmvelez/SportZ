using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoccerControl : MonoBehaviour
{
    public bool hasWon; // determines if the player has won
    public bool hasLost; // determines if the player has lost
    private float vertical; // reference to the vertical input from the Unity Input System
    public float soccerSpeed; // the speed at which the player moves
    private Rigidbody2D myRB; // reference to the rigidbody component on the player
    public GameObject ballSpawner; // reference to the object that spawns the soccer ball

    private AudioSource soccerSound;
    public AudioClip blockSound;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>(); // gets the rigidbody component
        soccerSound = GetComponent<AudioSource>();

        // the player hasn't won or lost
        hasWon = false;
        hasLost = false;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    //This is called at a fixed rate
    void FixedUpdate()
    {
        // gets the vertical input from the Input System
        vertical = Input.GetAxis("Vertical");

        // the player can move by using the up and down arrow keys, or by pressing w or s
        myRB.velocity = new Vector2(myRB.velocity.x, vertical * soccerSpeed);
    }

    // checks for collisions with other game objects
    void OnCollisionEnter2D(Collision2D other)
    {
        // if the player blocks the soccer ball
        if(other.gameObject.tag == "SoccerBall")
        {
            // the player has won this microgame
            hasWon = true;
            other.gameObject.SetActive(false);
            ballSpawner.SetActive(false);
            soccerSound.PlayOneShot(blockSound, 1f);
        }
    }
}
