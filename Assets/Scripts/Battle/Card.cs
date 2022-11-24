using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public enum CardType
{
    Heavy = 0,
    Finesse = 1,
    Collab = 2,
}
public enum AttackType
{
    STR,
    DEX,
    INT,
}

public class Card : MonoBehaviour
{
    public CardType cardType;
    public PartyMember equippedBy;
    public AttackType attackType;
    public TextMeshProUGUI attackTypeText;
    // Start is called before the first frame update
    void Start()
    {
        //Use equippedBy to determine character picture

        switch (attackType)
        {
            case AttackType.STR:
                attackTypeText.text = "STR";
                break;
            case AttackType.DEX:
                attackTypeText.text = "DEX";
                break;
            case AttackType.INT:
                attackTypeText.text = "INT";
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
