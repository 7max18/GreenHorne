using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardBehavior : MonoBehaviour
{
    public CardData data;
    public TextMeshProUGUI cardType;
    public TextMeshProUGUI attackType;
    public Image cardColor;
    // Start is called before the first frame update
    void Start()
    {
        switch(data.cardType)
        {
            case CardType.Heavy:
                cardColor.color = Color.red;
                cardType.text = "Heavy";
                break;
            case CardType.Finesse:
                cardColor.color = Color.green;
                cardType.text = "Finesse";
                break;
            case CardType.Collab:
                cardColor.color = Color.blue;
                cardType.text = "Collab";
                break;
        }

        //Use equippedBy to determine character picture

        switch (data.attackType)
        {
            case AttackType.STR:
                attackType.text = "STR";
                break;
            case AttackType.DEX:
                attackType.text = "DEX";
                break;
            case AttackType.INT:
                attackType.text = "INT";
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
