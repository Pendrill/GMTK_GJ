using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopkeepBucket : MonoBehaviour {
    int needed = 3;
    int count;
    ShopkeeperManager theShopkeeperManager;
    PlayerManager thePlayerManager;
    GameManager theGameManager;
	// Use this for initialization
	void Start () {
        theGameManager = FindObjectOfType<GameManager>();
        thePlayerManager = FindObjectOfType<PlayerManager>();
        theShopkeeperManager = FindObjectOfType<ShopkeeperManager>();
	}
	
	// Update is called once per frame
	void Update () {
		if(count >= needed)
        {
            theShopkeeperManager.setCurrentState(ShopkeeperManager.GameState.leave);
            thePlayerManager.setCurrentState(PlayerManager.GameState.Wait);
            theGameManager.setCurrentState(GameManager.GameState.Wait);
            count = 0;
        }
	}
    private void OnCollisionEnter2D(Collision2D Bomb)
    {

            count += Bomb.gameObject.GetComponent<BombController>().TIER;
            Destroy(Bomb.gameObject);
        
    }
    
}
