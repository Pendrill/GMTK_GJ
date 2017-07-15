using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
    //keep track of the Max and Current Health of the player character
    float maxHealth, currentHealth;

    //have access to the gameManager
    GameManager theGameManager;

    //List of states the player character can access
    public enum GameState { EnterScene, Fight, LeaveScene, Die}
    //keep track of the player's current state
    public GameState currentState;

    //keep track of time spent in the current scene
    float lastStateChange = 0.0f;

	// Use this for initialization
	void Start () {
        maxHealth = 100.0f;
        currentHealth = maxHealth;
        theGameManager = FindObjectOfType<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
        switch (currentState)
        {
            case GameState.EnterScene:

                
                break;

            case GameState.Fight:

                break;

            case GameState.LeaveScene:

                break;

            case GameState.Die:

                theGameManager.setCurrentState(GameManager.GameState.GameOverSequence);
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
