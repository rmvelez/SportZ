using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxingControl : MonoBehaviour
{
    public float boxingSpeed; // the rate at which the boxer moves
    private float horizontal; // the input that lets the player move horizontally
    private Rigidbody2D myRB; // reference to the rigidbody component on the player

    public GameObject normalPunch; // reference to the normal punch that the player can throw
    public GameObject knockoutPunch; // reference to the knockout punch that the player can also throw
    private float clockBetweenPunches; // the time it takes until the player can throw another punch
    public float startClockBetweenPunches; // the initial value of the punch timer

    public int hitCount; // the number of hits that the player has made towards the punching bag
    public bool hasWon; // indicates if the player has won
    public bool hasLost; // indicates if the player has lost

    public GameObject goSign; // reference to the sign that appears when the player can do a knockout punch

    private AudioSource boxingSound;
    public AudioClip lightPunchSound;
    public AudioClip heavyPunchSound;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>(); // gets the rigidbody component
        hitCount = 0; // the player hasn't thrown any hits yet
        normalPunch.SetActive(false); // both punch attacks are disabled
        knockoutPunch.SetActive(false);
        hasWon = false; // the player hasn't won yet
        goSign.SetActive(false); // this sign is inactive

        boxingSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        normalPunch.transform.position = new Vector2(transform.position.x + 3, -2);
        knockoutPunch.transform.position = new Vector2(transform.position.x + 4 , -2);

        // if the player presses P before landing 3 hits
        if (Input.GetKeyDown(KeyCode.Space) && clockBetweenPunches <= 0 && hitCount <= 2)
        {
            // they will throw a normal punch and reset the timer
            normalPunch.SetActive(true);
            clockBetweenPunches = startClockBetweenPunches;
            boxingSound.PlayOneShot(lightPunchSound, 1f);
        }
        // if the player made 3 or more hits when he or she presses P
        else if(Input.GetKeyDown(KeyCode.Space) && clockBetweenPunches <= 0 && hitCount >= 2)
        {
            // let the player do a knockout punch and reset the timer
            knockoutPunch.SetActive(true);
            clockBetweenPunches = startClockBetweenPunches;
            boxingSound.PlayOneShot(heavyPunchSound, 1f);
        }
        // otherwise
        else
        {
            // decrease this value until the player can punch again
            clockBetweenPunches -= Time.deltaTime;
        }

        // once the hit count is greater than or equal to three
        if (hitCount >= 3)
        {
            // show the go sign
            goSign.SetActive(true);
        }

        // if the player has won the microgame
        if(hasWon == true)
        {
            hasLost = false; // he or she didn't lose
        }
        // otherwise
        else
        {
            // he or she will lose when the timer runs out
            hasLost = true;
        }
    }

    // similar to update, but at a fixed rate
    void FixedUpdate()
    {
        // gets the horizontal input from the input system
        horizontal = Input.GetAxis("Horizontal");

        // the player can move left or right
        myRB.velocity = new Vector2(horizontal * boxingSpeed, myRB.velocity.y);
    }
}
