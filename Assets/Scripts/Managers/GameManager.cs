using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    //Reference to the current alchemist the player is controlling
    public Transform playerCharacter;
    //Reference to the current enemy being fought 
    public Transform currentEnemy;
    //Reference to the shopkeeper encountered
    public Transform shopkeeper;

    //List of all the possible states
    enum GameState { IntroSequence, PauseBeforeStart, FightingEnemy, OutroSequence, ItemCollection, ShopkeeperSequence, PauseScreen, GameOverSequence};
    //keeps track of the state we are currently in
    GameState currentState;

    //keeps track of the time spent in current state
    float lastStateChange = 0.0f;



	// Use this for initialization
	void Start () {
        setCurrentState(GameState.IntroSequence);
	}
	
	// Update is called once per frame
	void Update () {

        switch (currentState)
        {
            case GameState.IntroSequence:
                //figure out whether the player encounters a shopkeeper or an enemy
                //update the (enemy or shopkeeper) and player variables
                //have the player move up
                // have the ennemy or shopkeeper
                //move on to the pause before start
                break;
            case GameState.PauseBeforeStart:
                //check whether we are dealing with the shopkeeper or an ennemy
                //Fight/Barter comes up from the left, stops in the center, and then moves to the right
                //move on to FightingEnnemy or ShopKeeper phase
                break;
            case GameState.FightingEnemy:
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
                //GAME OVER
                break;
        }
	}
    
    void setCurrentState(GameState state)
    {
        currentState = state;
        lastStateChange = Time.time;
    }

    float getStateElapsed()
    {
        return Time.time - lastStateChange;
    }
}
