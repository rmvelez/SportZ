using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootballManagement : MonoBehaviour
{
    public float gameClock; // timer used to enable microgame events

    public GameObject warningSign; // sign that warns the player when the rival is coming

    public GameObject footballPlayer; // reference to the player character
    private FootballControl footballControl; // reference to the script that controls the player character

    public GameObject rivalPlayer; // reference to the rival player

    //private AudioSource controlSound;
    //public AudioClip warningSound;

    // Start is called before the first frame update
    void Start()
    {
        // when the game starts, the non-player objects are disabled and the script from the player is gained
        warningSign.SetActive(false);
        rivalPlayer.SetActive(false);
        footballControl = footballPlayer.GetComponent<FootballControl>();

        //controlSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        gameClock -= Time.deltaTime; // decreases in real time. Coencides with microgame timer on scene management script 

        // when there is between 5 & 4 seconds left
        if (gameClock <= 5 && gameClock >= 4)
        {
            // show the warning sign and have it set to sightly off from the player's position
            warningSign.SetActive(true);
            //controlSound.PlayOneShot(warningSound, 1f);
            warningSign.transform.position = new Vector2(footballPlayer.transform.position.x - 3, warningSign.transform.position.y);
        }
        // once 4 seconds are left
        else if (gameClock <= 4 && footballControl.hasDodged == false)
        {
            warningSign.SetActive(false); // disable the warning sign
            rivalPlayer.SetActive(true); // the rival player appears
            footballControl.canDodge = true; // the player is now able to dodge
        }
        // when the player has dodged
        else if (gameClock <= 4 && footballControl.hasDodged == true)
        {
            footballControl.canDodge = false; // he can't dodge anymore
        }
    }
}
