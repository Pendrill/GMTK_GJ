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

    //The spell bomb in question
    public GameObject bomb;

    void Start()
    {
        //Ignore collision between UI and spell
        Physics2D.IgnoreLayerCollision(5, 8);




    }
	
    void OnMouseDown()
    {
        //Debug.Log("Click");
        bmc.GetComponent<BombMixController>().FinalizeSpell();
        bomb = bmc.GetComponent<BombMixController>().SpawnSpell();
        if (bomb != null)
        {
            bmc.GetComponent<BombMixController>().PlayHoldClip();
            fireMode = true;
        }
        else
        {
            bmc.GetComponent<BombMixController>().ResetMix();
        }
    }

    void OnMouseOver()
    {
        //Debug.Log("Hover");
    }

	// Update is called once per frame
	void Update () {
        Vector3 newPos = bmc.transform.position;
        newPos.z = -0.1f;
        transform.position = newPos;
        float distance = Vector3.Distance(Input.mousePosition,  transform.position);
        //Debug.Log(distance);
        if (Input.GetButtonDown("Fire1")) 
        {
            //Debug.Log("FIRING");
            
        }


        if (fireMode)
        {
            if(bomb == null)
            {
                GetComponent<LineRenderer>().SetPosition(1, transform.position);
                bmc.GetComponent<BombMixController>().ResetMix();
                bomb = null;
                fireMode = false;
            }
            else if (Input.GetButton("Fire1"))
            {
                fireMode = true;
                GetComponent<LineRenderer>().SetPosition(0, transform.position);

                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0;


                GetComponent<LineRenderer>().SetPosition(1, mousePos);
            }
            else if(Input.GetButtonUp("Fire1"))
            {
                //Calculate the ray that is made from pullback of the mouse
                bmc.GetComponent<BombMixController>().PlayReleaseClip();
                Vector3 releasePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 direction = (transform.position - releasePosition).normalized;
                float magnitude = direction.magnitude;
                GetComponent<LineRenderer>().SetPosition(1, transform.position);
                bomb.GetComponent<Rigidbody2D>().AddForce(direction * magnitude * 30f, ForceMode2D.Impulse);
                Camera.main.GetComponent<CameraShake>().ShakeCamera(0.1f, 0.1f);
                //Debug.Log("Direction: " + direction + " Magnitude: " + magnitude);
                bmc.GetComponent<BombMixController>().ResetMix();
                bomb = null;
                fireMode = false;
            }
        }
    }
}
