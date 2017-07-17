using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** Script that contains a list of all valid spells. 
    Contains utilities for comparing as well.
*/
public class SpellLibrary : MonoBehaviour {

    //The bomb mix controller
    public BombMixController bmc;

    //Number of spells
    public int SPELL_NUMBER = 24;

    //Spell info with combination strings
    public Spell[] spellLibrary;

    //Make it the same length as spell library
    public GameObject[] spellPrefabs;

    public int BASE_DAMAGE = 10;
    public float T2_MULTIPLIER = 1.25f, T2_PURE_MULTIPLIER = 1.5f,
        T3_MULTIPLIER = 2f, T3_PURE_MULTIPLIER = 0f;


    void Start()
    {
        InitList();
    }


    public void InitList()
    {
        //Init brand new array
        spellLibrary = new Spell[SPELL_NUMBER];

        //Assign all values
        
        //Tier 1 spells
        spellLibrary[0] = new Spell("EmberPebble", BASE_DAMAGE, Element.Fire, "F", 1);
        spellLibrary[1] = new Spell("GaiaSeed", BASE_DAMAGE, Element.Earth, "E", 1);
        spellLibrary[2] = new Spell("NimbusQuill", BASE_DAMAGE, Element.Air, "A", 1);
        spellLibrary[3] = new Spell("MermaidScale", BASE_DAMAGE, Element.Water, "W", 1);

        //Tier 2  mixed spells
        spellLibrary[4] = new Spell("GaiaPebble", (int)(T2_MULTIPLIER * (float)BASE_DAMAGE), Element.Neutral, "FE", 2);
        spellLibrary[5] = new Spell("EmberScale", (int)(T2_MULTIPLIER * (float)BASE_DAMAGE), Element.Neutral, "FW", 2);
        spellLibrary[6] = new Spell("NimbusSeed", (int)(T2_MULTIPLIER * (float)BASE_DAMAGE), Element.Neutral, "EA", 2);
        spellLibrary[7] = new Spell("MermaidQuill", (int)(T2_MULTIPLIER * (float)BASE_DAMAGE), Element.Neutral, "AW", 2);

        //Tier 2 pure spells
        spellLibrary[8] = new Spell("EmberStone", (int)(T2_PURE_MULTIPLIER * (float)BASE_DAMAGE), Element.Fire, "FF", 2);
        spellLibrary[9] = new Spell("GaiaRoot", (int)(T2_PURE_MULTIPLIER * (float)BASE_DAMAGE), Element.Earth, "EE", 2);
        spellLibrary[10] = new Spell("MermaidFin", (int)(T2_PURE_MULTIPLIER * (float)BASE_DAMAGE), Element.Water, "WW", 2);
        spellLibrary[11] = new Spell("NimbusNimbus", (int)(T2_PURE_MULTIPLIER * (float)BASE_DAMAGE), Element.Air, "AA", 2);

        //Tier 3 mixed spells
        spellLibrary[12] = new Spell("MagmaSlide", (int)(T3_MULTIPLIER * (float)BASE_DAMAGE), Element.Fire, "FFE", 3);
        spellLibrary[13] = new Spell("SteamyShower", (int)(T3_MULTIPLIER * (float)BASE_DAMAGE), Element.Fire, "FFW", 3);
        spellLibrary[14] = new Spell("SummerBonfire", (int)(T3_MULTIPLIER * (float)BASE_DAMAGE), Element.Earth, "FEE", 3);
        spellLibrary[15] = new Spell("HeavyRain", (int)(T3_MULTIPLIER * (float)BASE_DAMAGE), Element.Water, "FWW", 3);
        spellLibrary[16] = new Spell("SandStorm", (int)(T3_MULTIPLIER * (float)BASE_DAMAGE), Element.Air, "AAE", 3);
        spellLibrary[17] = new Spell("LightningStrike", (int)(T3_MULTIPLIER * (float)BASE_DAMAGE), Element.Air, "AAW", 3);
        spellLibrary[18] = new Spell("RockRumble", (int)(T3_MULTIPLIER * (float)BASE_DAMAGE), Element.Earth, "AEE", 3);
        spellLibrary[19] = new Spell("RiptideRage", (int)(T3_MULTIPLIER * (float)BASE_DAMAGE), Element.Water, "AWW", 3);

        //Tier 3 pure spells
        spellLibrary[20] = new Spell("Singerella", (int)(T3_PURE_MULTIPLIER * (float)BASE_DAMAGE), Element.Fire, "FFF", 3);
        spellLibrary[21] = new Spell("TimeTornado", (int)(T3_PURE_MULTIPLIER * (float)BASE_DAMAGE), Element.Air, "AAA", 3);
        spellLibrary[22] = new Spell("ShatteredEarth", (int)(T3_PURE_MULTIPLIER * (float)BASE_DAMAGE), Element.Earth, "EEE", 3);
        spellLibrary[23] = new Spell("FlashFlood", (int)(T3_PURE_MULTIPLIER * (float)BASE_DAMAGE), Element.Water, "WWW", 3);

        //Tell bomb mix we are finished
        //bmc.ResetMix();

    }

    //Compare strings to see if elements of s1 is contained in s2.
    public bool CompareString(string s1, string s2)
    {
        //If greater length, return false.
        if (s1.Length > s2.Length)
            return false;

        //Bool array that corresponds to each character in s1
        bool[] isThere = new bool[s1.Length];
        for(int i = 0; i < isThere.Length; i++)
        {
            isThere[i] = false;
        }

        //Index value of string 1
        int index = 0;

        //Loop through the strings. If an element is a match, set a bool as true, remove the match, and continue
        while(index < isThere.Length)
        {
            for (int i = 0; i < s2.Length; i++)
            {
                //Debug.Log("Comparing " + s1[index] + " : " + s2[i]);
                if(s1[index] == s2[i])
                {
                    isThere[index] = true;

                    s2 = s2.Remove(i, 1);
                    //Debug.Log(s2);
                    break;
                }
            }
            index++;
        }

        //Compare to get returnBool
        bool returnBool = true;
        for(int i = 0; i < isThere.Length; i++)
        {
            //If we use && all the way through the array, it will be false if one gets false, all will be true.
            returnBool = returnBool && isThere[i];
        }

        return returnBool;
    }
}

//Generic element enum
public enum Element
{
    Invalid, Fire, Earth, Air, Water, Neutral
}

//Generic spell class
public class Spell
{
    //Name of spell
    public string name = "UNNAMED";

    //Damage spell does on impact
    public int damage = 0;

    //Affinity spell has
    public Element affinity = Element.Invalid;

    //Combination string that tells us its composition
    public string combination = "";

    //Discovered
    public bool discovered = false;

    //Tier
    public int tier = 0;

    //Constructor for spells
    public Spell(string inName, int inDamage, Element inAffinity, string inCombination, int inTier)
    {
        name = inName;
        damage = inDamage;
        affinity = inAffinity;
        combination = inCombination;
        tier = inTier;
    }
}
