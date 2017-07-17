using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Script that displays the damage and fades it out.
public class DamageDisplayText : MonoBehaviour {

    //Assuming we are spawned where the projectile hit.

    //The bmc that holds the colors
    GameObject bmc;

    //The damage we used and dealt to the opponent.
    public string value;

    //The affinity of the spell we represent
    public Element affinity = Element.Neutral;

    //Color of text
    Color textColor;

    //Get the BMC
    void Start()
    {
        bmc = GameObject.Find("BombMixingPosition");

    }


	// Use this for initialization
	void Update () {
        textColor = SelectColor(affinity);
        
        GetComponent<Text>().text = value;		
	}

    //Helper that selects the correct color
    //Helper function that selects a color based on affinity
    public Color SelectColor(Element affinity)
    {
        switch (affinity)
        {
            case Element.Air: return bmc.GetComponent<BombMixController>().airColor;
            case Element.Fire: return bmc.GetComponent<BombMixController>().fireColor;
            case Element.Earth: return bmc.GetComponent<BombMixController>().earthColor;
            case Element.Water: return bmc.GetComponent<BombMixController>().waterColor;
            case Element.Neutral: return Color.black;
            default: return Color.black;
        }
    }


    //Helper coroutine that fades out the damage value, then kills the object
    public IEnumerator BeginFade()
    {
        //Shows and fades in 2 seconds.
        float timer = 2f;
        float transparency = 1.0f;
        while(timer > 0)
        {
            //Calculate our transparency
            transparency = Mathf.Lerp(0.0f, 1.0f, timer / 2f);
            //Debug.Log(transparency);

            //Apply transparency
            GetComponent<Text>().color =
                new Color(textColor.r, textColor.g, textColor.b, transparency);

            timer -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        Destroy(gameObject);
    }
}
