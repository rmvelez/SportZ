using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeTennisBall : MonoBehaviour
{
    public float ballSpeed; // the speed at which the ball moves

    public bool movingRight; // determines which direction the ball is moving

    private AudioSource ballSound;
    public AudioClip hitSound;

    // Start is called before the first frame update
    void Start()
    {
        movingRight = false;

        ballSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // if the ball is moving right
        if(movingRight == true)
        {
            // move right towards the rival
            transform.Translate(transform.right * ballSpeed * Time.deltaTime);
        }
        // otherwise
        else
        {
            // move the ball towards the player
            transform.Translate(transform.right * -ballSpeed * Time.deltaTime);
        }
    }

    // checks for collisions with other game objects
    void OnCollisionEnter2D(Collision2D other)
    {
        // if the ball collides with the player racket
        if(other.gameObject.tag == "Racket")
        {
            // it now moves to the right
            movingRight = true;
            ballSound.PlayOneShot(hitSound, 1f);
        }
        // if the ball collides with the rival racket racket
        if (other.gameObject.tag == "OtherRacket")
        {
            // it now moves to the right
            movingRight = false;
            ballSound.PlayOneShot(hitSound, 1f);
        }
        // if the ball hits the rival
        if (other.gameObject.tag == "Rival")
        {
            // kill the rival
            other.gameObject.SetActive(false);
        }
    }
}
