using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMove : MonoBehaviour
{
    public float itemSpeed; // the speed that the junk moves

    // where the junk is set to move to
    private Transform targetA; 
    private Transform targetB;
    private Transform targetC;

    private int randTar; // determines which target to move towards

    private float durationClock;

    // Start is called before the first frame update
    void Start()
    {
        //sets it to move to the correct target
        targetA = GameObject.FindGameObjectWithTag("Spot").GetComponent<Transform>();
        targetB = GameObject.FindGameObjectWithTag("SpotB").GetComponent<Transform>();
        targetC = GameObject.FindGameObjectWithTag("SpotC").GetComponent<Transform>();

        randTar = Random.Range(1, 3);

        durationClock = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        // when the value is one
        if (randTar == 1)
        {
            // move towards the first target
            transform.position = Vector2.MoveTowards(transform.position, targetA.position, itemSpeed * Time.deltaTime);
        }
        // when the value is two
        else if (randTar == 2)
        {
            // move towards the second target
            transform.position = Vector2.MoveTowards(transform.position, targetB.position, itemSpeed * Time.deltaTime);
        }
        // when the value is three
        else if(randTar == 3)
        {
            // move towards the third target
            transform.position = Vector2.MoveTowards(transform.position, targetC.position, itemSpeed * Time.deltaTime);
        }

        durationClock -= Time.deltaTime;

        if(durationClock <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    //chceks for collisions with other game objects
    void OnCollisionEnter2D(Collision2D other)
    {
        // once the item reaches its destination
        if(other.gameObject.tag == "Spot" || other.gameObject.tag == "SpotB" || other.gameObject.tag == "SpotC")
        {
            // DESTROY IT!!!
            Destroy(this.gameObject);
        }
    }
}
