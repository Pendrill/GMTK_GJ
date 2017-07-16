using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The inventory of the player
public class InventoryScript : MonoBehaviour {

    //Number of possible items
    public int UNIQUE_ITEMS = 5;

    //Number of each item you start with
    public int ITEM_INIT_VALUE = 3;

    //Simple inventory, an array of the possible items and values of how many he/she has
    int[] containedItems;


	// Use this for initialization
	void Start () {
        containedItems = new int[UNIQUE_ITEMS];

        InitInventory();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    

    //Get the values of an item
    public int itemNumber(Item item)
    {
        int index = 0;
        switch (item)
        {
            case Item.EmberPebble:
                index = 0;
                break;
            case Item.GaiaSeed:
                index = 1;
                break;
            case Item.NimbusQuill:
                index = 2;
                break;
            case Item.MermaidScale:
                index = 3;
                break;
            case Item.Potion:
                index = 4;
                break;
            default:
                Debug.Log("Error: No item chosen.");
                break;
        }
        return containedItems[index];
    }

    //Check to see if there are any of hte given item in inventory
    public bool hasItem(Item item)
    {
        int index = 0;
        switch (item)
        {
            case Item.EmberPebble:
                index = 0;
                break;
            case Item.GaiaSeed:
                index = 1;
                break;
            case Item.NimbusQuill:
                index = 2;
                break;
            case Item.MermaidScale:
                index = 3;
                break;
            case Item.Potion:
                index = 4;
                break;
            default:
                Debug.Log("Error: No item chosen.");
                break;
        }
        if (containedItems[index] > 0)
        {
            return true;
        }
        return false;
    }

    //Init values
    public void InitInventory()
    {
        ChangeItemValue(Item.EmberPebble, ITEM_INIT_VALUE);
        ChangeItemValue(Item.GaiaSeed, ITEM_INIT_VALUE);
        ChangeItemValue(Item.NimbusQuill, ITEM_INIT_VALUE);
        ChangeItemValue(Item.MermaidScale, ITEM_INIT_VALUE);
        ChangeItemValue(Item.Potion, ITEM_INIT_VALUE);
    }

    //Helper function to make using inventory easier
    public void ChangeItemValue(Item item, int value)
    {
        int index = 0;
        switch (item)
        {
            case Item.Void: Debug.Log("Error: Invalid item choice.");
                break;
            case Item.EmberPebble:  index = 0;
                break;
            case Item.GaiaSeed:  index = 1;
                break;
            case Item.NimbusQuill:  index = 2;
                break;
            case Item.MermaidScale: index = 3;
                break;
            case Item.Potion: index = 4;
                break;
            default:
                Debug.Log("Error: No item chosen.");
                break;
        }
        containedItems[index] += value;
    }
}

public enum Item { Void, EmberPebble, GaiaSeed, NimbusQuill, MermaidScale, Potion };
