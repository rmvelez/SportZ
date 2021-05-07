using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowControl : MonoBehaviour
{
    public float arrowSpeed; // speed of the arrow

    private Transform target; // where the arrow will go to

    // Start is called before the first frame update
    void Start()
    {
        // target is set to the reticle
        target = GameObject.FindGameObjectWithTag("Spot").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // moves towards the reticle when it apperas
        transform.position = Vector2.MoveTowards(transform.position, target.position, arrowSpeed * Time.deltaTime);
    }

    // checks for collisions with other game objects
    void OnTriggerEnter2D(Collider2D other)
    {
        // if the player hits the reticle
        if(other.gameObject.tag == "Spot")
        {
            // the arrow is gone
            Destroy(this.gameObject);
        }
        // if the arrow hits a target
        if(other.gameObject.tag == "Target")
        {
            // the arrow is gone, but so is the target that it hits
            other.gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
    }
}
