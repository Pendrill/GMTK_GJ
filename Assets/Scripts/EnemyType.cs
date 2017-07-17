using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType : MonoBehaviour {
    public string monsterType;
    public Item[] drop;
    EnemyManager theEnemyManager;
    GameManager theGameManager;
	// Use this for initialization
	void Start () {
        theGameManager = FindObjectOfType<GameManager>();
        drop = new Item[theGameManager.totalDrop];
        if(Random.Range(1,11) <= 2)
        {
            theGameManager.totalDrop += 1;
            drop = new Item[theGameManager.totalDrop];
        }
        for (int i = 0; i < drop.Length; i++)
        {
            drop[i] = (Item)Random.Range(1, 5);
        }
        theEnemyManager = GetComponent<EnemyManager>();
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public float CalculateAndApplyDamage(BombController theBomb)
    {
        float damage = theBomb.DAMAGE;
     
        if (theBomb.bounces == 0)
        {
            theBomb.bounces = 1;
        }

        if(GetComponent<AmplifyStatus>() != null)
        {
            damage *= GetComponent<AmplifyStatus>().MULTIPLIER;
        }


        if (monsterType.Trim().Equals("Fire".Trim()))
        {
            if (theBomb.AFFINITY == Element.Water)
            {
                damage *= theBomb.bounces * 2;
            }
            else if (theBomb.AFFINITY == Element.Earth)
            {
                damage *= theBomb.bounces;
                damage /= 2;
            }
            else
            {
                damage *= theBomb.bounces;
            }
        }
        else if (monsterType.Trim().Equals("Water".Trim()))
        {
            if (theBomb.AFFINITY == Element.Air)
            {
                damage *= theBomb.bounces * 2;
            }
            else if (theBomb.AFFINITY == Element.Fire)
            {
                damage *= theBomb.bounces;
                damage /= 2;
            }
            else
            {
                damage *= theBomb.bounces;
            }
        }
        else if (monsterType.Trim().Equals("Air".Trim()))
        {
            if (theBomb.AFFINITY == Element.Earth)
            {
                damage *= theBomb.bounces * 2;
            }
            else if (theBomb.AFFINITY == Element.Water)
            {
                damage *= theBomb.bounces;
                damage /= 2;
            }
            else
            {
                damage *= theBomb.bounces;
            }
        }
        else if (monsterType.Trim().Equals("Earth".Trim()))
        {
            if (theBomb.AFFINITY == Element.Fire)
            {
                damage *= theBomb.bounces * 2;
            }
            else if (theBomb.AFFINITY == Element.Air)
            {
                damage *= theBomb.bounces;
                damage /= 2;
            }
            else
            {
                damage *= theBomb.bounces;
            }
        }
        //Debug.Log("The damage that was done was: " + damage);

        theEnemyManager.dealDamage(damage);
        return damage;
    }

    /*
    private void OnCollisionEnter2D(Collision2D bomb)
    {
        float damage = 1.0f;
        BombController theBomb = bomb.gameObject.GetComponent<BombController>();
        if(theBomb.bounces == 0)
        {
            theBomb.bounces = 1;
        }

        if (monsterType.Trim().Equals("Fire".Trim()))
        {
            if(theBomb.AFFINITY == Element.Water)
            {
                damage = theBomb.DAMAGE * theBomb.bounces * 2;
            }else if(theBomb.AFFINITY == Element.Earth)
            {
                damage = theBomb.DAMAGE * theBomb.bounces / 2;
            }else
            {
                damage = theBomb.DAMAGE * theBomb.bounces;
            }
        }
        else if (monsterType.Trim().Equals("Water".Trim()))
        {
            if(theBomb.AFFINITY == Element.Air)
            {
                damage = theBomb.DAMAGE * theBomb.bounces * 2;
            }
            else if(theBomb.AFFINITY == Element.Fire)
            {
                damage = theBomb.DAMAGE * theBomb.bounces / 2;
            }
            else
            {
                damage = theBomb.DAMAGE * theBomb.bounces;
            }
        }
        else if (monsterType.Trim().Equals("Air".Trim()))
        {
            if(theBomb.AFFINITY == Element.Earth)
            {
                damage = theBomb.DAMAGE * theBomb.bounces * 2;
            }
            else if(theBomb.AFFINITY == Element.Water)
            {
                damage = theBomb.DAMAGE * theBomb.bounces / 2;
            }
            else
            {
                damage = theBomb.DAMAGE * theBomb.bounces;
            }
        }
        else if (monsterType.Trim().Equals("Earth".Trim()))
        {
            if(theBomb.AFFINITY == Element.Fire)
            {
                damage = theBomb.DAMAGE * theBomb.bounces * 2;
            }
            else if(theBomb.AFFINITY == Element.Air)
            {
                damage = theBomb.DAMAGE * theBomb.bounces / 2;
            }
            else
            {
                damage = theBomb.DAMAGE * theBomb.bounces;
            }
        }
        Debug.Log("The damage that was done was: " + damage);
        
        theEnemyManager.dealDamage(damage);
    }*/
}
