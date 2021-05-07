using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    // reference to the spawners that the soccer ball can come from
    public Transform spawnerOne;
    public Transform spawnerTwo;
    public Transform spawnerThree;

    private int randSpawn; // used to determine which spawner the soccer ball should come from

    public GameObject soccerBall; // reference to the soccer ball

    private float clockBetweenShots; // the time until the soccer ball appears
    public float startClockBetweenShots; // the initial value of the previous timer

    // Start is called before the first frame update
    void Start()
    {
        clockBetweenShots = startClockBetweenShots; // the timer starts at the initial value
    }

    // Update is called once per frame
    void Update()
    {
        // if the value of randSpawn is 1
        if (randSpawn == 1 && clockBetweenShots <= 0)
        {
            // the soccer ball will appear from the first spawner
            Instantiate(soccerBall, spawnerOne.position, soccerBall.transform.rotation);
            clockBetweenShots = startClockBetweenShots; // the timer resets
        }
        // or if the value is 2
        else if (randSpawn == 2 && clockBetweenShots <= 0)
        {
            // the soccer ball will appear from the second spawner
            Instantiate(soccerBall, spawnerTwo.position, soccerBall.transform.rotation);
            clockBetweenShots = startClockBetweenShots; // the timer resets
        }
        // or if the value if 3
        else if (randSpawn == 3 && clockBetweenShots <= 0)
        {
            // the soccer ball will appear from the third spawner
            Instantiate(soccerBall, spawnerThree.position, soccerBall.transform.rotation);
            clockBetweenShots = startClockBetweenShots; // the timer resets
        }
        // otherwise
        else
        {
            clockBetweenShots -= Time.deltaTime; // the timer decreases until it hits zero
            randSpawn = Random.Range(1, 3); // the var is set to a random number
        }
    }
}
