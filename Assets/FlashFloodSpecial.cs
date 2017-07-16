using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashFloodSpecial : MonoBehaviour {

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
        indicator.GetComponent<DamageDisplayText>().affinity = Element.Water;
        indicator.GetComponent<DamageDisplayText>().value = "BONUS";
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
            inventory = GameObject.Find("Inventory").GetComponent<InventoryScript>();

            //Select random element
            int index = Random.Range(0, 4);


            //Reduce random item by one 
            inventory.ChangeItemValue(IndexToItem(index), 2);

            //Spawn an indicator
            GameObject indicator = Instantiate(Resources.Load("Prefabs/HitIndicator"), position, Quaternion.identity, GameObject.Find("Canvas").transform) as GameObject;
            indicator.GetComponent<DamageDisplayText>().value = "+2";
            indicator.GetComponent<DamageDisplayText>().affinity = ItemToAffinity(IndexToItem(index));
            indicator.GetComponent<DamageDisplayText>().StartCoroutine(
                indicator.GetComponent<DamageDisplayText>().BeginFade());
        }
    }

    //Helper function that converts itemType into affinity
    public Element ItemToAffinity(Item item)
    {
        switch (item)
        {
            case Item.EmberPebble: return Element.Fire;
            case Item.MermaidScale: return Element.Water;
            case Item.NimbusQuill: return Element.Air;
            case Item.GaiaSeed: return Element.Earth;
            default:
                Debug.Log("Error. Void Element from Heavy Rain.");
                break;
        }
        return Element.Invalid;
    }

    //Helper funtion that converts index into itemType
    public Item IndexToItem(int index)
    {
        switch (index)
        {
            case 0: return Item.EmberPebble;
            case 1: return Item.GaiaSeed;
            case 2: return Item.NimbusQuill;
            case 3: return Item.MermaidScale;
            default:
                Debug.Log("Error. Invalid item to decrease from Heavy Rain.");
                break;
        }
        return Item.Void;
    }
}
