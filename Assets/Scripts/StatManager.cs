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

public class StatManager : MonoBehaviour
{
    public PartyMember playerCharacter;
    public int STR;
    public int DEX;
    public int INT;
    public int WIS;
    public int CHA;
    public int CON;
    public Equipment weapon;
    public Equipment armor;
    public Equipment trinket;
}
