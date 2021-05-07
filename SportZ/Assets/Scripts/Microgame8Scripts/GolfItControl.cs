using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfItControl : MonoBehaviour
{
    // these bools determine if the player has won or lost
    public bool hasWon;
    public bool hasLost;

    public GameObject golfItBall; // reference to the golf it ball that the player must hit
    public GameObject warningSign; // reference to the warning sign that appears before the ball does

    public Transform golfClubSpawn; // reference to where the golf it club appears
    public GameObject golfClub; // reference to the golf it club
    private float clockBetweenSwings; // the time it takes before the player can do a swing
    public float startClockBetweenSwings; // the reset value for the previous timer

    public float clockHold; // determines how long certain objects appear

    // Start is called before the first frame update
    void Start()
    {
        // when the microgame begins, the objects are inactive and player hasn't won or lost
        hasWon = false;
        hasLost = false;
        golfItBall.SetActive(false);
        warningSign.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // this timer decreases in real time and runs concurrently with the microgame timer on the state manager script
        clockHold -= Time.deltaTime;

        // if there are less than 5 seconds left
        if(clockHold <= 5)
        {
            // the warning sign will appear
            warningSign.SetActive(true);
        }
        // if there are less than 3 seconds left
        if(clockHold <= 3)
        {
            golfItBall.SetActive(true);
        }

        // if the player press Y and this timer is less than or equal to zero
        if(Input.GetKeyDown(KeyCode.Space) && clockBetweenSwings <= 0)
        {
            // the player swings the glof club and this timer resets to its initial value
            Instantiate(golfClub, golfClubSpawn.position, golfClub.transform.rotation);
            clockBetweenSwings = startClockBetweenSwings;
        }
        // otherwise
        else
        {
            // this timer will decrease until it is zero
            clockBetweenSwings -= Time.deltaTime;
        }
    }
}
