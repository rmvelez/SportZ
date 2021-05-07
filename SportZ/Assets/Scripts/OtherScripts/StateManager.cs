using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StateManager : MonoBehaviour
{
    // used to indicate the various states of the game
    public enum GameState {TitleState, WinState, LoseState, LearnState, OneState, TwoState, ThreeState, FourState, FiveState, SixState, SevenState, EightState, NineState};
    public GameState currentGameState; // used to reference the current game state

    // these objects reference the non micro game scenes
    public GameObject title;
    public GameObject win;
    public GameObject lose;
    public GameObject learn;

    // the micro game scene objects
    public GameObject microOne;
    public GameObject microTwo;
    public GameObject microThree;
    public GameObject microFour;
    public GameObject microFive;
    public GameObject microSix;
    public GameObject microSeven;
    public GameObject microEight;
    public GameObject microNine;

    // reference to the text that displays the timer for each microgame
    public TMP_Text limitText;
    public TMP_Text limitText2;
    public TMP_Text limitText3;
    public TMP_Text limitText4;
    public TMP_Text limitText5;
    public TMP_Text limitText6;
    public TMP_Text limitText7;
    public TMP_Text limitText8;
    public TMP_Text limitText9;

    public float waitPeriod; // the time it takes until the next scene is loaded
    public float gamePeriod; // the time that a microgame lasts for

    public int highScore; // tracks how many wins the player has gained
    public TMP_Text winHighScoreText; // displays the high score on the win screen
    public TMP_Text loseHighScoreText; // displays the same score on the lose screen

    private bool hasStarted; // determines if the endless mode has started

    public float randMicrogame; // used to determine which microgame to load

    public float delayClock; // used to determine the length of delay until the next microgame is loaded

    public bool nextMicrogame; // determines when to start the timer for the next microgame

    public GameObject baseballPlayer; // refernec to the baseball player character from the first microgame
    private BaseballControl baseballControl; // reference to the script that controls the baseball player
    public GameObject junkSpawnA; // spawns the junk thats thrown at the player
    public GameObject junkSpawnB;
    public GameObject junkSpawnC;
    public GameObject ballSpawn; // spawns the ball that the player must catch
    private MicrogameControl microgameControl; // references the script on the first microgame

    // references to the player character and associated scripts for the second microgame
    public GameObject footballPlayer;
    private FootballControl footballControl;
    public GameObject footballGame;
    private FootballManagement footballManagement;
    public GameObject rivalFootballPlayer;
    private RivalMove rivalMove;
    private MicrogameControl microgameControl2;

    // references to the player character and associated scripts for the third microgame
    public GameObject basketSoccerPlayer;
    private BasketSoccerControl basketSoccerControl;
    public GameObject rivalBasketSoccerPlayer;
    private BasketSoccerRival basketSoccerRival;
    public GameObject microThreeBall;
    private BasketSoccerBall basketSoccerBall;
    private MicrogameControl microgameControl3;

    // references to the player character and associated scripts for the fourth microgame
    public GameObject boxingPlayer;
    private BoxingControl boxingControl;
    public GameObject punchingBag;
    private MicrogameControl microgameControl4;

    // references to the player character and associated scripts for the fifth microgame
    public GameObject bowlingPlayer;
    private BowlingControl bowlingControl;
    public GameObject bowlingPuck;
    private HockeyPuck hockeyPuck;
    public GameObject bowlingPins;
    private MicrogameControl microgameControl5;

    // references to the player character and associated scripts for the sixth microgame
    public GameObject dodgeTennisPlayer;
    private DodgeTennisControl dodgeTennisControl;
    public GameObject dodgeTennisRival;
    public GameObject tableTennisBall;
    private DodgeTennisBall dodgeTennisBall;
    private MicrogameControl microgameControl6;

    // references to the player character and associated scripts for the seventh microgame
    public GameObject soccerPlayer;
    private SoccerControl soccerControl;
    private MicrogameControl microgameControl7;

    // references to the player character and associated scripts for the eighth microgame
    public GameObject golfPlayer;
    private GolfItControl golfItControl;
    private MicrogameControl microgameControl8;

    // reference to the player character and all associated scripts and game objects for the ninth microgame
    public GameObject horsePlayer;
    public GameObject targetA;
    public GameObject targetB;
    public GameObject targetC;
    private NineControl nineControl;
    private MicrogameControl microgameControl9;

    // Start is called before the first frame update
    void Start()
    {
        currentGameState = GameState.TitleState; // the game starts here
        ShowScreen(title);
        hasStarted = false; // the endless mode has not started
        nextMicrogame = false; // don't load the next microgame
        highScore = 0;

        GetScripts(); // gets all the scripts that this script needs
    }

    // Update is called once per frame
    void Update()
    {
        // once endless mode has started
        if(hasStarted == true)
        {
            //run until the timer has hit zero
            waitPeriod -= Time.deltaTime;

            // once that happens
            if (waitPeriod <= 0)
            {
                // call on this function
                MicrogameLimit();
            }
        }

        if (nextMicrogame == true)
        {
            delayClock -= Time.deltaTime;

            if (delayClock <= 0)
            {
                StartGame();
            }
        }
    }

    // this function is called on the start button on the title screen
    // also called after you win a microgame
    public void StartGame()
    {
        // the game loads a microgame and sets it up
        //currentGameState = GameState.TwoState;
        //ShowScreen(microTwo);
        LoadMicrogame(); // calls on this function to load a random microgame
        MicrogameConditions(); // calls on this function to set up each microgame
        delayClock = 3;
        hasStarted = true; // also, endless mode starts
        nextMicrogame = false;
    }

    // this function is called on the restart button on the lose screen
    public void RestartGame()
    {
        LoadMicrogame(); // calls on this function to load a random microgame
        MicrogameConditions(); // calls on this function to set up each microgame
        delayClock = 3;
        highScore = 0;
        hasStarted = true; // also, endless mode starts
        nextMicrogame = false;
    }

    // this function is called on the quit button
    public void BackToStart()
    {
        // goes back to the title screen
        currentGameState = GameState.TitleState;
        ShowScreen(title);
    }

    // this functions is called on the instructions button
    public void HowToPlay()
    {
        // goes back to the title screen
        currentGameState = GameState.LearnState;
        ShowScreen(learn);
    }

    // this function gets all the scripts that the state manager script needs to function properly
    private void GetScripts()
    {
        // gets the scripts from the following game objects for the first microgame
        baseballControl = baseballPlayer.GetComponent<BaseballControl>();
        microgameControl = microOne.GetComponent<MicrogameControl>();

        // gets the scripts from the following game objects for the second microgame
        footballControl = footballPlayer.GetComponent<FootballControl>();
        footballManagement = footballGame.GetComponent<FootballManagement>();
        rivalMove = rivalFootballPlayer.GetComponent<RivalMove>();
        microgameControl2 = microTwo.GetComponent<MicrogameControl>();

        // gets the scripts from the following game objects for the third microgame
        basketSoccerControl = basketSoccerPlayer.GetComponent<BasketSoccerControl>();
        basketSoccerRival = rivalBasketSoccerPlayer.GetComponent<BasketSoccerRival>();
        basketSoccerBall = microThreeBall.GetComponent<BasketSoccerBall>();
        microgameControl3 = microThree.GetComponent<MicrogameControl>();

        // gets the scripts from the following game objects for the fourth microgame
        boxingControl = boxingPlayer.GetComponent<BoxingControl>();
        microgameControl4 = microFour.GetComponent<MicrogameControl>();

        // gets the scripts from the following game objects for the fifth microgame
        bowlingControl = bowlingPlayer.GetComponent<BowlingControl>();
        hockeyPuck = bowlingPuck.GetComponent<HockeyPuck>();
        microgameControl5 = microFive.GetComponent<MicrogameControl>();

        // gets the scripts from the following game objects for the sixth microgame
        dodgeTennisControl = dodgeTennisPlayer.GetComponent<DodgeTennisControl>();
        dodgeTennisBall = tableTennisBall.GetComponent<DodgeTennisBall>();
        microgameControl6 = microSix.GetComponent<MicrogameControl>();

        // gets the scripts from the following game objects for the seventh microgame
        soccerControl = soccerPlayer.GetComponent<SoccerControl>();
        microgameControl7 = microSeven.GetComponent<MicrogameControl>();

        // gets the scripts from the following game objects for the eighth microgame
        golfItControl = golfPlayer.GetComponent<GolfItControl>();
        microgameControl8 = microEight.GetComponent<MicrogameControl>();

        // gets the scripts from the following game objects for the ninth microgame
        nineControl = horsePlayer.GetComponent<NineControl>();
        microgameControl9 = microNine.GetComponent<MicrogameControl>();
    }

    // function that ensures that the win and lose conditions are reset when the next microgame is loaded
    // this helps to ensure that the player does not instantly win or lose if they play the same microgame again
    private void MicrogameConditions()
    {
        this.transform.position = new Vector3(0, 0, -10);

        // conditions for the first microgame
        baseballControl.hasWon = false;
        baseballControl.hasLost = false;
        junkSpawnA.SetActive(true);
        junkSpawnB.SetActive(true);
        junkSpawnC.SetActive(true);
        ballSpawn.SetActive(true);
        baseballPlayer.transform.position = new Vector2(0, 0);

        // conditions for the second microgame
        footballControl.hasWon = false;
        footballControl.hasLost = false;
        footballControl.hasDodged = false;
        footballControl.canDodge = false;
        footballControl.isMoving = true;
        footballManagement.gameClock = 6;
        footballManagement.warningSign.SetActive(false);
        footballManagement.rivalPlayer.SetActive(false);
        footballManagement.rivalPlayer.transform.position = new Vector2(-3, -3.4f);
        footballPlayer.transform.position = new Vector2(0, -3.5f);
        rivalMove.isMoving = true;

        // conditions for the third microgame
        basketSoccerControl.hasWon = false;
        basketSoccerControl.hasLost = false;
        basketSoccerRival.isMoving = true;
        basketSoccerRival.canKick = true;
        basketSoccerBall.isFlying = false;
        basketSoccerBall.isFlyingBackwards = false;
        basketSoccerPlayer.transform.position = new Vector2(-6, -3.4f);
        rivalBasketSoccerPlayer.transform.position = new Vector2(6, -3.4f);
        microThreeBall.transform.position = new Vector2(0, -3.6f);
        microThreeBall.SetActive(true);

        // conditions for the fourth microgame
        boxingControl.hasWon = false;
        boxingControl.hasLost = false;
        boxingControl.hitCount = 0;
        boxingControl.goSign.SetActive(false);
        boxingPlayer.transform.position = new Vector2(-5.1f, -1.33f);
        punchingBag.SetActive(true);
        punchingBag.transform.position = new Vector2(3.1f, -1.43f);

        // conditions for the fifth microgame
        bowlingControl.hasWon = false;
        bowlingControl.hasLost = false;
        bowlingControl.canHit = false;
        bowlingControl.stopSign.SetActive(true);
        bowlingControl.goSign.SetActive(false);
        bowlingControl.startClockBetweenHits = 2f;
        bowlingControl.startWaitClock = 1f;
        bowlingPuck.SetActive(true);
        hockeyPuck.isMoving = false;
        bowlingPuck.transform.position = new Vector2(-1.75f, -2.63f);
        bowlingPins.SetActive(true);

        // conditions for the sixth microgame
        dodgeTennisControl.hasWon = false;
        dodgeTennisControl.hasLost = false;
        dodgeTennisBall.movingRight = false;
        dodgeTennisPlayer.SetActive(true);
        dodgeTennisRival.SetActive(true);
        tableTennisBall.transform.position = new Vector2(2, 0);

        // conditions for the seventh microgame
        soccerControl.hasWon = false;
        soccerControl.hasLost = false;
        soccerControl.ballSpawner.SetActive(true);
        soccerPlayer.transform.position = new Vector2(3, 0);

        // conditions for the eigth microgame
        golfItControl.hasWon = false;
        golfItControl.hasLost = false;
        golfItControl.warningSign.SetActive(false);
        golfItControl.golfItBall.SetActive(false);
        golfItControl.clockHold = 6f;
        golfItControl.golfItBall.transform.position = new Vector2(0, 5.39f);

        // conditions for the ninth microgame
        nineControl.hasWon = false;
        nineControl.hasLost = false;
        nineControl.canMove = true;
        horsePlayer.transform.position = new Vector2(-4, -2.21f);
        targetA.SetActive(true);
        targetB.SetActive(true);
        targetC.SetActive(true);

    }

    // this function controls how the microgame runs
    private void MicrogameLimit()
    {
        // if the player has lost the microgame
        if (gamePeriod <= 0 && baseballControl.hasLost == true || gamePeriod <= 0 && footballControl.hasLost == true || gamePeriod >= 0 && basketSoccerControl.hasLost == true || gamePeriod <= 0 && boxingControl.hasLost == true || gamePeriod <= 0 && bowlingControl.hasLost == true || gamePeriod >= 0 && dodgeTennisControl.hasLost == true || gamePeriod >= 0 && soccerControl.hasLost == true || gamePeriod <= 0 && golfItControl.hasLost == true || gamePeriod <= 0 && nineControl.hasLost == true)
        {
            // set the game to the lose state and reset all of the variables
            currentGameState = GameState.LoseState;
            ShowScreen(lose);
            hasStarted = false;
            waitPeriod = 3f;
            gamePeriod = 6;
            loseHighScoreText.text = "High Score: " + highScore.ToString();
            MicrogameControlReset();
        }
        // if the player has won the microgame
        else if(baseballControl.hasWon == true && gamePeriod <= 0 || gamePeriod <= 0 && footballControl.hasWon == true || gamePeriod >= 0 && basketSoccerControl.hasWon == true || gamePeriod <= 0 && boxingControl.hasWon == true || gamePeriod >= 0 && bowlingControl.hasWon == true || gamePeriod >= 0 && dodgeTennisControl.hasWon == true || gamePeriod >= 0 && soccerControl.hasWon == true || gamePeriod <= 0 && golfItControl.hasWon == true || gamePeriod <= 0 && nineControl.hasWon == true)
        {
            // set the game to the win state and waits until the next microgame is ready
            currentGameState = GameState.WinState;
            ShowScreen(win);
            hasStarted = false;
            waitPeriod = 3f;
            gamePeriod = 6;
            highScore++;
            winHighScoreText.text = "High Score: " + highScore.ToString();
            MicrogameControlReset();
            nextMicrogame = true;
        }
        // otherwise
        else
        {
            // decrease the game time until it hits zero, while also displaying it
            gamePeriod -= Time.deltaTime;
            SetTimerText();
        }
    }

    // resets the wait timer on each microgame script instance for each microgame
    // and ensures that when loaded, each microgame starts on the message screen
    private void MicrogameControlReset()
    {
        microgameControl.waitPeriod = 3f;
        microgameControl2.waitPeriod = 3f;
        microgameControl3.waitPeriod = 3f;
        microgameControl4.waitPeriod = 3f;
        microgameControl5.waitPeriod = 3f;
        microgameControl6.waitPeriod = 3f;
        microgameControl7.waitPeriod = 3f;
        microgameControl8.waitPeriod = 3f;
        microgameControl9.waitPeriod = 3f;

        microgameControl.microGame.SetActive(false);
        microgameControl.messageScreen.SetActive(true);

        microgameControl2.microGame.SetActive(false);
        microgameControl2.messageScreen.SetActive(true);

        microgameControl3.microGame.SetActive(false);
        microgameControl3.messageScreen.SetActive(true);

        microgameControl4.microGame.SetActive(false);
        microgameControl4.messageScreen.SetActive(true);

        microgameControl5.microGame.SetActive(false);
        microgameControl5.messageScreen.SetActive(true);

        microgameControl6.microGame.SetActive(false);
        microgameControl6.messageScreen.SetActive(true);

        microgameControl7.microGame.SetActive(false);
        microgameControl7.messageScreen.SetActive(true);

        microgameControl8.microGame.SetActive(false);
        microgameControl8.messageScreen.SetActive(true);
        
        microgameControl9.microGame.SetActive(false);
        microgameControl9.messageScreen.SetActive(true);
    }

    // ensures that the microgame timer gets displayed as it counts down for each microgame
    private void SetTimerText()
    {
        limitText.text = Mathf.Round(gamePeriod).ToString();
        limitText2.text = Mathf.Round(gamePeriod).ToString();
        limitText3.text = Mathf.Round(gamePeriod).ToString();
        limitText4.text = Mathf.Round(gamePeriod).ToString();
        limitText5.text = Mathf.Round(gamePeriod).ToString();
        limitText6.text = Mathf.Round(gamePeriod).ToString();
        limitText7.text = Mathf.Round(gamePeriod).ToString();
        limitText8.text = Mathf.Round(gamePeriod).ToString();
        limitText9.text = Mathf.Round(gamePeriod).ToString();
    }

    // function that loads a random microgame when the game starts 
    private void LoadMicrogame()
    {
        // this var is set to a random number
        randMicrogame = Random.Range(1, 9);

        // depending on the number, it will load a corresponding microgame
        if (randMicrogame == 1)
        {
            currentGameState = GameState.OneState;
            ShowScreen(microOne);
        }
        if(randMicrogame == 2)
        {
            currentGameState = GameState.TwoState;
            ShowScreen(microTwo);
        }
        if (randMicrogame == 3)
        {
            currentGameState = GameState.ThreeState;
            ShowScreen(microThree);
        }
        if (randMicrogame == 4)
        {
            currentGameState = GameState.FourState;
            ShowScreen(microFour);
        }
        if (randMicrogame == 5)
        {
            currentGameState = GameState.FiveState;
            ShowScreen(microFive);
        }
        if (randMicrogame == 6)
        {
            currentGameState = GameState.SixState;
            ShowScreen(microSix);
        }
        if (randMicrogame == 7)
        {
            currentGameState = GameState.SevenState;
            ShowScreen(microSeven);
        }
        if (randMicrogame == 8)
        {
            currentGameState = GameState.EightState;
            ShowScreen(microEight);
        }
        if (randMicrogame == 9)
        {
            currentGameState = GameState.NineState;
            ShowScreen(microNine);
        }
    }

    // private function used to determine which game screen to show
    private void ShowScreen(GameObject gameObjectToShow)
    {
        // the other scenes are set to false
        title.SetActive(false);
        win.SetActive(false);
        lose.SetActive(false);
        learn.SetActive(false);

        // including the microgames
        microOne.SetActive(false);
        microTwo.SetActive(false);
        microThree.SetActive(false);
        microFour.SetActive(false);
        microFive.SetActive(false);
        microSix.SetActive(false);
        microSeven.SetActive(false);
        microEight.SetActive(false);
        microNine.SetActive(false);

        gameObjectToShow.SetActive(true); // ensures that only one screen is active at a time
    }
}
