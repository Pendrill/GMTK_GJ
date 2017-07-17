using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Controls the bomb/concotion that is being thrown
public class BombController : MonoBehaviour {

    //The outer glow of the bomb
    GameObject glowObject;

    //The color of the glow
    Color glowColor;

    //The bouncing sound of abomb
    public AudioClip bounceClip;

    //The hit sound of a bomb
    public AudioClip hitClip;

    //The audio source of a bomb
    AudioSource audioSource;

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

    //The power colors
    public Color[] powerColors;

    //Size of the bomb
    public float bombSize;
    float MAX_SIZE;

    //Bool that is enabled when its dead
    bool isDead = false;

	// Use this for initialization
	void Start () {
        bombSize = transform.localScale.x;
        MAX_SIZE = bombSize;
        audioSource = GetComponent<AudioSource>();
        glowObject = transform.GetChild(0).gameObject;
        
	}
	
	// Update is called once per frame
	void Update () {
		if(LIFE > 0f && !isDead)
        {
            LIFE-= Time.deltaTime;
        }
        else if(LIFE <= 0f)
        {
            Destroy(gameObject);
        }

        //Change the color of the glow based on bounces
        glowObject.GetComponent<SpriteRenderer>().color = SelectGlowColor(bounces);
        
        bombSize = MAX_SIZE * (LIFE / MAX_LIFE);
        transform.localScale = new Vector3(bombSize, bombSize, 1);
        GetComponent<TrailRenderer>().startWidth = 0.5f * (LIFE / MAX_LIFE);
        GetComponent<TrailRenderer>().endWidth = 0;
	}

    //Selects a new color based on a number
    public Color SelectGlowColor(int bounces)
    {
        //The index is increased every 2 bounces
        int index = bounces/2;

        //Cap the index to the highgest number in the array
        index = Mathf.Min(index, powerColors.Length - 1);

        //Return the color from the power colors
        return powerColors[index];
    }


    //Plays hit clip
    void PlayHitClip()
    {
        audioSource.clip = hitClip;
        audioSource.Play();
    }

    //Plays bounce clip
    void PlayBounceClip()
    {
        audioSource.clip = bounceClip;
        audioSource.Play();
    }

    //Coroutine to let object stay but be ethereal.
    public IEnumerator KillBomb()
    {
        //Spawn the damage value where we hit the enemy
        GameObject damageIndicator = Instantiate(Resources.Load("Prefabs/HitIndicator"), Camera.main.WorldToScreenPoint(transform.position), Quaternion.identity, GameObject.Find("Canvas").transform) as GameObject;
        damageIndicator.GetComponent<DamageDisplayText>().affinity = AFFINITY;
        damageIndicator.GetComponent<DamageDisplayText>().value = DAMAGE + "";
        damageIndicator.GetComponent<DamageDisplayText>().StartCoroutine(damageIndicator.GetComponent<DamageDisplayText>().BeginFade());


        //Give it 3 seconds to play sounds and other effects to die down
        float timer = 3f;
        isDead = true;
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Renderer>().enabled = false;

        glowObject.GetComponent<Renderer>().enabled = false;
       // GetComponent<TrailRenderer>().enabled = false;
        PlayHitClip();
        while(timer > 0f)
        {
            timer -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //If we hit the monster
        if (col.collider.CompareTag("Monster"))
        {
            Camera.main.GetComponent<CameraShake>().ShakeCamera(0.5f, 0.3f);
            if(col.collider.GetComponent<EnemyType>() != null)
            {
                DAMAGE = (int)col.collider.gameObject.GetComponent<EnemyType>().CalculateAndApplyDamage(this);
            }
            else
            {
                DAMAGE = 1 * bounces + TIER;
            }
            
            StartCoroutine(KillBomb());
        }
        else
        {
            PlayBounceClip();
            bounces++;
        }
    }


}
