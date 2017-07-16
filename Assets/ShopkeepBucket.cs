using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopkeepBucket : MonoBehaviour {
    int needed = 3;
    public int count;
    ShopkeeperManager theShopkeeperManager;
    PlayerManager thePlayerManager;
    GameManager theGameManager;
    bool once = true;
	// Use this for initialization
	void Start () {
        theGameManager = FindObjectOfType<GameManager>();
        thePlayerManager = FindObjectOfType<PlayerManager>();
        theShopkeeperManager = FindObjectOfType<ShopkeeperManager>();

        Physics2D.IgnoreLayerCollision(9, 10, true);
	}
	
	// Update is called once per frame
	void Update () {
		if(count >= needed && once )
        {
            once = false;
            theShopkeeperManager.setCurrentState(ShopkeeperManager.GameState.leave);
            thePlayerManager.setCurrentState(PlayerManager.GameState.Wait);
            theGameManager.setCurrentState(GameManager.GameState.Wait);
            //count = 0;
        }
	}
    private void OnTriggerEnter2D(Collider2D Bomb)
    {

        if(Bomb.gameObject.layer == 8)
        {
            count += Bomb.gameObject.GetComponent<BombController>().TIER;
            Destroy(Bomb.gameObject);
        }
    }
}
