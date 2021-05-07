using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NineControl : MonoBehaviour
{
    public GameObject arrowRet; // the reticle that the player uses to aim
    public GameObject bowArrow; // the arrows that the player can fire
    public Transform arrowSpawn; // the place where the arrow comes from
    private float clockBetweenShots; // delays the rate at which the player can shoot
    public float startClockBetweenShots; // the initial value of the previous timer

    private float jump; // reference to the jump input in the Unity input manager
    public float jumpForce; // determines how high the player can jump
    public bool grounded; // checks to see if the player is on the ground
    public GameObject groundCheck = null; // used to determine if the previous bool should be true
    private Rigidbody2D myRB; // reference to the rigidbody component on the player

    public float horseSpeed; // the rate at which the player moves
    public bool canMove; // determines if the player can move
    public GameObject camera; // reference to the camera
     
    public bool hasWon; // determines if the player has won
    public bool hasLost; // determines if the player has lost

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>(); // gets the rigidbody component
        canMove = true; // the player can move

        // the player hasn't won or lost
        hasWon = false;
        hasLost = false;
    }

    // Update is called once per frame
    void Update()
    {
        // the arrow reticle moves based on the current position of the mouse
        arrowRet.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));

        // if the player clicks and the timer is less than or equal to zero
        if(Input.GetMouseButton(0) && clockBetweenShots <= 0)
        {
            // fire an arrow and reset the timer
            Instantiate(bowArrow, arrowSpawn.position, bowArrow.transform.rotation);
            clockBetweenShots = startClockBetweenShots;
        }
        // otherwise
        else
        {
            // the timer will decrease until it hits zero
            clockBetweenShots -= Time.deltaTime;
        }
    }

    // Update is called at a fixed rate
    void FixedUpdate()
    {
        // gets the jump input from the unity Input System
        jump = Input.GetAxis("Jump");

        // once the player moves past the center of the screen
        if(transform.position.x >= 0)
        {
            // the camera will follow the player
            camera.transform.position = new Vector3(this.transform.position.x + 2, camera.transform.position.y, camera.transform.position.z);
        }
        
        // if the player is able to move
        if(canMove == true)
        {
            // move towards the finish line
            transform.Translate(transform.right * horseSpeed * Time.deltaTime);
        }
        // otherwise
        else
        {
            // stop moving
            transform.position = new Vector2(transform.position.x, transform.position.y);
        }

        // if the ground check is beneath the floor
        if(Physics2D.Linecast(transform.position,groundCheck.transform.position))
        {
            // the player is on the ground
            grounded = true;
        }
        // otherwise
        else
        {
            // he or she isn't
            grounded = false;
        }

        // once the player is on the ground
        if(grounded == true)
        {
            // make the player jump
            myRB.AddForce(new Vector2(0, jump * jumpForce));
        }
    }

    // checks for collisions with other game objects
    void OnCollisionEnter2D(Collision2D other)
    {
        // if the player hits one of the barriers
        if(other.gameObject.tag == "Barrier")
        {
            // the player can't move and has lost the microgame
            canMove = false;
            hasLost = true;
        }
        // or, if the player hits the end goal
        else if (other.gameObject.tag == "Endgoal")
        {
            // the player can't move, but has won the microgame
            canMove = false;
            hasWon = true;
        }
    }
}
