using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HockeyPuck : MonoBehaviour
{
    public float puckSpeed; // the speed at which the puc moves

    public GameObject bowlingPlayer; // refernce to the player
    private BowlingControl bowlingControl; // reference to the script that the player has

    public GameObject camera; // reference to the camera

    public bool isMoving; // determines if the puck can move

    // Start is called before the first frame update
    void Start()
    {
        bowlingControl = bowlingPlayer.GetComponent<BowlingControl>(); // gets the script from the player
    }

    // Update is called once per frame
    void Update()
    {
        // if the puck is able to move
        if (isMoving == true)
        {
            // move it towards the pins
            transform.Translate(transform.right * puckSpeed * Time.deltaTime);
        }

        // once the puck goes beyond the starting area
        if (transform.position.x >= 6)
        {
            // have the camera follow it
            camera.transform.position = new Vector3(this.transform.position.x, camera.transform.position.y, camera.transform.position.z);
        }
    }

    // checks for collisions with other game objects
    void OnCollisionEnter2D(Collision2D other)
    {
        // when hit by the mouth of the hockey stick
        if(other.gameObject.tag == "StickMouth")
        {
            // the puck is now able to move
            isMoving = true;
        }
        // when the puck hits the bowling pins
        if(other.gameObject.tag == "BowlingPin")
        {
            // eliminate the pins and puck and the player has won
            other.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
            bowlingControl.hasWon = true;
        }
    }
}
