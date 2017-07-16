using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//RoomManager will allow us to switch to a random layout.

public class RoomManager : MonoBehaviour {

    //Selected index layout
    int selected = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Change the room on space, debugging purposes
            EnableRandomRoom();
        }
    }

	//Helper function that will enable a random layout that is NOT the same as last time.
    public void EnableRandomRoom()
    {
        DisableAllRooms();
        selected = SelectRandomIndex();
        transform.GetChild(selected).gameObject.SetActive(true);
    }
    
    //Helper function that will select a random index for us.
    public int SelectRandomIndex()
    {
        //Create a list we will shuffle
        List<ShuffleClass> possibleLayouts= new List<ShuffleClass>();

        //Add every element EXCEPT the one that was last selected
        for(int i = 0; i < transform.childCount; i++)
        {
            if (i != selected)
            {
                possibleLayouts.Add(new ShuffleClass(i, Random.Range(0f, 1f)));
            }    
        }

        //Sort the list by weight
        Shuffle(possibleLayouts);

        //Return the top of the list
        return possibleLayouts[0].value;

    }

    //Helper function that shuffles really badly. Naive approach. Quicksort can be implemented if time is allowed.
    public void Shuffle(List<ShuffleClass> list)
    {
        for(int i = 0; i < list.Count; i++)
        {
            Swap(list, i, Random.Range(0, list.Count));
        }
    }

    //Helper function that just swaps too elements in the list. Makes life easier
    private void Swap(List<ShuffleClass> list, int index1, int index2)
    {
        var temp = list[index2];
        list[index2] = list[index1];
        list[index1] = temp;
    }

    //Helper function that will disable all rooms for us
    public void DisableAllRooms()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}

//Object we use to shuffle. Contains a value and a weight.
public class ShuffleClass
{
    public int value;
    public float weight;

    public ShuffleClass(int inValue, float inWeight)
    {
        value = inValue;
        weight = inWeight;
    }
}
