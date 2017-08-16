using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Controls mixing bombs
public class BombMixController : MonoBehaviour {

    //The sounds we play when holding and releasing bomb
    public AudioClip holdClip, releaseClip;

    //The sound for accepting a new ingredient and not getting a dud, and a dudClip
    public AudioClip ingredientClip, dudClip;

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

    //The text that displays the spell being fired
    public Text spellText;

    //Spell info with combination strings that are valid with current combination
    public List<Spell> validSpells;

    //The colors which will be used as the text
    public Color fireColor, waterColor, earthColor, airColor;

    //The colors that display the power of a spell
    public Color[] powerColors;

    void Start()
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 4, 0));
        worldPos.z = 0;
        transform.position = worldPos;
    
        //ResetMix();
        audioSource = GetComponent<AudioSource>();

        //Set colors on all button text
        bm.transform.GetChild(0).GetChild(0).GetComponent<Text>().color = fireColor;
        bm.transform.GetChild(1).GetChild(0).GetComponent<Text>().color = waterColor;

        bm.transform.GetChild(2).GetChild(0).GetComponent<Text>().color = earthColor;

        bm.transform.GetChild(3).GetChild(0).GetComponent<Text>().color = airColor;


    }

    //Helper function that plays the sound of the dud
    public void PlayDudClip()
    {
        audioSource.clip = dudClip;
        audioSource.Play();
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

    //Helper function that plays the sound of an ingredient being added.
    public void PlayIngredientClip()
    {
        audioSource.clip = ingredientClip;
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
            temp.GetComponent<BombController>().powerColors = powerColors;
            crafting = false;

            spellText.color = SelectColor(spell.affinity);
            spellText.text = spell.name;
            return temp;
        }
        return null;
    }

    //Helper function that selects a color based on affinity
    public Color SelectColor(Element affinity)
    {
        switch (affinity)
        {
            case Element.Air: return airColor;
            case Element.Fire: return fireColor;
            case Element.Earth: return earthColor;
            case Element.Water: return waterColor;
            case Element.Neutral: return Color.black;
            default: return Color.black;
        }
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

        if(validSpells == null)
        {
            validSpells = new List<Spell>();
            sl = gameManager.GetComponent<SpellLibrary>();
            for (int i = 0; i < sl.spellLibrary.Length; i++)
            {
                validSpells.Add(sl.spellLibrary[i]);
            }
        }

        while (index < validSpells.Count)
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
            //Debug.Log("Dud spell.");
            spell = new Spell("DudSpell", 0, Element.Neutral, "", combination.Length);
            PlayDudClip();
        }
        else if(validSpells.Count == 1)
        {
            //Debug.Log("Found the spell: " + validSpells[0].name);
            PlayIngredientClip();
        }
        else
        {
            //Debug.Log("Found no specific spell: " + validSpells.Count);
            PlayIngredientClip();
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

        //Reset valid spells list
        if (validSpells != null)
        {
            validSpells.Clear();
        }

        //Reset spell
        spell = null;

        //Reset combinatory string
        combination = "";

        for(int i = 0; i < sl.spellLibrary.Length; i++)
        {
            validSpells.Add(sl.spellLibrary[i]);
        }

        spellText.text = "";
    }
}
