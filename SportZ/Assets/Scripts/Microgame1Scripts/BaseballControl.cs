using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseballControl : MonoBehaviour
{
    public bool hasWon; // checks to see if the player has won
    public bool hasLost; // checks to see if the player has lost

    // references to the objects that spawns the junk
    public GameObject junkSpawnA;
    public GameObject junkSpawnB;
    public GameObject junkSpawnC;

    // reference to the spawner that spawns the balls
    public GameObject ballSpawn;

    private float horizontal;
    private float vertical;
    private Rigidbody2D myRB;
    public float playSpeed;

    //reference to the camera and the script attached to it
    public GameObject mainCamera;
    private StateManager stateManager;

    private AudioSource baseballSound;
    public AudioClip booSound;
    public AudioClip cheerSound;

    // Start is called before the first frame update
    void Start()
    {
        // both conditions are false when the game starts
        hasWon = false; 
        hasLost = false;

        myRB = GetComponent<Rigidbody2D>();
        stateManager = mainCamera.GetComponent<StateManager>();
        baseballSound = GetComponent<AudioSource>();

        baseballSound.PlayOneShot(booSound, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        // the the timer is less than or equal to zero, and the player hasn't won or lost yet
        if (stateManager.gamePeriod <= 0 && hasWon == false && hasLost == false)
        {
            // the player will lose when the timer runs out
            hasLost = true;
        }
    }

    void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        // the player character moves with either the arrow keys or WASD
        myRB.velocity = new Vector2(horizontal * playSpeed, vertical * playSpeed);
        //transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y, 10));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // if the player catches the baseball
        if(other.gameObject.tag == "Baseball")
        {
            // the player has won the Baseball Microgame
            Destroy(other.gameObject);
           
            if(hasLost == false)
            {
                hasWon = true;
                junkSpawnA.SetActive(false);
                junkSpawnB.SetActive(false);
                junkSpawnC.SetActive(false);
                ballSpawn.SetActive(false);
                baseballSound.PlayOneShot(cheerSound, 1f);
            }
        }
        // or, if the player cataches any junk
        if(other.gameObject.tag == "Junk")
        {
            // the player has lost it
            Destroy(other.gameObject);

            if (hasWon == false)
            {
                hasLost = true;
                ballSpawn.SetActive(false);
                baseballSound.PlayOneShot(booSound, 1f);
            }
        }
    }
}
