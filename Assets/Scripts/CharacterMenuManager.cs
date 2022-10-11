using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenuManager : MonoBehaviour
{
    public StatManager selectedChar;
    public StatManager defaultSelectedChar;

    public Image infoPanel;

    public TextMeshProUGUI charName;
    public Image charPic;

    public TextMeshProUGUI STR;
    public TextMeshProUGUI DEX;
    public TextMeshProUGUI INT;
    public TextMeshProUGUI WIS;
    public TextMeshProUGUI CHA;
    public TextMeshProUGUI CON;

    public TextMeshProUGUI weapon;
    public TextMeshProUGUI armor;
    public TextMeshProUGUI trinket;

    // Start is called before the first frame update
    void OnEnable()
    {
        DisplayStats(defaultSelectedChar);
    }

    public void DisplayStats(StatManager character)
    {
        switch (character.playerCharacter)
        {
            case PartyMember.Yua:
                infoPanel.color = Color.blue;
                charName.text = "Yua";
                break;
            case PartyMember.Logan:
                infoPanel.color = Color.red;
                charName.text = "Logan";
                break;
            case PartyMember.Dan:
                infoPanel.color = Color.green;
                charName.text = "Dan";
                break;
            case PartyMember.Jim:
                infoPanel.color = Color.yellow;
                charName.text = "Jim";
                break;
        }

        //Add logic to change character pictures once we have them

        STR.text = "STR\n" + character.STR.ToString();
        DEX.text = "DEX\n" + character.DEX.ToString();
        INT.text = "INT\n" + character.INT.ToString();
        WIS.text = "WIS\n" + character.WIS.ToString();
        CHA.text = "CHA\n" + character.CHA.ToString();
        CON.text = "CON\n" + character.CON.ToString();

        if (character.weapon != null)
        {
            weapon.text = "Weapon: " + character.weapon.name;
        }
        else
        {
            weapon.text = "Weapon: None";
        }

        if (character.armor != null)
        {
            armor.text = "Armor: " + character.armor.name;
        }
        else
        {
            armor.text = "Armor: None";
        }

        if (character.trinket != null)
        {
            trinket.text = "Trinket: " + character.trinket.name;
        }
        else
        {
            trinket.text = "Trinket: None";
        }
    }
}
