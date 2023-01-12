using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public List<DrawerCardSlot> cardSlots = new List<DrawerCardSlot>();
    public GameObject goButton;
    public Drawer drawer;
    public Deck deck;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckIfReady()
    {
        bool allCardsReadied = true;
        for (int i = 0; i < cardSlots.Count; i++)
        {
            if (cardSlots[i].gameObject.activeSelf && cardSlots[i].transform.childCount == 0)
            {
                allCardsReadied = false;
                break;
            }
        }

        if (allCardsReadied)
        {
            goButton.SetActive(true);
        }
        else
        {
            goButton.SetActive(false);
        }
    }

    public void LaunchAttack()
    {
        for (int i = 0; i < cardSlots.Count; i++)
        {
            if (cardSlots[i].gameObject.activeSelf)
            {
                GameObject usedCard = cardSlots[i].transform.GetChild(0).gameObject;
                deck.cards.Add(usedCard.GetComponent<Card>().info);
                Destroy(usedCard);
            }
        }
        goButton.SetActive(false);
        drawer.OpenOrClose();
        deck.drawnForTurn = false;
    }
}
