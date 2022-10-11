using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType
{
    Weapon,
    Armor,
    Trinket,
}

[CreateAssetMenu(fileName = "NewEquipment", menuName = "ScriptableObjects/Equipment", order = 1)]
public class Equipment : ScriptableObject
{
    public EquipmentType type;
    public int strEffect;
    public int dexEffect;
    public int intEffect;
    public int wisEffect;
    public int chaEffect;
    public int conEffect;
    public string otherEffects;
    [TextArea(3, 5)]
    public string description;
    public StatManager equippedBy;
}

//Note: who's equipping what will have to be saved to a file at some point.
