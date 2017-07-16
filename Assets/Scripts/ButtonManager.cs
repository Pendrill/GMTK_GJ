using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Button manager. Simply deactivates all buttons when an object is spawned.
public class ButtonManager : MonoBehaviour {


    //The list of buttons under this manager
    public Button[] buttons = new Button[4];

    //Sets the interactable value of all the buttons under this manager.
    public void SetAllButtons(bool interactable)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<SpawnDuplicate>().isActive = interactable;
        }
    }
}
