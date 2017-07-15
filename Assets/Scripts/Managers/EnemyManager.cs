using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
    public float maxHealth, currentHealth;
    public enum GameState { wait, appear, attack, die};
    public GameState currentState;
    float lastStateChange = 0.0f;
    float time;

    SpriteRenderer enemyRenderer;
    float alpha, nextAttack;

    GameManager theGameManager;
    PlayerManager thePlayerManager;

	// Use this for initialization
	void Start () {
        maxHealth = 100.0f;
        currentHealth = maxHealth;
        enemyRenderer = GetComponent<SpriteRenderer>();
        theGameManager = FindObjectOfType<GameManager>();
        thePlayerManager = FindObjectOfType<PlayerManager>();
        nextAttack = 5f;
        //setCurrentState(GameState.appear);
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(currentState);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentHealth = -1;
        }
        if(currentHealth <= 0)
        {
            theGameManager.setCurrentState(GameManager.GameState.Wait);
            thePlayerManager.setCurrentState(PlayerManager.GameState.Wait);
            setCurrentState(GameState.die);
        }
        switch (currentState)
        {
            case GameState.wait:
                time = 0f;
                break;
            case GameState.appear:
                time += Time.deltaTime*2;
                Color tmp = enemyRenderer.color;
                alpha = Mathf.Lerp(0.0f, 1.0f, time);
                tmp.a = alpha;
                enemyRenderer.color = tmp;
                if(getStateElapsed() > 1.0f)
                {
                    setCurrentState(GameState.wait);
                    theGameManager.setCurrentState(GameManager.GameState.PauseBeforeStart);
                    time = 0;

                }
                break;
            case GameState.attack:
                Debug.Log("The enemy is now attacking");
                nextAttack -= Time.deltaTime;
                if(nextAttack <= 0.0f)
                {
                    Debug.Log("the ennemy performed an attack");
                    thePlayerManager.currentHealth -= 5;
                    nextAttack = 5.0f;
                }
                break;
            case GameState.die:
                currentHealth = 1;
                time += Time.deltaTime * 2;
                Color tmp2 = enemyRenderer.color;
                alpha = Mathf.Lerp(1.0f, 0.0f, time);
                tmp2.a = alpha;
                enemyRenderer.color = tmp2;
                if (getStateElapsed() > 1.0f)
                {
                    setCurrentState(GameState.wait);
                    theGameManager.setCurrentState(GameManager.GameState.OutroSequence);
                    time = 0;

                }
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
