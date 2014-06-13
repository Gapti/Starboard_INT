using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Collections;

/// <summary>
/// Fitness:  Player health points increased.
///Durability:  Player inventory slots increased.
///Firepower:  Ranged damage increased for all ranged weapons.  
///Tenacity:  Resistance to all damage types increased.  
///Judgment:  Reduced experience needed to level up.  Increased critical attack chance.  
///Mending:  Increased passive regain of health points after damage taken.  
///Charisma:  Increased chance of a charisma dialogue option working when selected.
///Intimidation:  Increased chance of a prone to violence dialogue option working when selected.
/// </summary>
public class INTCharacter : MonoBehaviour
{
    private INTAttribute[] _attributes = new INTAttribute[Enum.GetNames(typeof(INTAttributeTypes)).Length];
    private List<INTAttributeModifier> _modifiers = new List<INTAttributeModifier>();

	// Use this for initialization
	void Start () {

        for (int i = 0; i < _attributes.Length; i++)
        {
            INTAttribute attrib = new INTAttribute
            {
                Base = 0,
                BaseModifier = 0,
                Multiplier = 1f,
                Total = 0f
            };
            _attributes[i] = attrib;
        }
        _modifiers.Add(new BaseHealthModifier());

        UpdateAttributes();
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /// <summary>
    /// Should be called when the player is modified in any way
    /// (Equipment change, attribute change, Skill Change, BaseModifier Change)
    /// </summary>
    public void UpdateAttributes()
    {
        for (int i = 0; i < _attributes.Length; i++)
        {
            var intAttribute = _attributes[i];
            intAttribute.BaseModifier = 0;
            intAttribute.Multiplier = 1f;
            intAttribute.Total = 0;

            foreach (var modifier in _modifiers)
            {
                modifier.ApplyModifiers(intAttribute, (INTAttributeTypes)i);
            }

            intAttribute.CalculateTotal();
        }
    }

    public float GetCurrent(INTAttributeTypes type)
    {
        int lookup = (int) type;
        return _attributes[lookup].Total - _attributes[lookup].Damage;
    }

    public float GetCurrentPercentageOf(INTAttributeTypes type)
    {
        float current = GetCurrent(type);
        if (current <= 0f)
            return 0f;
        return current/_attributes[(int) type].Total*100f;
    }

    public INTAttribute this[INTAttributeTypes type]
    {
        get { return this[(int) type]; }
        set { this[(int) type] = value; }
    }

    public INTAttribute this[int type]
    {
        get { return _attributes[type]; }
        set { _attributes[type] = value; }
    }
}

public enum INTAttributeTypes
{   
     Fitness,
     Durability,
     Firepower,
     Tenacity,
     Judgment,
     Mending,
     Charisma,
     Intimidation,
     Health
}


