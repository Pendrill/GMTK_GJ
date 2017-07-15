using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Controller for the fire button.
//Moves button on top of mixing pot.
//Has method to exit mixing mode and go to fire mode.
public class FireButtonController : MonoBehaviour {

    //The bomb mixing controller object
    public GameObject bmc;

    //Whether or not we are in fire mode
    public bool fireMode = false;
	
	// Update is called once per frame
	void Update () {
        transform.position = Camera.main.WorldToScreenPoint(bmc.transform.position);

        float distance = Vector3.Distance(Input.mousePosition,  transform.position);
        Debug.Log(distance);
        if (Input.GetButtonDown("Fire1") && distance < 15) 
        {
            Debug.Log("FIRING");
            fireMode = true;
        }


        if (fireMode)
        {
            if (Input.GetButton("Fire1"))
            {
                fireMode = true;
            }
            else
            {
                fireMode = false;
            }
        }
    }

    //The method to move to firing mode
    public void FireMode()
    {
        bmc.GetComponent<BombMixController>().FinalizeSpell();
        bmc.GetComponent<BombMixController>().SpawnSpell();
        fireMode = true;
    }
}
