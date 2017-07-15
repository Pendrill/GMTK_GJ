using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    //Reference to the current alchemist the player is controlling
    public Transform playerCharacter;
    public PlayerManager thePlayerManager;
    //Reference to the current enemy being fought 
    public Transform currentEnemy;
    public EnemyManager theEnemyManager;
    //Reference to the shopkeeper encountered
    public Transform shopkeeper;
    public ShopkeeperManager theShopkeeperManager;

    public GameObject[] enemies;

    //List of all the possible states
    public enum GameState {Wait, IntroSequence, PauseBeforeStart, FightingEnemy, OutroSequence, ItemCollection, ShopkeeperSequence, PauseScreen, GameOverSequence, RestartPhase};
    //keeps track of the state we are currently in
    public GameState currentState;

    //keeps track of the time spent in current state
    float lastStateChange = 0.0f;

    public Text Fight;

    public float time, alpha;

    bool enemyIsDefeated, playerIsDead;

    public Image fadeToBlack;



	// Use this for initialization
	void Start () {
        //setCurrentState(GameState.IntroSequence);
        Instantiate(enemies[Random.Range(0, 3)]);
        thePlayerManager = FindObjectOfType<PlayerManager>();
        theEnemyManager = FindObjectOfType<EnemyManager>();
	}
	
	// Update is called once per frame
	void Update () {

        switch (currentState)
        {
            case GameState.Wait:
                time = 0f;
                break;
            case GameState.IntroSequence:
                theEnemyManager.currentHealth = 100;
                thePlayerManager.currentHealth = 5;
                thePlayerManager.setCurrentState(PlayerManager.GameState.EnterScene);
                //figure out whether the player encounters a shopkeeper or an enemy
                //update the (enemy or shopkeeper) and player variables
                //have the player move up
                // have the ennemy or shopkeeper
                //move on to the pause before start
                theEnemyManager = FindObjectOfType<EnemyManager>();
                theShopkeeperManager = FindObjectOfType<ShopkeeperManager>();
                theEnemyManager.setCurrentState(EnemyManager.GameState.wait);
                setCurrentState(GameState.Wait);
                break;
            case GameState.PauseBeforeStart:
                time += Time.deltaTime;
                //Fight.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(new Vector2(-377, -4), new Vector2(-15, -4), time);
                Fight.GetComponent<RectTransform>().anchoredPosition = new Vector2(Mathf.SmoothStep(-377, -15, time), -4);
                if (getStateElapsed() > 2.0f)
                {
                    thePlayerManager.setCurrentState(PlayerManager.GameState.Fight);
                    theEnemyManager.setCurrentState(EnemyManager.GameState.attack);
                    setCurrentState(GameState.FightingEnemy);
                    time = 0.0f;
                }
                //check whether we are dealing with the shopkeeper or an ennemy
                //Fight/Barter comes up from the left, stops in the center, and then moves to the right
                //move on to FightingEnnemy or ShopKeeper phase
                break;
            case GameState.FightingEnemy:
                time += Time.deltaTime;
                Fight.GetComponent<RectTransform>().anchoredPosition = new Vector2(Mathf.SmoothStep(-15, 387, time*3), -4); // new Vector2(-15, -4), new Vector2(387, -4), time*3);

                //this code might be unnecessary, I think i can do these things from the player and ennemy manager.
                /*if (enemyIsDefeated)
                {
                    setCurrentState(GameState.OutroSequence);
                }else if (playerIsDead)
                {
                    setCurrentState(GameState.GameOverSequence);
                }*/

                //Activate the player fighting phase
                //check if either the player or the ennemy are dead
                //move on to the gameOver or Outro Sequence
                break;
            case GameState.OutroSequence:
                theEnemyManager.setCurrentState(EnemyManager.GameState.wait);
                thePlayerManager.setCurrentState(PlayerManager.GameState.LeaveScene);
                time += Time.deltaTime * 2;
                Color tmp = fadeToBlack.color;
                alpha = Mathf.Lerp(0.0f, 1.0f, time/2);
                tmp.a = alpha;
                fadeToBlack.color = tmp;
                if (getStateElapsed() > 2.0f)
                {
                    thePlayerManager.setCurrentState(PlayerManager.GameState.Wait);
                    theEnemyManager.setCurrentState(EnemyManager.GameState.wait);
                    setCurrentState(GameState.ItemCollection);
                    time = 0.0f;
                }
                //Have the enemie perform death animation and then fade out.
                //Then have the player move upwards out of the canvas
                //Then fade to black 
                //Move on to Item Collection Phase
                break;
            case GameState.ItemCollection:
                Debug.Log("You have collected this: insert name here");
                if(getStateElapsed() > 3.0f)
                {
                    setCurrentState(GameState.RestartPhase);
                }
                //Display the Items that were collected
                //Update the inventory of the player
                //wait until the player presses continue before restarting the phase
                //updating levels and percentages might happen here 
                break;
            case GameState.RestartPhase:
                //this is where we change the background, music, ennemies, shopkeep, etc
                
                time += Time.deltaTime * 2;
                Color tmp2 = fadeToBlack.color;
                alpha = Mathf.Lerp(1.0f, 0.0f, time / 2);
                tmp2.a = alpha;
                fadeToBlack.color = tmp2;
                if(getStateElapsed() > 2.0f)
                {
                    Destroy(theEnemyManager.gameObject);
                    Instantiate(enemies[Random.Range(0, 3)]);
                    time = 0;
                    setCurrentState(GameState.IntroSequence);
                }
                break;
            case GameState.ShopkeeperSequence:
                //enable the phase to barter/fight the shopkeeper
                //figure out if we need an other outro sequence for the shopkeeper, or if we can skip that
                //move back to the intro sequence
                break;
            case GameState.PauseScreen:
                //this is for if we have the time;
                break;
            case GameState.GameOverSequence:
                Debug.Log("The Player Died");
                time += Time.deltaTime * 2;
                Color tmp3 = fadeToBlack.color;
                alpha = Mathf.Lerp(0.0f, 1.0f, time);
                tmp3.a = alpha;
                fadeToBlack.color = tmp3;
                //GAME OVER
                break;
        }
	}
    
    public void setCurrentState(GameState state)
    {
        currentState = state;
        lastStateChange = Time.time;
    }

    float getStateElapsed()
    {
        return Time.time - lastStateChange;
    }
    public void resetTime()
    {
        time = 0.0f;
    }
}
