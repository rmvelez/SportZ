using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockoutPunch : MonoBehaviour
{
    private float durationClock; // tracks how long the punch should be active
    public float startDurationClock; // the initial value of the previous timer

    public GameObject boxingPlayer; // reference to the player
    private BoxingControl boxingControl; // reference to the boxing script on the player

    // Start is called before the first frame update
    void Start()
    {
        boxingControl = boxingPlayer.GetComponent<BoxingControl>(); // gets the script from the player
    }

    // Update is called once per frame
    void Update()
    {
        // when this timer has run out
        if (durationClock <= 0)
        {
            // disable the knockout punch and reset the clock
            this.gameObject.SetActive(false);
            durationClock = startDurationClock;
        }
        // otherwise
        else
        {
            // decrease the value of this timer until it hits zero
            durationClock -= Time.deltaTime;
        }
    }

    // used to check for collisions with other game objects
    void OnCollisionEnter2D(Collision2D other)
    {
        // when this punch hits the punching bag
        if (other.gameObject.tag == "PunchingBag")
        {
            // destroy the punching bag and give the player the W
            other.gameObject.SetActive(false);
            boxingControl.hasWon = true;
        }
    }
}
