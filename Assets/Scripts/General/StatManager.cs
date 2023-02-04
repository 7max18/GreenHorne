using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PartyMember
{
    None,
    Yua,
    Logan,
    Dan,
    Jim,
}

public enum CharacterClass
{
    None,
    Melee,
    Magic,
    Ranger,
}

public class StatManager : MonoBehaviour
{
    public PartyMember playerCharacter;
    public int HP = 100;
    public int maxHP = 100;
    public int STR;
    public int DEX;
    public int INT;
    public int WIS;
    public int CHA;
    public int CON;
    public Equipment weapon;
    public Equipment armor;
    public Equipment trinket;
    public CharacterClass characterClass;
    public CardType[] cards = new CardType[5];
}
