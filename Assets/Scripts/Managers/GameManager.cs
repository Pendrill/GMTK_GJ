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
    public GameObject shopkeeper;
    public ShopkeeperManager theShopkeeperManager;
    public ButtonManager theButtonManager;

    public GameObject[] enemies;
    public GameObject[] backgrounds;

    public int currentLevel, lastLevelShop;
    public bool shopKeepLevel;

    //List of all the possible states
    public enum GameState {Wait, IntroSequence, PauseBeforeStart, FightingEnemy, OutroSequence, ItemCollection, ShopkeeperSequence, PauseScreen, GameOverSequence, RestartPhase};
    //keeps track of the state we are currently in
    public GameState currentState;

    //keeps track of the time spent in current state
    float lastStateChange = 0.0f;

    public Text Fight, Collection;

    public float time, alpha;

    bool enemyIsDefeated, playerIsDead;

    public Image fadeToBlack;
    public int totalDrop = 5;

    InventoryScript theInventoryScript;



	// Use this for initialization
	void Start () {
        currentLevel = 1;
        lastLevelShop = 1;
        //setCurrentState(GameState.IntroSequence);
        Instantiate(enemies[Random.Range(0, 3)]);
        thePlayerManager = FindObjectOfType<PlayerManager>();
        theEnemyManager = FindObjectOfType<EnemyManager>();
        theInventoryScript = FindObjectOfType<InventoryScript>();
        theButtonManager = FindObjectOfType<ButtonManager>();
	}
	
	// Update is called once per frame
	void Update () {

        switch (currentState)
        {
            case GameState.Wait:
                time = 0f;
                break;
            case GameState.IntroSequence:
                //theEnemyManager.currentHealth = 100;
                //thePlayerManager.currentHealth = 5;
                thePlayerManager.setCurrentState(PlayerManager.GameState.EnterScene);
                //figure out whether the player encounters a shopkeeper or an enemy
                //update the (enemy or shopkeeper) and player variables
                //have the player move up
                // have the ennemy or shopkeeper
                //move on to the pause before start
                if (shopKeepLevel)
                {
                    //WE NEED TO DISABLE THIS VALUE AT SOME POINT!!!!!
                    theShopkeeperManager = FindObjectOfType<ShopkeeperManager>();
                    //set the shopkeeper to wait                    
                }
                else
                {
                    theEnemyManager = FindObjectOfType<EnemyManager>();
                    theEnemyManager.setCurrentState(EnemyManager.GameState.wait);
                }  
                setCurrentState(GameState.Wait);
                break;
            case GameState.PauseBeforeStart:
                time += Time.deltaTime;
                //Fight.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(new Vector2(-377, -4), new Vector2(-15, -4), time);
                Fight.GetComponent<RectTransform>().anchoredPosition = new Vector2(Mathf.SmoothStep(-450, -15, time), -4);
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
                Fight.GetComponent<RectTransform>().anchoredPosition = new Vector2(Mathf.SmoothStep(-15, 600, time*3), -4); // new Vector2(-15, -4), new Vector2(387, -4), time*3);

                theButtonManager.SetAllButtons(true);
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
                theButtonManager.SetAllButtons(false);
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
                int fire = 0, water = 0, earth = 0, air = 0;
                if (!shopKeepLevel)
                {
                    Collection.text = "You picked up: ";
                    
                    for (int i = 0; i < totalDrop; i++)
                    {
                        if (theEnemyManager.GetComponent<EnemyType>().drop[i] == Item.EmberPebble)
                        {
                            fire += 1;
                            //theInventoryScript.ChangeItemValue(Item.EmberPebble, 1);
                        }
                        else if (theEnemyManager.GetComponent<EnemyType>().drop[i] == Item.GaiaSeed)
                        {
                            earth += 1;
                            //theInventoryScript.ChangeItemValue(Item.GaiaSeed, 1);
                        }
                        else if (theEnemyManager.GetComponent<EnemyType>().drop[i] == Item.NimbusQuill)
                        {
                            air += 1;
                            //theInventoryScript.ChangeItemValue(Item.NimbusQuill, 1);
                        }
                        else if (theEnemyManager.GetComponent<EnemyType>().drop[i] == Item.MermaidScale)
                        {
                            water += 1;
                            //theInventoryScript.ChangeItemValue(Item.MermaidScale, 1);
                        }
                    }
                    if (fire != 0)
                    {
                        Collection.text += "\n Ember Pebble: " + fire;
                    }
                    if (earth != 0)
                    {
                        Collection.text += "\n GaiaSeed: " + earth;
                    }
                    if (air != 0)
                    {
                        Collection.text += "\n NimbusQuill: " + air;
                    }
                    if (water != 0)
                    {
                        Collection.text += "\n MermaidScale: " + water;
                    }
                    Collection.gameObject.SetActive(true);
                }else
                {
                    if(thePlayerManager.currentHealth == thePlayerManager.maxHealth)
                    {
                       // thePlayerManager.currentHealth -= 25;
                       // thePlayerManager.maxHealth += 50;
                        Collection.text = "Thank you for these ressources stranger. \nYou seem in good health, let me enhances your abilities\n\nYour max health pool has increased to: " + thePlayerManager.maxHealth + 50 + "\nUnfortunately, you had to sustain 25 damage.";
                    }
                    else
                    {
                        //thePlayerManager.currentHealth += 25;
                        //Mathf.Clamp(thePlayerManager.currentHealth, 0, thePlayerManager.maxHealth);
                        Collection.text = "Thank you for theses ressources stranger.\nLet me heal your wounds";
                    }
                    Collection.gameObject.SetActive(true);
                }
                if(getStateElapsed() > 4.0f)
                {
                    if (!shopKeepLevel)
                    {
                        if (fire != 0)
                        {
                            theInventoryScript.ChangeItemValue(Item.EmberPebble, fire);
                        }
                        if (earth != 0)
                        {
                            theInventoryScript.ChangeItemValue(Item.GaiaSeed, earth);
                        }
                        if (air != 0)
                        {
                            theInventoryScript.ChangeItemValue(Item.NimbusQuill, air);
                        }
                        if (water != 0)
                        {
                            theInventoryScript.ChangeItemValue(Item.MermaidScale, water);
                        }
                    }else
                    {
                        if (thePlayerManager.currentHealth == thePlayerManager.maxHealth)
                        {
                            thePlayerManager.currentHealth -= 25;
                            thePlayerManager.maxHealth += 50;
                            //Collection.text = "Thank you for these ressources stranger. \nYou seem in good health, let me enhances your abilities\n\nYour max health pool has increased to: " + thePlayerManager.maxHealth + "\nUnfortunately, you had to sustain 25 damage.";
                        }
                        else
                        {
                            //thePlayerManager.currentHealth += 25;
                            thePlayerManager.currentHealth = Mathf.Clamp(thePlayerManager.currentHealth += 25, 0, thePlayerManager.maxHealth) ;
                            //Collection.text = "Thank you for theses ressources stranger.\nLet me heal your wounds";
                        }
                    }
                    Collection.gameObject.SetActive(false);
                    setCurrentState(GameState.RestartPhase);
                }
                //Display the Items that were collected
                //Update the inventory of the player
                //wait until the player presses continue before restarting the phase
                //updating levels and percentages might happen here 
                break;
            case GameState.RestartPhase:
                //this is where we change the background, music, ennemies, shopkeep, etc
                shopKeepLevel = false;
                time += Time.deltaTime * 2;
                Color tmp2 = fadeToBlack.color;
                alpha = Mathf.Lerp(1.0f, 0.0f, time / 2);
                tmp2.a = alpha;
                fadeToBlack.color = tmp2;
                if(getStateElapsed() > 2.0f)
                {
                    //attempts to spawn a shopkeeper
                    int levelsSinceShop = currentLevel - lastLevelShop;
                    currentLevel += 1;
                    //level that just ended was a shop, automatically spawn a monster for next level
                    if (levelsSinceShop == 0 && currentLevel != 2)
                    {
                        Destroy(theShopkeeperManager.gameObject);
                        Instantiate(enemies[Random.Range(0, 3)]);
                        
                    }else if(levelsSinceShop < 4)
                    {
                        Destroy(theEnemyManager.gameObject);
                        int spawnshop = Random.Range(1, 11);
                        if(spawnshop <= 10)
                        {
                            //Instantiate the shopkeeper
                            Instantiate(shopkeeper);
                            shopKeepLevel = true;
                            lastLevelShop = currentLevel;
                        }else
                        {
                            Instantiate(enemies[Random.Range(0, 3)]);
                        }                        
                    }
                    else if(levelsSinceShop < 8)
                    {
                        Destroy(theEnemyManager.gameObject);
                        int spawnshop = Random.Range(1, 11);
                        if (spawnshop <= 4)
                        {
                            //Instantiate the shopkeeper
                            Instantiate(shopkeeper);
                            shopKeepLevel = true;
                            lastLevelShop = currentLevel;
                        }
                        else
                        {
                            Instantiate(enemies[Random.Range(0, 3)]);
                        }
                    }
                    else if(levelsSinceShop < 10)
                    {
                        Destroy(theEnemyManager.gameObject);
                        int spawnshop = Random.Range(1, 11);
                        if (spawnshop <= 6)
                        {
                            //Instantiate the shopkeeper
                            Instantiate(shopkeeper);
                            shopKeepLevel = true;
                            lastLevelShop = currentLevel;
                        }
                        else
                        {
                            Instantiate(enemies[Random.Range(0, 3)]);
                        }
                    }
                    else if(levelsSinceShop >= 10)
                    {
                        Destroy(theEnemyManager.gameObject);
                        //instantiate the shopkeeper
                        Instantiate(shopkeeper);
                        shopKeepLevel = true;
                        lastLevelShop = currentLevel;
                    }
                    //Destroy(theEnemyManager.gameObject);
                    //Instantiate(enemies[Random.Range(0, 3)]);
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
