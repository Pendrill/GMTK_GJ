using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Controls the bomb/concotion that is being thrown
public class BombController : MonoBehaviour {

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

    //Size of the bomb
    public float bombSize;
    float MAX_SIZE;

	// Use this for initialization
	void Start () {
        bombSize = transform.localScale.x;
        MAX_SIZE = bombSize;
        audioSource = GetComponent<AudioSource>();
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
        //Give it 3 seconds to play sounds and other effects to die down
        float timer = 3f;
       
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Renderer>().enabled = false;
        GetComponent<TrailRenderer>().enabled = false;
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
            StartCoroutine(KillBomb());
        }
        else
        {
            PlayBounceClip();
            bounces++;
        }
    }


}
