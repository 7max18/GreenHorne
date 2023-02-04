using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public List<DrawerCardSlot> cardSlots = new List<DrawerCardSlot>();
    public GameObject goButton;
    public Drawer drawer;
    public Deck deck;
    public List<BattleEnemy> enemies = new List<BattleEnemy>();
    public Slider yuaHP;
    public Slider loganHP;
    public Slider danHP;
    public Slider jimHP;
    public Slider yuaFinesseMeter;
    public Slider loganFinesseMeter;
    public Slider danFinesseMeter;
    public Slider jimFinesseMeter;
    public Slider collabMeter;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] enemyObjs = GameObject.FindGameObjectsWithTag("Enemy");
        
        for (int i = 0; i < enemyObjs.Length; i++)
        {
            enemies.Add(enemyObjs[i].GetComponent<BattleEnemy>());
        }
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
        goButton.SetActive(false);
        drawer.OpenOrClose();

        List<Card> usedCards = new List<Card>();

        for (int i = 0; i < cardSlots.Count; i++)
        {
            if (cardSlots[i].gameObject.activeSelf)
            {
                GameObject usedCard = cardSlots[i].transform.GetChild(0).gameObject;
                Card cardData = usedCard.GetComponent<Card>();

                Attack(cardData);

                deck.cards.Add(cardData.info);
                usedCards.Add(cardData);
            }
        }

        CheckForMatch(usedCards);

        for (int i = 0; i < usedCards.Count; i++)
        {
            Destroy(usedCards[i].gameObject);
        }

        deck.drawnForTurn = false;
    }

    private void Attack(Card card)
    {
        StatManager attacker = null;

        switch (card.equippedBy)
        {
            case PartyMember.Yua:
                attacker = GameObject.FindGameObjectWithTag("Yua").GetComponent<StatManager>();
                break;
            case PartyMember.Logan:
                attacker = GameObject.FindGameObjectWithTag("Logan").GetComponent<StatManager>();
                break;
            case PartyMember.Dan:
                attacker = GameObject.FindGameObjectWithTag("Dan").GetComponent<StatManager>();
                break;
            case PartyMember.Jim:
                attacker = GameObject.FindGameObjectWithTag("Jim").GetComponent<StatManager>();
                break;
        }

        int damage = 0;

        switch (card.attackType)
        {
            case AttackType.STR:
                damage = attacker.STR;
                break;
            case AttackType.INT:
                damage = attacker.INT;
                break;
            case AttackType.DEX:
                damage = attacker.DEX;
                break;
        }

        switch (card.cardType)
        {
            case CardType.Heavy:
                break;
            case CardType.Finesse:
                damage = (int)(damage * 0.5f);
                break;
            case CardType.Collab:
                damage = (int)(damage * 0.7f);
                break;
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].HP -= damage;
        }
    }

    private void CheckForMatch(List<Card> cards)
    {
        CardType matchCondition = cards[0].cardType;

        for (int i = 1; i < cards.Count; i++)
        {
            if (cards[i].cardType != matchCondition)
            {
                return;
            }
        }

        if (matchCondition == CardType.Finesse)
        {
            foreach (Card card in cards)
            {
                switch (card.equippedBy)
                {
                    case PartyMember.Yua:
                        yuaFinesseMeter.value += 0.25f;
                        break;
                    case PartyMember.Logan:
                        loganFinesseMeter.value += 0.25f;
                        break;
                    case PartyMember.Dan:
                        danFinesseMeter.value += 0.25f;
                        break;
                    case PartyMember.Jim:
                        jimFinesseMeter.value += 0.25f;
                        break;
                }
            }
        }
        else if (matchCondition == CardType.Collab)
        {
            collabMeter.value += 0.25f;
        }

        return;
    }
}
