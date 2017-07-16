using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Button manager. Simply deactivates all buttons when an object is spawned.
public class ButtonManager : MonoBehaviour {

    //The audiosource for the button audio
    AudioSource audioSource;

    //The audio clips for each button
    public AudioClip[] pickupClips = new AudioClip[4];

    //The list of buttons under this manager
    public Button[] buttons = new Button[4];

<<<<<<< HEAD
        //Always set to opposite of whether or not something has been spawned.
        //SetAllButtons(!isSpawned);
	}
=======
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    //Plays the corresponding sound
    public void PlayPickupSound(int index)
    {
        audioSource.clip = pickupClips[index];
        audioSource.Play();
    }

>>>>>>> d65c85fe3e973bf914a3d327ae5bf27827f5cac2

    //Sets the interactable value of all the buttons under this manager.
    public void SetAllButtons(bool interactable)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
<<<<<<< HEAD
            buttons[i].GetComponent<SpawnDuplicate>().gameObject.SetActive(interactable);
=======
            //buttons[i].GetComponent<SpawnDuplicate>().isActive = interactable;
            buttons[i].gameObject.SetActive(interactable);
>>>>>>> d65c85fe3e973bf914a3d327ae5bf27827f5cac2
        }
    }
}
