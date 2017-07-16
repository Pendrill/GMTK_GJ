using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopkeeperManager : MonoBehaviour {
    float maxHealth, currentHealth;
    public enum GameState { wait, appear, barter, attack, leave, die };
    public GameState currentState;
    float lastStateChange = 0.0f;
    public string[] barteringWords;

    float time, alpha, nextAttack;
    SpriteRenderer shopKeepRen;

    PlayerManager thePlayerManager;
    GameManager theGameManager;

	// Use this for initialization
	void Start () {
        maxHealth = 100.0f;
        currentHealth = maxHealth;
        theGameManager = FindObjectOfType<GameManager>();
        thePlayerManager = FindObjectOfType<PlayerManager>();
        shopKeepRen = GetComponent<SpriteRenderer>();
        nextAttack = 2.0f;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentHealth = -1;
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            theGameManager.setCurrentState(GameManager.GameState.Wait);
            thePlayerManager.setCurrentState(PlayerManager.GameState.Wait);
            setCurrentState(GameState.leave);
        }
        if (currentHealth <= 0)
        {
            theGameManager.setCurrentState(GameManager.GameState.Wait);
            thePlayerManager.setCurrentState(PlayerManager.GameState.Wait);
            setCurrentState(GameState.die);
        }
        switch (currentState)
        {
            case GameState.wait:

                break;
            case GameState.appear:
                time += Time.deltaTime * 2;
                Color tmp = shopKeepRen.color;
                alpha = Mathf.Lerp(0.0f, 1.0f, time);
                tmp.a = alpha;
                shopKeepRen.color = tmp;
                if (getStateElapsed() > 1.0f)
                {
                    setCurrentState(GameState.wait);
                    theGameManager.Fight.text = barteringWords[Random.Range(0, barteringWords.Length)];
                    theGameManager.setCurrentState(GameManager.GameState.PauseBeforeStart);
                    time = 0;

                }
                break;
            case GameState.barter:

                break;
            case GameState.attack:
                Debug.Log("You've met a terrible fate haven't you");
                nextAttack -= Time.deltaTime;
                if (nextAttack <= 0.0f)
                {
                    Debug.Log("the shopkeeper proceeded to destroy you");
                    thePlayerManager.currentHealth -= 5;
                    nextAttack = 2.0f;
                }
                break;
            case GameState.leave:
                time += Time.deltaTime * 2;
                Color tmp2 = shopKeepRen.color;
                alpha = Mathf.Lerp(1.0f, 0.0f, time);
                tmp2.a = alpha;
                shopKeepRen.color = tmp2;
                if (getStateElapsed() > 1.0f)
                {
                    setCurrentState(GameState.wait);
                    theGameManager.setCurrentState(GameManager.GameState.OutroSequence);
                    time = 0;

                }
                break;
            case GameState.die:
                currentHealth = 1;
                time += Time.deltaTime * 2;
                Color tmp3 = shopKeepRen.color;
                alpha = Mathf.Lerp(1.0f, 0.0f, time);
                tmp3.a = alpha;
                shopKeepRen.color = tmp3;
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
