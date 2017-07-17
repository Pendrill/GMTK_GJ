using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagmaSlideSpecial : MonoBehaviour {

    //The audioclip
    AudioClip sfx;

    //The audioSource
    AudioSource audioSource;

    //The indicator position
    Vector3 position;

    // Use this for initialization
    void Start () {
        position = Camera.main.WorldToScreenPoint(transform.position);
        GameObject indicator = Instantiate(Resources.Load("Prefabs/HitIndicator"), position, Quaternion.identity, GameObject.Find("Canvas").transform) as GameObject;
        indicator.GetComponent<DamageDisplayText>().affinity = Element.Fire;
        indicator.GetComponent<DamageDisplayText>().value = "WEAKEN";
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

            //Do the effect.
            //Get inventory
            GameObject player = GameObject.Find("Player");
            player.AddComponent<AmplifyStatus>();
            player.GetComponent<AmplifyStatus>().MULTIPLIER = 3;
            player.GetComponent<AmplifyStatus>().isPlayer = true;

            //Spawn an indicator
            GameObject indicator = Instantiate(Resources.Load("Prefabs/HitIndicator"), position, Quaternion.identity, GameObject.Find("Canvas").transform) as GameObject;
            indicator.GetComponent<DamageDisplayText>().value = "WEAKENED";
            indicator.GetComponent<DamageDisplayText>().affinity = Element.Fire;
            indicator.GetComponent<DamageDisplayText>().StartCoroutine(
                indicator.GetComponent<DamageDisplayText>().BeginFade());
        }
    }
}
