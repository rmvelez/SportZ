using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootballControl : MonoBehaviour
{
    public float playerSpeed; // the speed at which the player moves
    public GameObject camera; // reference to the main camera
    public bool hasWon; // determines if the player has WON
    public bool hasLost; // determines if the player has LOST
    public bool isMoving; // determines if the player is moving
    public bool hasDodged; // determines if the player HAS dodged
    public bool canDodge; // determines if the player CAN dodge

    private AudioSource footballSound;
    public AudioClip crowdSound;
    //public AudioClip cheerSound;
    public AudioClip booSound;

    // Start is called before the first frame update
    void Start()
    {
        hasLost = false; // the player hasn't lost
        hasWon = false; // the player hasn't won either
        isMoving = true; // the player can move
        hasDodged = false; // the player hasn't dodged yet
        canDodge = false; // the player cannot dodge yet

        footballSound = GetComponent<AudioSource>();
        footballSound.PlayOneShot(crowdSound, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        // once the player starts moving
        if (this.transform.position.x >= 0)
        {
            // move the camera with the player
            camera.transform.position = new Vector3(this.transform.position.x, camera.transform.position.y, camera.transform.position.z);
        }

        // once the player is unable to move
        if(isMoving == false)
        {
            // the player stops
            transform.position = new Vector2(transform.position.x, transform.position.y);
        }
        // otherwise
        else
        {
            // the player keeps moving
            transform.Translate(transform.right * playerSpeed * Time.deltaTime);
        }

        // if the player presses X once they ae able to dodge
        if(Input.GetKeyDown(KeyCode.Space) && canDodge == true)
        {
            // move the player away from the rival
            transform.position = new Vector2(transform.position.x + 2f, transform.position.y);
            isMoving = false; // the player can't move anymore
            hasDodged = true; // the player has dodged
        }

        // once the player has dodged
        if (hasDodged == true)
        {
            hasWon = true; // he or she has won
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // if the rival collides with the player
        if(other.gameObject.tag == "Rival")
        {
            hasLost = true; // the player has lost
            isMoving = false; // the player is unable to move
            footballSound.PlayOneShot(booSound);
        }
    }
}
