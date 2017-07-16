using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType : MonoBehaviour {
    public string monsterType;
    public Item drop;
	// Use this for initialization
	void Start () {
        drop = (Item)Random.Range(1, 5);
	}
	
	// Update is called once per frame
	void Update () {
        
	}
    private void OnCollisionEnter2D(Collision2D bomb)
    {
        BombController theBomb = bomb.gameObject.GetComponent<BombController>();
        if (monsterType.Trim().Equals("Fire".Trim()))
        {
            if(theBomb.AFFINITY == Element.Water)
            {

            }else if(theBomb.AFFINITY == Element.Earth)
            {

            }else
            {

            }
        }
        else if (monsterType.Trim().Equals("Water".Trim()))
        {
            if(theBomb.AFFINITY == Element.Air)
            {

            }else if(theBomb.AFFINITY == Element.Fire)
            {

            }else
            {

            }
        }
        else if (monsterType.Trim().Equals("Air".Trim()))
        {
            if(theBomb.AFFINITY == Element.Earth)
            {
                
            }else if(theBomb.AFFINITY == Element.Water)
            {

            }else
            {

            }
        }
        else if (monsterType.Trim().Equals("Earth".Trim()))
        {
            if(theBomb.AFFINITY == Element.Fire)
            {

            }else if(theBomb.AFFINITY == Element.Air)
            {

            }else
            {

            }
        }
    }
}
