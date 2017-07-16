using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Controls the bomb/concotion that is being thrown
public class BombController : MonoBehaviour {

    //Damage it will deal on hit
    public int DAMAGE = 0;

    //Affinity of spell
    public Element AFFINITY = Element.Invalid;

    //Timer of spell
    public float LIFE = 10f;
    float MAX_LIFE = 10f;

    //Tier of the spell
    public int TIER = 0;

    //Bounces on bomb
    public int bounces = 0;

    //Size of the bomb
    public float bombSize;
    float MAX_SIZE;

	// Use this for initialization
	void Start () {
        bombSize = transform.localScale.x;
        MAX_SIZE = bombSize;
	}
	
	// Update is called once per frame
	void Update () {
		if(LIFE > 0f)
        {
            LIFE-= Time.deltaTime;
        }
        else if(LIFE <= 0f)
        {
            Destroy(gameObject);
        }
        
        bombSize = MAX_SIZE * (LIFE / MAX_LIFE);
        transform.localScale = new Vector3(bombSize, bombSize, 1);
        GetComponent<TrailRenderer>().startWidth = 0.5f * (LIFE / MAX_LIFE);
        GetComponent<TrailRenderer>().endWidth = 0;
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        //If we hit the monster
        if (col.collider.CompareTag("Monster"))
        {
            Camera.main.GetComponent<CameraShake>().ShakeCamera(0.5f, 0.3f);
            Destroy(gameObject);
        }
        else
        {
            bounces++;
        }
    }


}
