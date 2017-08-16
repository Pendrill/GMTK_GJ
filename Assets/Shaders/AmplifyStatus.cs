using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmplifyStatus : MonoBehaviour
{
    //The amplify value, changes based on spell
    public int MULTIPLIER = 1;

    //The bool if we are on the player
    public bool isPlayer = false;

    //The timer
    float timer = 30f;

    void Update()
    {
        if (isPlayer)
        {
            timer -= Time.deltaTime;
            if(timer < 0)
            {
                Destroy(this);
            }
        }
    }

}
