using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Spawn's a duplicate from the button so that the player can drag it to the bomb
public class SpawnDuplicate : MonoBehaviour {

    //Player inventory
    public InventoryScript inventory;

    //The Item in this button
    public Item itemType;

    //The element this button will spawn
    public GameObject element;

    //The active sprite
    public Sprite activeSprite;

    //The inactive sprite
    public Sprite inactiveSprite;

    //If false, turn to inactive sprite
    public bool isActive = true;

    //The buttonmanager
    public ButtonManager bm;

    //The number indicator corresponding to this button
    public Text text;

    void Start()
    {
        text =  transform.GetChild(0).GetComponent<Text>();
    }

    void Update()
    {
        //When active, use appropriate sprite and allow us to press it
        if (isActive)
        {
            GetComponent<Image>().sprite = activeSprite;
            GetComponent<Button>().interactable = true;
        }
        else //When inactive, use appropriate sprite, and don't allow us to press it. 
        {
            GetComponent<Image>().sprite = inactiveSprite;
            GetComponent<Button>().interactable = false;
        }
        text.text = inventory.itemNumber(itemType) + "";
    }

    //Creates a duplicate element to be mixed
	public void Duplicate()
    {
        //If we have the item
        if (inventory.hasItem(itemType))
        {
            //Reduce the number of this item we own
            inventory.ChangeItemValue(itemType, -1);

            //Buttonmanager is spawned.
            bm.SetAllButtons(false);

            //Calculate the position to spawn our objects at
            Vector3 worldPosition = Vector3.zero;
            worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPosition.z = 0;

            GameObject temp = Instantiate(element, worldPosition, Quaternion.identity);
        }
        else //If we don't have this item, play a sound
        {

        }
    }
}
