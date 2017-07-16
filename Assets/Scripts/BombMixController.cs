using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Controls mixing bombs
public class BombMixController : MonoBehaviour {

    //The sounds we play when holding and releasing bomb
    public AudioClip holdClip, releaseClip;

    //Our audioSource
    AudioSource audioSource;

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

        audioSource = GetComponent<AudioSource>();
    }

    //Helper function that plays the sound of the hold
    public void PlayHoldClip()
    {
        audioSource.clip = holdClip;
        audioSource.Play();
    }

    //Helper function that plays the sound of the release
    public void PlayReleaseClip()
    {
        audioSource.clip = releaseClip;
        audioSource.Play();
    }
	
	// Update is called once per frame
	void Update () {

        //If the combinatory string is 3 length
        if (!isCrafting())
        {
            bm.SetAllButtons(false);
        }
	}

    //Spawns the correct bomb based on the spell
    public GameObject SpawnSpell()
    {
        if(spell != null)
        {
            GameObject temp = Instantiate(Resources.Load("SpellPrefabs/" + spell.name), bomb.transform.position, Quaternion.identity) as GameObject;
            temp.GetComponent<BombController>().DAMAGE = spell.damage;
            temp.GetComponent<BombController>().AFFINITY = spell.affinity;
            temp.GetComponent<BombController>().TIER = spell.tier;
            crafting = false;
            return temp;
        }
        return null;
    }

    //Finalize spell. Selects the spell it exactly corresponds to.
    public void FinalizeSpell()
    {
        int index = 0;
        bm.SetAllButtons(false);
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
            spell = new Spell("DudSpell", 0, Element.Neutral, "", combination.Length);
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
        //Reset button visibility
        bm.SetAllButtons(true);

        //Reset spell
        spell = null;

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
