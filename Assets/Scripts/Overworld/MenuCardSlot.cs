using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class MenuCardSlot : MonoBehaviour
{
    public PartyMember character;
    private StatManager characterStats;
    public int slotIndex;
    public CardType cardType;
    // Start is called before the first frame update
    void Start()
    {
        switch (character)
        {
            case PartyMember.Yua:
                characterStats = GameObject.FindGameObjectWithTag("Yua").GetComponent<StatManager>();
                break;
            case PartyMember.Logan:
                characterStats = GameObject.FindGameObjectWithTag("Logan").GetComponent<StatManager>();
                break;
            case PartyMember.Dan:
                characterStats = GameObject.FindGameObjectWithTag("Dan").GetComponent<StatManager>();
                break;
            case PartyMember.Jim:
                characterStats = GameObject.FindGameObjectWithTag("Jim").GetComponent<StatManager>();
                break;
        }
        cardType = characterStats.cards[slotIndex];
        SetCardSlot();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SetCardSlot()
    {
        switch (cardType)
        {
            case CardType.Heavy:
                GetComponent<Image>().color = Color.red;
                GetComponentInChildren<TextMeshProUGUI>().text = "Heavy";
                break;
            case CardType.Finesse:
                GetComponent<Image>().color = Color.green;
                GetComponentInChildren<TextMeshProUGUI>().text = "Finesse";
                break;
            case CardType.Collab:
                GetComponent<Image>().color = Color.blue;
                GetComponentInChildren<TextMeshProUGUI>().text = "Collab";
                break;
        }
    }

    public void CycleCard()
    {
        cardType++;
        if ((int)cardType == 3)
        {
            cardType = 0;
        }
        characterStats.cards[slotIndex] = cardType;
        SetCardSlot();
    }
}
