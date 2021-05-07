using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxingPunch : MonoBehaviour
{
    private float durationClock; // how long the punch will last for
    public float startDurationClock; // resets the previous timer

    public GameObject boxingPlayer; // reference to the player
    private BoxingControl boxingControl; // reference to the boxing script on the player

    // Start is called before the first frame update
    void Start()
    {
        boxingControl = boxingPlayer.GetComponent<BoxingControl>(); // gets this script from the player
    }

    // Update is called once per frame
    void Update()
    {
        // when the timer hits zero
        if(durationClock <= 0)
        {
            // disable this punch and reset the timer
            this.gameObject.SetActive(false);
            durationClock = startDurationClock;
        }
        // otherwise
        else
        {
            // decrease this timer until it hits zero
            durationClock -= Time.deltaTime;
        }
    }

    // used to check for collisions with other game objects
    void OnCollisionEnter2D(Collision2D other)
    {
        // when this puch hits the punching bag
        if(other.gameObject.tag == "PunchingBag")
        {
            // push the player back a bit, but increase the hit count
            boxingPlayer.transform.position = new Vector2(boxingPlayer.transform.position.x - 0.2f, boxingPlayer.transform.position.y);
            boxingControl.hitCount++;
        }
    }
}
