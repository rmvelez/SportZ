using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketSoccerBall : MonoBehaviour
{
    public float soccerSpeed; // the speed of the soccer ball

    private Rigidbody2D thisRB; // reference to the rigidbody of the ball

    public bool isFlying; // checks to see if its flying towards the opponent's hoop
    public bool isFlyingBackwards; // checks to see if its flying towards the player's goal

    public GameObject basketSoccerPlayer; // reference to the player character
    private BasketSoccerControl basketSoccerControl; // reference to the script that's on the player

    public GameObject camera; // reference to the camera

    // Start is called before the first frame update
    void Start()
    {
        thisRB = GetComponent<Rigidbody2D>(); // sets the variable to the rigidbody component on the player
        basketSoccerControl = basketSoccerPlayer.GetComponent<BasketSoccerControl>(); // gets the script from the player

        //ballSound = GetComponent<AudioSource>();
        
        // the ball is not flying in either direction
        isFlying = false;
        isFlyingBackwards = false;
    }

    // Update is called once per frame
    void Update()
    {
        // if the ball is flying forwards
        if (isFlying == true)
        {
            // move it towards the opponent's hoop
            thisRB.velocity = new Vector2(thisRB.velocity.x + soccerSpeed, thisRB.velocity.y + soccerSpeed);
        }
        // if the ball is flying backwards
        else if (isFlyingBackwards == true)
        {
            // move it towards the player's hoop
            thisRB.velocity = new Vector2(thisRB.velocity.x - soccerSpeed, thisRB.velocity.y + soccerSpeed);
        }
        // otherwise
        else
        {
            //the ball is stationary while on the ground
            thisRB.velocity = new Vector2(thisRB.velocity.x, thisRB.velocity.y - 2);
        }

        // if the ball moves beyond the bounds of the camera
        if (transform.position.x >= 1 || transform.position.x <= -1)
        {
            // have it follow the ball
            camera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, camera.transform.position.z);
        }
    }

    // checks for collisions with other game objects
    void OnCollisionEnter2D (Collision2D other)
    {
        // if the ball was kicked by the player
        if (other.gameObject.tag == "Kick")
        {
                // move it slightly from the player and allow it to fly forwards
                transform.position = new Vector2(transform.position.x + 1, transform.position.y + 4);
                isFlying = true;
        }
        // if the ball is kicked by the opponent
        if(other.gameObject.tag == "OtherKick")
        {
                // move it slightly from the opponent and allow it to fly backwards
                transform.position = new Vector2(transform.position.x - 1, transform.position.y + 4);
                isFlyingBackwards = true;
        }
        // if the ball hits the bar of the opponent's hoop
        if(other.gameObject.tag == "Bar")
        {
            // the player wins the microgame
            basketSoccerControl.hasWon = true;
            this.gameObject.SetActive(false);
        }
        // if the ball hits the bar of the player's hoop
        if(other.gameObject.tag == "OtherBar")
        {
            //the player loses the microgame
            basketSoccerControl.hasLost = true;
            this.gameObject.SetActive(false);
        }
    }
}
