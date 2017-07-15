using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopkeeperManager : MonoBehaviour {
    float maxHealth, currentHealth;
    public enum GameState { appear, barter, attack, leave, die };
    public GameState currentState;
    float lastStateChange = 0.0f;
	// Use this for initialization
	void Start () {
        maxHealth = 100.0f;
        currentHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
        switch (currentState)
        {
            case GameState.appear:

                break;
            case GameState.barter:

                break;
            case GameState.attack:

                break;
            case GameState.leave:

                break;
            case GameState.die:

                break;
        }
	}

    public void getCurrentState(GameState state)
    {
        currentState = state;
        lastStateChange = Time.time;
    }
    
    float getElapsedTime()
    {
        return Time.time - lastStateChange;
    }
}
