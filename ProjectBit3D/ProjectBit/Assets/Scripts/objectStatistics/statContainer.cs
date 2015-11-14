using UnityEngine;
using System.Collections;

public class statContainer {
    //Constants
    const float STRCONS = 100;
    const float INTCONS = 100;
    const float AGICONS = 100;
    const float ENDCONS = 100;
    const float CONCONS = 100;
    const float WISCONS = 100;

    //Type Constants
    //stat types
    public const string Strength = "str";
    public const string Intelligence = "int";
    public const string Agility = "agi";
    public const string Constitution = "con";
    public const string Endurance = "dex";
    public const string Wisdom = "wis";

    public const string Level = "lev";
    //Derived attributes
    public const string Health = "HP";
    public const string Mana = "MP";
    public const string Stamina = "Stam";
    public const string Max = "max";
    public const string Current = "current";
    public const string Regeneration = "regen";

    //attatch type
    public static string Entity { get { return "entity"; } }
    public static string Destructible { get { return "destructible"; } }
    public static string Weapon { get { return "weapon"; } }



    //misc
    private int atrPoints;


    //attributes
    private string Type;
    private namedStat[] StatList;
    private RegeneratingResource[] AttributeList;


    //stats
    private RegeneratingResource health; //holds health information
    private RegeneratingResource mana;
    private RegeneratingResource stamina;
    private float _spd;
    private float _dmg;
    private float _mdmg;




    //constructors
    public statContainer(string type)
    {
        AttributeList = new RegeneratingResource[0];
        StatList = new namedStat[0];
        if (type == Destructible) constructDestructible();
        if (type == Entity) constructEntity();
        if (AttributeList.Length == 0)
        {
            AttributeList = new RegeneratingResource[1];
            AttributeList[0] = new RegeneratingResource(Health);
        }
        sortStats();
    }

    public statContainer(string[] stat,string[] attribute)
    {
        
    }



    private void constructDestructible()
    {
        AttributeList = new RegeneratingResource[1];
        AttributeList[0] = new RegeneratingResource(Health);
        StatList = new namedStat[1];
        StatList[0] = new namedStat(Strength);

    }

    private void constructEntity()
    {
        AttributeList = new RegeneratingResource[3];
        AttributeList[0] = new RegeneratingResource(Health);
        AttributeList[1] = new RegeneratingResource(Mana);
        AttributeList[2] = new RegeneratingResource(Stamina);
        StatList = new namedStat[7];
        StatList[0] = new namedStat(Strength);
        StatList[1] = new namedStat(Intelligence);
        StatList[2] = new namedStat(Agility);
        StatList[3] = new namedStat(Endurance);
        StatList[4] = new namedStat(Constitution);
        StatList[5] = new namedStat(Wisdom);
        StatList[6] = new namedStat(Level);
    }


    //properties
    public int this[string attribute]
    {
        get { return findAttribute(attribute).Current; }
        private set { findAttribute(attribute).Current = value; }
    }


    public float this[string stat, string part]
    {
        get
        {
            for (int i = 0; i < AttributeList.Length; i++)
            {
                if (AttributeList[i].Name == stat)
                {
                    return AttributeList[i][part];
                }
            }
            return -1;
        }
        set
        {
            for (int i = 0; i < AttributeList.Length; i++)
            {
                if (AttributeList[i].Name == stat)
                {
                    AttributeList[i][part] = value;
                }
            }
        }
    }

    //auxilary methods
    private namedStat findAttribute(string name)
    {
        int n = 0;
        int min = 0;
        int max = StatList.Length;
        while (n != min || n != max)
        {
            n = (min + max) / 2;
            if (min + 1 == max)
                if (StatList[max].Name.CompareTo(name) == 0)
                    return StatList[n];
            int pos = StatList[n].Name.CompareTo(name);
            if (pos == 0)
                return StatList[n];
            if (pos < 0)
                min = n;
            else
                max = n;
        }
        return new namedStat("error", -1);
    }


    private void sortStats()//heapsorts the attribute list
    {
        int length = StatList.Length;
        if (length == 0) return;
        for (int i = length / 2; i > 0; i--)
            heap(i, length);
        for (int i = length - 1; i > 0; i--)
        {
            swapAttribute(i, 0);
            heap(0, i);
        }
    }

    private void heap(int i, int l)
    {
        if (i * 2 < l)
        {
            if (i * 2 + 1 < l)
            {
                if (StatList[i * 2].Name.CompareTo(StatList[i * 2 + 1].Name) > 0)
                {
                    if (StatList[i].Name.CompareTo(StatList[i * 2 + 1].Name) > 0)
                    {
                        swapAttribute(i, i * 2 + 1);
                        heap(i * 2 + 1, l);
                    }
                }
                else
                    if (StatList[i].Name.CompareTo(StatList[i * 2].Name) > 0)
                {
                    swapAttribute(i, i * 2);
                    heap(i * 2, l);
                }
            }
            else
                if (StatList[i].Name.CompareTo(StatList[i * 2].Name) > 0)
            {
                swapAttribute(i, i * 2);
                heap(i * 2, l);
            }
        }
    }

    private void swapAttribute(int i, int j)
    {
        namedStat h = StatList[i];
        StatList[i] = StatList[j];
        StatList[j] = h;
    }


    //setting
    private void calcStats()
    {
        health.setMax((int)(this["Constitution"] * CONCONS));
        mana.setMax((int)(this["Wisdom"] * WISCONS));
        stamina.setMax((int)(this["Endurance"] * ENDCONS));
        _spd = (this["Agility"] * AGICONS);
        _dmg = (this["Strength"] * STRCONS);
        _mdmg = (this["Intelligence"] * INTCONS);
    }//*/


    public void addStat(string name)
    {

    }
    public void addAttribute(string name)
    {

    }


    //Hooks for UI 
    //NOT IMPLEMENTED YET
    //Cris, look at these for what to call from the UI  The above methods (not Properties) are for internal use
    //properties might be changed

    /*
    //For getting stat numbers use:     ALL PROPERTIES ARE READ ONLY
    //  stats:
    //      this[string name]    -   indexed property
    //          string name -   name of the stat
    //  attributes:
    //      this[sting name][string type]   -   Double Indexed property
    //          string name -   name of the attribute
    //          string type -   which part of the attribute
    //              Possible: Max,Current,Regeneration  -   Use the class constants to call
    //
    //          */

    /*
    //Method Name: addState
    //Arguments:
    //  string name -   name of the stat being increased
    //
    //Arguments:
    //  string name -   name of the stat being increased
    //  int amount  -   number to increase stat by.  If not enough stat points left then it won't add
    //
    //                        */
    public void addStat(string name, int amount)
    {
        if (atrPoints > amount)
        {
            atrPoints -= amount;
            this[name] += amount;
        }
        //  calcStats();
    }

    public string[] statList()//returns the names of all the stats
    {
        return new string[0];
    }
    public string[] attributeList()//returns the names of all the attributes
    {
        return new string[0];
    }



}