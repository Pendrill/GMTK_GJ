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
    float alpha;

    GameManager theGameManager;

	// Use this for initialization
	void Start () {
        maxHealth = 100.0f;
        currentHealth = maxHealth;
        enemyRenderer = GetComponent<SpriteRenderer>();
        theGameManager = FindObjectOfType<GameManager>();
        //setCurrentState(GameState.appear);
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(currentState);
        switch (currentState)
        {
            case GameState.wait:
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

                }
                break;
            case GameState.attack:
                Debug.Log("The enemy is now attacking");
                break;
            case GameState.die:

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
