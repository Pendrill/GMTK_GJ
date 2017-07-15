﻿using System.Collections;
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

    //List of all the possible states
    public enum GameState {Wait, IntroSequence, PauseBeforeStart, FightingEnemy, OutroSequence, ItemCollection, ShopkeeperSequence, PauseScreen, GameOverSequence};
    //keeps track of the state we are currently in
    public GameState currentState;

    //keeps track of the time spent in current state
    float lastStateChange = 0.0f;

    public Text Fight;

    float time;

    bool enemyIsDefeated, playerIsDead;



	// Use this for initialization
	void Start () {
        //setCurrentState(GameState.IntroSequence);
        thePlayerManager = FindObjectOfType<PlayerManager>();
        theEnemyManager = FindObjectOfType<EnemyManager>();
	}
	
	// Update is called once per frame
	void Update () {

        switch (currentState)
        {
            case GameState.Wait:
                break;
            case GameState.IntroSequence:
                thePlayerManager.setCurrentState(PlayerManager.GameState.EnterScene);
                //figure out whether the player encounters a shopkeeper or an enemy
                //update the (enemy or shopkeeper) and player variables
                //have the player move up
                // have the ennemy or shopkeeper
                //move on to the pause before start
                theEnemyManager = FindObjectOfType<EnemyManager>();
                theShopkeeperManager = FindObjectOfType<ShopkeeperManager>();
                break;
            case GameState.PauseBeforeStart:
                time += Time.deltaTime;
                Fight.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(new Vector2(-377, -4), new Vector2(-15, -4), time);
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
                Fight.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(new Vector2(-15, -4), new Vector2(387, -4), time*3);

                if (enemyIsDefeated)
                {
                    setCurrentState(GameState.OutroSequence);
                }else if (playerIsDead)
                {
                    setCurrentState(GameState.GameOverSequence);
                }
                //Activate the player fighting phase
                //check if either the player or the ennemy are dead
                //move on to the gameOver or Outro Sequence
                break;
            case GameState.OutroSequence:
                //Have the enemie perform death animation and then fade out.
                //Then have the player move upwards out of the canvas
                //Then fade to black 
                //Move on to Item Collection Phase
                break;
            case GameState.ItemCollection:
                //Display the Items that were collected
                //Update the inventory of the player
                //wait until the player presses continue before restarting the phase
                //updating levels and percentages might happen here 
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
}
