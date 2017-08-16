using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Special for Singerella
//On hit, amplify all future damage.
public class SingerellaSpecial : MonoBehaviour {

    //The audioclip
    AudioClip sfx;

    //The audioSource
    AudioSource audioSource;

    //The indicator position
    Vector3 position;

    //On summon, show the words AMP.
    void Start()
    {
        position = Camera.main.WorldToScreenPoint(transform.position);
        GameObject indicator = Instantiate(Resources.Load("Prefabs/HitIndicator"), position, Quaternion.identity, GameObject.Find("Canvas").transform) as GameObject;
        indicator.GetComponent<DamageDisplayText>().affinity = Element.Fire;
        indicator.GetComponent<DamageDisplayText>().value = "AMPLIFY";
        indicator.GetComponent<DamageDisplayText>().StartCoroutine(
            indicator.GetComponent<DamageDisplayText>().BeginFade());
        audioSource = GameObject.Find("SFXSource").GetComponent<AudioSource>();
    }

	void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Monster"))
        {
            GameObject monster = col.collider.gameObject;

            monster.AddComponent<AmplifyStatus>();
            monster.GetComponent<AmplifyStatus>().MULTIPLIER = 3;

            //Play the sfx immediately/.
            audioSource.clip = sfx;
            audioSource.Play();

            //Spawn an indicator
            GameObject indicator = Instantiate(Resources.Load("Prefabs/HitIndicator"), position, Quaternion.identity, GameObject.Find("Canvas").transform) as GameObject;
            indicator.GetComponent<DamageDisplayText>().value = "AMPED";
            indicator.GetComponent<DamageDisplayText>().affinity = Element.Fire;
            indicator.GetComponent<DamageDisplayText>().StartCoroutine(
                indicator.GetComponent<DamageDisplayText>().BeginFade());
        }
    }
}
