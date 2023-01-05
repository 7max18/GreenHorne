using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

[System.Serializable]
public class CardInfo
{
    public PartyMember equippedBy;
    public int cardNumber;

    public CardInfo(PartyMember character, int num)
    {
        equippedBy = character;
        cardNumber = num;
    }
}
public class Deck : MonoBehaviour
{
    public GameObject heavyCard;
    public GameObject finesseCard;
    public GameObject collabCard;
    public HandSlot[] hand = new HandSlot[5];
    public Transform extraCardSlot;
    public int cardsDrawn;
    private List<PartyMember> characters = Enum.GetValues(typeof(PartyMember)).Cast<PartyMember>().ToList();
    private List<CardInfo> cards = new List<CardInfo>();
    private static System.Random rng = new System.Random();
    private bool drawnForTurn;
    void Start()
    {
        GetCardPool();
        Shuffle();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetCardPool()
    {
        for (int i = 1; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                cards.Add(new CardInfo(characters[i], j));
            }
        }
    }

    //Adapted from https://stackoverflow.com/questions/273313/randomize-a-listt
    void Shuffle()
    {
        int n = cards.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            CardInfo value = cards[k];
            cards[k] = cards[n];
            cards[n] = value;
        }
    }

    public void DrawCards()
    {
        if (!drawnForTurn)
        {
            CardInfo drawnCard; 

            for (int i = 0; i < 5; i++)
            {
                if (!hand[i].hasCard)
                {
                    drawnCard = cards[0];
                    CreateCard(drawnCard, hand[i].transform);
                    hand[i].hasCard = true;
                }
            }

            drawnCard = cards[rng.Next(cards.Count)];
            CreateCard(drawnCard, extraCardSlot);

            drawnForTurn = true;
        }
    }

    private void CreateCard(CardInfo drawnCard, Transform location)
    {
        StatManager characterStats = null;

        switch (drawnCard.equippedBy)
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

        GameObject newCard = null;

        switch (characterStats.cards[drawnCard.cardNumber])
        {
            case CardType.Heavy:
                newCard = Instantiate(heavyCard, location);
                break;
            case CardType.Finesse:
                newCard = Instantiate(finesseCard, location);
                break;
            case CardType.Collab:
                newCard = Instantiate(collabCard, location);
                break;
        }

        Card cardData = newCard.GetComponent<Card>();

        switch (characterStats.characterClass)
        {
            case CharacterClass.Melee:
                cardData.attackType = AttackType.STR;
                break;
            case CharacterClass.Magic:
                cardData.attackType = AttackType.INT;
                break;
            case CharacterClass.Ranger:
                cardData.attackType = AttackType.DEX;
                break;
        }

        cardData.equippedBy = characterStats.playerCharacter;
        cardData.hand = hand;

        cards.Remove(drawnCard);
    }
}
