using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    public float waitDuration; // the time it takes until the prefab is gone

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        waitDuration -= Time.deltaTime; // decreses this timer over real time

        // once the timer hits zero
        if (waitDuration <= 0)
        {
            // destroy the prefab
            Destroy(this.gameObject);
        }
    }
}
