using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfItBall : MonoBehaviour
{
    public float golfSpeed; // controls the speed of the golf ball

    // references to the player and the script on it
    public GameObject golfPlayer; 
    private GolfItControl golfItControl;

    private AudioSource golfSound;
    public AudioClip hitSound;

    // Start is called before the first frame update
    void Start()
    {
        // gets the script from the player
        golfItControl = golfPlayer.GetComponent<GolfItControl>();
        golfSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // checks for collisions with other game objects
    void OnCollisionEnter2D(Collision2D other)
    {
        // if the golf it ball gets hit by the golf it club
        if(other.gameObject.tag == "GolfClubHead")
        {
            // the ball is moved from the hole, moves away, and the player wins the microgame
            transform.position = new Vector2(transform.position.x - 2, transform.position.y);
            transform.Translate(transform.right * -golfSpeed * Time.deltaTime);
            golfSound.PlayOneShot(hitSound, 1f);
            golfItControl.hasWon = true;
        }
    }
}
