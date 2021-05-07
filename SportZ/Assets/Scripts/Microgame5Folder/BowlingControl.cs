using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingControl : MonoBehaviour
{
    public bool hasWon; // determines if the player has won
    public bool hasLost; // determines if the player has lost

    public GameObject hockeyStick; // reference to the stick that the player uses to hit the puck
    public Transform stickSpawn; // reference to the spawner for the stick
    private float clockBetweenHits; // the time until the player can hit the puck
    public float startClockBetweenHits; // the initial value of the previous timer
    public bool canHit; // determines if the player can hit the puck

    public GameObject stopSign; // the sign that appears when the player is unable to hit the puck
    public GameObject goSign; // the sign that appears when the player CAN hit the puck
    private float waitClock; // the time until the player CANNOT hit the puck anymore
    public float startWaitClock; // the initial value of the previous timer

    private AudioSource bowlSound;
    public AudioClip hitSound;

    // Start is called before the first frame update
    void Start()
    {
        // when the game starts, the player is unable to hit the puck rght away
        stopSign.SetActive(true);
        goSign.SetActive(false);
        hasWon = false;
        clockBetweenHits = startClockBetweenHits;
        waitClock = startWaitClock;

        bowlSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // when this timer hits zero
       if (clockBetweenHits <= 0)
        {
            canHit = true; // the player can now hit the puck
            goSign.SetActive(true); // the go sign appears
            stopSign.SetActive(false); // the stop sign dissapears
            waitClock -= Time.deltaTime; // runs while the player can hit the puck
            // once this timer hits zero
            if (waitClock <= 0)
            {
                // the first timer resets and the player can't hit the puck anymore
                clockBetweenHits = startClockBetweenHits;
            }
        }
       // otherwise
        else
        {
            canHit = false; // the player can't hit the puck
            goSign.SetActive(false); // this sign is inactive
            stopSign.SetActive(true); // this sign IS active
            clockBetweenHits -= Time.deltaTime; // this timer decreases until it hits zero
            waitClock = startWaitClock; // this timer is reset
        }

       // when the player hits the A key at the right time
       if (Input.GetKeyDown(KeyCode.Space) && canHit == true)
        {
            // the hocky stick appears
            Instantiate(hockeyStick, stickSpawn.position, hockeyStick.transform.rotation);
            bowlSound.PlayOneShot(hitSound, 1f);
        }

       // once the player has won
       if(hasWon == true)
        {
            // the player can't lose
            hasLost = false;
        }
       // otherwise
       else
        {
            // the player will lose when time runs out
            hasLost = true;
        }
    }
}
