using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseballMove : MonoBehaviour
{
    public float itemSpeed; // the speed of the baseball

    private Transform target; // what its set to move to

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Spot").GetComponent<Transform>(); // sets the target to a spot on screen
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, itemSpeed * Time.deltaTime);

        //throwDelay -= Time.deltaTime; // this decreases until it becomes 0

        // once that happens
        //if (throwDelay <= 0)
        //{
        // move towards the target
        //transform.position = Vector2.MoveTowards(transform.position, target.position, itemSpeed * Time.deltaTime);
        //}
    }
}
