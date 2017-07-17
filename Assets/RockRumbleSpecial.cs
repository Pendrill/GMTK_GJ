using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockRumbleSpecial : MonoBehaviour
{

    //SFX to play on usage
    public AudioClip sfx;

    //AudioSource
    AudioSource audioSource;

    //Player inventory
    InventoryScript inventory;

    //Indicator position we are using
    Vector3 position;

    // Use this for initialization
    void Start()
    {
        position = Camera.main.WorldToScreenPoint(transform.position);
        GameObject indicator = Instantiate(Resources.Load("Prefabs/HitIndicator"), position, Quaternion.identity, GameObject.Find("Canvas").transform) as GameObject;
        indicator.GetComponent<DamageDisplayText>().affinity = Element.Earth;
        indicator.GetComponent<DamageDisplayText>().value = "SLOW";
        indicator.GetComponent<DamageDisplayText>().StartCoroutine(
            indicator.GetComponent<DamageDisplayText>().BeginFade());
        audioSource = GameObject.Find("SFXSource").GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Monster"))
        {

            //Play the sfx immediately/.
            audioSource.clip = sfx;
            audioSource.Play();

            //Apply effect
            col.collider.GetComponent<EnemyManager>().attackTime = Mathf.Clamp(col.collider.GetComponent<EnemyManager>().attackTime * 2, 1, 10);

            //Spawn an indicator
            GameObject indicator = Instantiate(Resources.Load("Prefabs/HitIndicator"), position, Quaternion.identity, GameObject.Find("Canvas").transform) as GameObject;
            indicator.GetComponent<DamageDisplayText>().value = "SLOWED";
            indicator.GetComponent<DamageDisplayText>().affinity = Element.Earth;
            indicator.GetComponent<DamageDisplayText>().StartCoroutine(
                indicator.GetComponent<DamageDisplayText>().BeginFade());
        }
    }
}
