using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Controls mixing bombs
public class BombMixController : MonoBehaviour {

    //Game Manager
    public GameObject gameManager;

    //Spell library
    SpellLibrary sl;

    //Is still crafting
    public bool crafting = true;

    //Current combination of elements represented in a string.
    public string combination = "";

    //The bomb that is being manipulated 
    public GameObject bomb;

    //The spell that was finally selected
    public Spell spell = null;

    //The buttonmanager
    public ButtonManager bm;

    //Spell info with combination strings that are valid with current combination
    public List<Spell> validSpells;

    void Start()
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 3, 0));
        worldPos.z = 0;
        transform.position = worldPos;

        validSpells = new List<Spell>();
        sl = gameManager.GetComponent<SpellLibrary>();
        //ResetMix();
    }
	
	// Update is called once per frame
	void Update () {

        //Set whether or not we can still craft.
        crafting = isCrafting();

        //Set ButtonManager active bools
        bm.gameObject.active = crafting;
	}

    //Spawns the correct bomb based on the spell
    public void SpawnSpell()
    {
        if(spell != null)
        {
            GameObject temp = Instantiate(Resources.Load("SpellPrefabs/" + spell.name), bomb.transform.position, Quaternion.identity) as GameObject;
            temp.GetComponent<BombController>().DAMAGE = spell.damage;
            temp.GetComponent<BombController>().AFFINITY = spell.affinity;
            ResetMix();
        }
    }

    //Finalize spell. Selects the spell it exactly corresponds to.
    public void FinalizeSpell()
    {
        int index = 0;

        while (index < validSpells.Count)
        {
            if (!sl.CompareString(combination, validSpells[index].combination))
            {
                //Debug.Log("Removing: " + validSpells[index].combination);

                validSpells.Remove(validSpells[index]);

                index--;
            }
            else
            {
                //If it contained all the elements of our mixing pot AND is the same length, it is our spell.
                if(combination.Length == validSpells[index].combination.Length)
                {
                    spell = validSpells[index];
                }
            }
            index++;
        }
    }

    //Update valid spell list to the correct ones
    public void UpdateValidSpells()
    {
        int index = 0;

        while(index < validSpells.Count)
        {
            if(!sl.CompareString(combination, validSpells[index].combination))
            {
                //Debug.Log("Removing: " + validSpells[index].combination);

                validSpells.Remove(validSpells[index]);
                
                index--;
            }
            else
            {
                //Debug.Log("Keeping: " + validSpells[index].combination);
            }
            index++;
        }

        if(validSpells.Count == 0)
        {
            Debug.Log("Dud spell.");
            spell = new Spell("DudSpell", 0, Element.Neutral, "");
        }
        else if(validSpells.Count == 1)
        {
            Debug.Log("Found the spell: " + validSpells[0].name);
        }
        else
        {
            Debug.Log("Found no specific spell: " + validSpells.Count);
        }
    }

    //Check to see if there is still something we can craft
    public bool isCrafting()
    {
        if(combination.Length < 3)
        {
            return true;
        }
        return false;
    }

    //Restarts the mix, used on spawning the item
    public void ResetMix()
    {
        //Reset combinatory string
        combination = "";

        //Reset valid spells list
        validSpells.Clear();

        for(int i = 0; i < sl.spellLibrary.Length; i++)
        {
            validSpells.Add(sl.spellLibrary[i]);
        }
    }
}
