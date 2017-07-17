using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {

    //The transparency of the player
    float transparency = 1.0f;

    //The health dial
    public Image healthDial;

    //keep track of the Max and Current Health of the player character
    public float maxHealth, currentHealth;

    //have access to the gameManager
    GameManager theGameManager;
    EnemyManager theEnemyManager;
    ShopkeeperManager theShopkeeperManager;

    //List of states the player character can access
    public enum GameState { EnterScene, Fight, LeaveScene, Die, Wait}
    //keep track of the player's current state
    public GameState currentState;

    //keep track of time spent in the current scene
    float lastStateChange = 0.0f;

    float time;

    public Vector3 offScreenStartingPos, FightingPos, ExitPos;

	// Use this for initialization
	void Start () {
        maxHealth = 100.0f;
        currentHealth = maxHealth;
        theGameManager = FindObjectOfType<GameManager>();
        offScreenStartingPos = new Vector3(-5, -10, 0);
        FightingPos = new Vector3(-5, -3, 0);
        ExitPos = new Vector3(2, 7, 0);
        setCurrentState(GameState.EnterScene);
	}
	
	// Update is called once per frame
	void Update () {

        //Update health every frame no matter what
        healthDial.fillAmount = (currentHealth / maxHealth);
        GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, transparency);
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, transparency);


        //End the game if dead
        if (currentHealth <= 0f)
        {
            theEnemyManager.setCurrentState(EnemyManager.GameState.wait);
            theGameManager.resetTime();
            theGameManager.setCurrentState(GameManager.GameState.Wait);
            setCurrentState(GameState.Die);
        }

        //State machine
        switch (currentState)
        {
            case GameState.Wait:
                time = 0f;
                break;
            case GameState.EnterScene:
                time += Time.deltaTime / 2;
                
                transform.position = new Vector2(Mathf.SmoothStep(offScreenStartingPos.x, FightingPos.x, time), Mathf.SmoothStep(offScreenStartingPos.y, FightingPos.y, time));// Vector3.Lerp(offScreenStartingPos, FightingPos, time);
                transform.localScale = new Vector3(Mathf.SmoothStep(0.3f, 0.1f, time), Mathf.SmoothStep(0.3f, 0.1f, time), Mathf.SmoothStep(0.3f, 0.1f, time));//Vector3.Lerp(new Vector3(30, 30, 30), new Vector3(15, 15, 15), Time.time/2);
                if (getStateElapsed() > 2.0f)
                {
                    if (theGameManager.shopKeepLevel)
                    {
                        theShopkeeperManager = FindObjectOfType<ShopkeeperManager>();
                        theShopkeeperManager.setCurrentState(ShopkeeperManager.GameState.appear);
                    }
                    else
                    {
                        theEnemyManager = FindObjectOfType<EnemyManager>();
                        theEnemyManager.setCurrentState(EnemyManager.GameState.appear);
                    }                    
                    setCurrentState(GameState.Wait);
                    time = 0;
                }
                break;

            case GameState.Fight:
                time += Time.deltaTime / 2;
                transparency = Mathf.SmoothStep(1.0f, 0.3f, time);

                break;

            case GameState.LeaveScene:
                time += Time.deltaTime / 2;
                transparency = Mathf.SmoothStep(0.3f, 1.0f, time);
                transform.position = new Vector2(Mathf.SmoothStep(FightingPos.x, ExitPos.x, time), Mathf.SmoothStep(FightingPos.y, ExitPos.y, time));//Vector3.Lerp(FightingPos, ExitPos, time);
                transform.localScale = new Vector3(Mathf.SmoothStep(0.1f, 0.05f, time), Mathf.SmoothStep(0.1f, 0.05f, time), Mathf.SmoothStep(0.1f, 0.05f, time));//Vector3.Lerp(new Vector3(15, 15, 15), new Vector3(5, 5, 5), time);

                break;

            case GameState.Die:
                currentHealth = 1;
                theGameManager.setCurrentState(GameManager.GameState.GameOverSequence);
                setCurrentState(GameState.Wait);
                FindObjectOfType<ButtonManager>().SetAllButtons(false);
                healthDial.gameObject.SetActive(false);
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
}
