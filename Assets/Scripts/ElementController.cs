using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Controls the element, allows for colliding into the mixing position, and returning to the backpack.
public class ElementController : MonoBehaviour {

    //The combination value of the elemnt
    public char value;

    //The button manager
    public ButtonManager bm;

    void Start()
    {
        //Identify the bm.
        bm = GameObject.Find("MixingUI").GetComponent<ButtonManager>();
    }

    void Update()
    {
        //Follow the player's mouse
        //First get the mouse x and y in the world space
        Vector2 mousePos = Input.mousePosition;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        worldPos.z = 0;
        transform.position = worldPos;

    }
	
	void OnTriggerEnter2D(Collider2D col)
    {
        //We have entered the bomb mixing area
        if(col.name == "BombMixingPosition")
        {
            col.GetComponent<BombMixController>().combination += value;
            col.GetComponent<BombMixController>().UpdateValidSpells();
            bm.SetAllButtons(true);
            Destroy(gameObject);
        }
        else if(col.name == "ReturnToBagZone")
        {
            bm.SetAllButtons(true);
            Destroy(gameObject);
        }
    }
}
