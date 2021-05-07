using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalCheck : MonoBehaviour
{
    public GameObject soccerPlayer; // reference to the player
    private SoccerControl soccerControl; // reference to the script on the player

    // Start is called before the first frame update
    void Start()
    {
        // gets the script from the player
        soccerControl = soccerPlayer.GetComponent<SoccerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // checks for collisions with other game objects
    void OnCollisionEnter2D(Collision2D other)
    {
        // if a soccer ball gets in the goal
        if(other.gameObject.tag == "SoccerBall")
        {
            // the player has lost
            soccerControl.hasLost = true;
        }
    }
}
