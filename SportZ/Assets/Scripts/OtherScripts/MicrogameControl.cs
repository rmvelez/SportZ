using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MicrogameControl : MonoBehaviour
{
    public GameObject messageScreen; // the screen that appears before the microgame starts
    public GameObject microGame; // the actual microgame itself

    public float waitPeriod; // how long it takes before the microgame starts

    public TMP_Text clockText;

    // Start is called before the first frame update
    void Start()
    {
        // the player will see the message screen first
        messageScreen.SetActive(true);
        microGame.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // once the wait period timer is 0
        if (waitPeriod <= 0)
        {
            // START THE MICROGAME!!!
            messageScreen.SetActive(false);
            microGame.SetActive(true);
        }
        // otherwise
        else
        {
            // decrease the value of this timer until it hits zero
            waitPeriod -= Time.deltaTime;
            clockText.text = Mathf.Round(waitPeriod).ToString();
        }
    }
}
