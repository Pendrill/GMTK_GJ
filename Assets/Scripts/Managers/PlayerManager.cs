using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
    //keep track of the Max and Current Health of the player character
    public float maxHealth, currentHealth;

    //have access to the gameManager
    GameManager theGameManager;
    EnemyManager theEnemyManager;

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
        maxHealth = 1.0f;
        currentHealth = maxHealth;
        theGameManager = FindObjectOfType<GameManager>();
        offScreenStartingPos = new Vector3(-5, -6, 0);
        FightingPos = new Vector3(-5, -1, 0);
        ExitPos = new Vector3(-5, 11, 0);
        setCurrentState(GameState.EnterScene);
	}
	
	// Update is called once per frame
	void Update () {
        if(currentHealth <= 0f)
        {
            theEnemyManager.setCurrentState(EnemyManager.GameState.wait);
            theGameManager.resetTime();
            theGameManager.setCurrentState(GameManager.GameState.Wait);
            setCurrentState(GameState.Die);
        }
        switch (currentState)
        {
            case GameState.Wait:
                time = 0f;
                break;
            case GameState.EnterScene:
                time += Time.deltaTime / 2;
                transform.position = new Vector2(Mathf.SmoothStep(offScreenStartingPos.x, FightingPos.x, time), Mathf.SmoothStep(offScreenStartingPos.y, FightingPos.y, time));// Vector3.Lerp(offScreenStartingPos, FightingPos, time);
                transform.localScale = new Vector3(Mathf.SmoothStep(30, 15, time), Mathf.SmoothStep(30, 15, time), Mathf.SmoothStep(30, 15, time));//Vector3.Lerp(new Vector3(30, 30, 30), new Vector3(15, 15, 15), Time.time/2);
                if (getStateElapsed() > 2.0f)
                {
                    theEnemyManager = FindObjectOfType<EnemyManager>();
                    theEnemyManager.setCurrentState(EnemyManager.GameState.appear);
                    setCurrentState(GameState.Wait);
                    time = 0;
                }
                break;

            case GameState.Fight:
                Debug.Log("You have now entered the fighting state");
                break;

            case GameState.LeaveScene:
                time += Time.deltaTime / 2;
                transform.position = new Vector2(Mathf.SmoothStep(FightingPos.x, ExitPos.x, time), Mathf.SmoothStep(FightingPos.y, ExitPos.y, time));//Vector3.Lerp(FightingPos, ExitPos, time);
                transform.localScale = new Vector3(Mathf.SmoothStep(15, 5, time), Mathf.SmoothStep(15, 5, time), Mathf.SmoothStep(15, 5, time));//Vector3.Lerp(new Vector3(15, 15, 15), new Vector3(5, 5, 5), time);

                break;

            case GameState.Die:
                currentHealth = 1;
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
