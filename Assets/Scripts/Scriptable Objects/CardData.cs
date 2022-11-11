using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType
{
    Heavy,
    Finesse,
    Collab,
}
public enum AttackType
{
    STR,
    DEX,
    INT,
}

[CreateAssetMenu(fileName = "NewCard", menuName = "ScriptableObjects/Cards", order = 2)]
public class CardData : ScriptableObject
{
    public CardType cardType;
    public PartyMember equippedBy;
    public AttackType attackType;
}
