using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    public GameObject item; // reference to the object that is spawned
    private float timeBetweenThrows; // the time it takes before another item is thrown
    public float startTimeBetweenThrows; // the initial value of the fomer timer

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // once the timer hits zero
        if (timeBetweenThrows <= 0)
        {
            Instantiate(item, transform.position, transform.rotation); // spawn another item to be thrown
            timeBetweenThrows = startTimeBetweenThrows; // reset the clock
        }
        else
        {
            timeBetweenThrows -= Time.deltaTime; // the timer will decrease until it hits zero
        }
    }
}
