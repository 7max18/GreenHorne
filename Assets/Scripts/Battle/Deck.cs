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
    public Transform[] cardPositions = new Transform[5];
    public int drawSlotIndex;
    public int cardsDrawn;
    private List<PartyMember> characters = Enum.GetValues(typeof(PartyMember)).Cast<PartyMember>().ToList();
    private List<CardInfo> cards = new List<CardInfo>();
    private static System.Random rng = new System.Random();
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

    public void DrawCard()
    {
        if (drawSlotIndex < 5)
        {
            CardInfo drawnCard = cards[cardsDrawn];
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

            CreateCard(characterStats, drawnCard.cardNumber);
            drawSlotIndex++;
            cardsDrawn++;
        }
    }

    private void CreateCard(StatManager character, int i)
    {
        GameObject newCard = null;

        switch (character.cards[i])
        {
            case CardType.Heavy:
                newCard = Instantiate(heavyCard, cardPositions[drawSlotIndex]);
                break;
            case CardType.Finesse:
                newCard = Instantiate(finesseCard, cardPositions[drawSlotIndex]);
                break;
            case CardType.Collab:
                newCard = Instantiate(collabCard, cardPositions[drawSlotIndex]);
                break;
        }

        Card cardData = newCard.GetComponent<Card>();

        switch (character.characterClass)
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

        cardData.equippedBy = character.playerCharacter;
    }
}
