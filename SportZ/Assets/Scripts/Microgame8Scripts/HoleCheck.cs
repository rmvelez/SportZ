using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleCheck : MonoBehaviour
{
    public GameObject golfItPlayer; // reference to the golf player
    private GolfItControl golfItControl; // reference to the script on the player

    // Start is called before the first frame update
    void Start()
    {
        // gets the script from the player
        golfItControl = golfItPlayer.GetComponent<GolfItControl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // checks for collisions with other game objects
    void OnCollisionEnter2D(Collision2D other)
    {
        // if the golf it ball lands in the hole
        if(other.gameObject.tag == "GolfItBall")
        {
            // the player has lost this microgame
            golfItControl.hasLost = true;
        }
    }
}
