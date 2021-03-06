﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopkeeperManager : MonoBehaviour {
    float maxHealth, currentHealth;
    public enum GameState { wait, appear, barter, attack, leave, die };
    public GameState currentState;
    float lastStateChange = 0.0f;
    public string[] barteringWords;
    public GameObject Bucket;

    float time, alpha, nextAttack;
    SpriteRenderer shopKeepRen,bucketRen;

    PlayerManager thePlayerManager;
    GameManager theGameManager;

	// Use this for initialization
	void Start () {
        maxHealth = 100.0f;
        currentHealth = maxHealth;
        theGameManager = FindObjectOfType<GameManager>();
        thePlayerManager = FindObjectOfType<PlayerManager>();
        shopKeepRen = GetComponent<SpriteRenderer>();
        bucketRen = Bucket.GetComponent<SpriteRenderer>();
        nextAttack = 2.0f;
        
	}
	
    //Helper function to apply alpha to all children
    public void AllChildrenAlpha(float alpha)
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            Color temp = transform.GetChild(i).GetComponent<SpriteRenderer>().color;
            transform.GetChild(i).GetComponent<SpriteRenderer>().color =
                new Color(temp.r, temp.g, temp.b, alpha);
        }
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
            time = 0;
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
                AllChildrenAlpha(tmp.a);
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
                time += Time.deltaTime * 2;
                Color tmp4 = bucketRen.color;
                alpha = Mathf.Lerp(1.0f, 0.0f, time);
                tmp4.a = alpha;
                AllChildrenAlpha(tmp4.a);
                //Debug.Log("You've met a terrible fate haven't you");
                nextAttack -= Time.deltaTime;
                if (nextAttack <= 0.0f)
                {
                    //Debug.Log("the shopkeeper proceeded to destroy you");
                    thePlayerManager.currentHealth -= 5;
                    Camera.main.GetComponent<CameraShake>().ShakeCamera(0.2f, 0.1f);
                    nextAttack = 2.0f;
                }
                break;
            case GameState.leave:
                time += Time.deltaTime * 2;
                Color tmp2 = shopKeepRen.color;
                alpha = Mathf.Lerp(1.0f, 0.0f, time);
                tmp2.a = alpha;
                shopKeepRen.color = tmp2;
                AllChildrenAlpha(tmp2.a);
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
                AllChildrenAlpha(tmp3.a);
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

    //
    private void OnCollisionEnter2D(Collision2D col)
    {
        //If it is a bomb
        if(col.gameObject.layer == 8)
        {
            Bucket.GetComponent<Collider2D>().enabled = false;
            setCurrentState(GameState.attack);
            currentHealth -= (1 * col.gameObject.GetComponent<BombController>().bounces) + col.gameObject.GetComponent<BombController>().TIER;
        }
    }
}
