using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RivalMove : MonoBehaviour
{
    public float rivalSpeed; // the speed that the rival moves at

    public bool isMoving; // determines if the rival can move

    // reference to the player character and the script that makes him or her move
    public GameObject footballPlayer; 
    private FootballControl footballControl;

    // Start is called before the first frame update
    void Start()
    {
        isMoving = true; // the rival character can move
        footballControl = footballPlayer.GetComponent<FootballControl>(); // gets the control script from the player
    }

    // Update is called once per frame
    void Update()
    {
        // once the rival character is unable to move
        if (isMoving == false)
        {
            // make him stop
            transform.position = new Vector2(transform.position.x, transform.position.y);
        }
        // otherwise
        else
        {
            // he will move towards the player
            transform.Translate(transform.right * rivalSpeed * Time.deltaTime);
        }

        // if the player has won the microgame
        if (footballControl.hasWon == true)
        {
            isMoving = false; // the rival can't move anymore
        }
    }

    // checks for collisions with other game objects
    void OnCollisionEnter2D(Collision2D other)
    {
        // if the rival collides with the player
        if (other.gameObject.tag == "Player")
        {
            isMoving = false; // he can't move anymore
        }
    }
}
