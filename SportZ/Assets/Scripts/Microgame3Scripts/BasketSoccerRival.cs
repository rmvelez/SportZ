using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketSoccerRival : MonoBehaviour
{
    public Transform kickSpawn; //the spawner that spawns the kick
    public GameObject kickMove; // the actual kick attack
    private float clockBetweenKicks; // the time it takes until the enemy can kick again
    public float startClockBetweenKicks; // the initial value of the previous timer

    public float rivalSpeed; // the rate at which the rival moves
    public bool isMoving; // determines if the rival can move
    public bool canKick; // checks to see if the rival can kick
    public GameObject basketSoccerBall; // reference to the basket soccer ball

    public float distanceBetweenObjects;

    private AudioSource rivalSound;
    public AudioClip kickSound;

    // Start is called before the first frame update
    void Start()
    {
        isMoving = true; // the rival is able to move
        canKick = true; // the rival is able to kick

        rivalSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        distanceBetweenObjects = transform.position.x + basketSoccerBall.transform.position.x;

        // once the timer hits zero
        if(clockBetweenKicks <= 0 && distanceBetweenObjects <= 1.5f && canKick == true)
        {
            // the rival kicks, but the timer resets
            Instantiate(kickMove, kickSpawn.position, kickMove.transform.rotation);
            clockBetweenKicks = startClockBetweenKicks;
            rivalSound.PlayOneShot(kickSound, 1f);
        }
        // otherwise
        else
        {
            // the rival can't kick until the timer hits zero
            clockBetweenKicks -= Time.deltaTime;
        }

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
            transform.Translate(transform.right * -rivalSpeed * Time.deltaTime);
        }

        // once the ball is kicked
        if (basketSoccerBall.transform.position.x <= -2)
        {
            isMoving = false; // the rival can no longer move
            canKick = false;
        }
    }
}
