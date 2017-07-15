using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
    //keep track of the Max and Current Health of the player character
    float maxHealth, currentHealth;

    //have access to the gameManager
    GameManager theGameManager;
    EnemyManager theEnemyManager;

    //List of states the player character can access
    public enum GameState { EnterScene, Fight, LeaveScene, Die, Wait}
    //keep track of the player's current state
    public GameState currentState;

    //keep track of time spent in the current scene
    float lastStateChange = 0.0f;

    public Vector3 offScreenStartingPos, FightingPos, ExitPos;

	// Use this for initialization
	void Start () {
        maxHealth = 100.0f;
        currentHealth = maxHealth;
        //theGameManager = FindObjectOfType<GameManager>();
        offScreenStartingPos = new Vector3(-5, -6, 0);
        FightingPos = new Vector3(-5, -1, 0);
        ExitPos = new Vector3(-5, 11, 0);
        setCurrentState(GameState.EnterScene);
	}
	
	// Update is called once per frame
	void Update () {
        switch (currentState)
        {
            case GameState.Wait:
                break;
            case GameState.EnterScene:
                transform.position = Vector3.Lerp(offScreenStartingPos, FightingPos, Time.time/2);
                transform.localScale = Vector3.Lerp(new Vector3(30, 30, 30), new Vector3(15, 15, 15), Time.time/2);
                if(getStateElapsed() > 2.0f)
                {
                    theEnemyManager = FindObjectOfType<EnemyManager>();
                    theEnemyManager.setCurrentState(EnemyManager.GameState.appear);
                    setCurrentState(GameState.Wait);
                }
                break;

            case GameState.Fight:
                Debug.Log("You have now entered the fighting state");
                break;

            case GameState.LeaveScene:

                break;

            case GameState.Die:

                //theGameManager.setCurrentState(GameManager.GameState.GameOverSequence);
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
