using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Unity.VisualScripting;

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

public class Card : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public CardType cardType;
    public PartyMember equippedBy;
    public AttackType attackType;
    public TextMeshProUGUI attackTypeText;
    private RectTransform rt;
    private Canvas canvas;
    private CanvasGroup cg;
    public HandSlot[] hand = new HandSlot[5];
    public BattleManager battleManager;
    // Start is called before the first frame update
    void Start()
    {
        //Use equippedBy to determine character picture
        canvas = transform.parent.parent.parent.GetComponent<Canvas>();
        rt = GetComponent<RectTransform>();
        cg = GetComponent<CanvasGroup>();
        battleManager = GameObject.FindGameObjectWithTag("BattleManager").GetComponent<BattleManager>();

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

    public void OnBeginDrag(PointerEventData eventData)
    {
        cg.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rt.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        cg.blocksRaycasts = true;

        if (!eventData.pointerEnter.GetComponent<DrawerCardSlot>())
        {
            if (!transform.parent.GetComponent<HandSlot>())
            {
                for (int i = 0; i < hand.Length; i++)
                {
                    if (!hand[i].hasCard)
                    {
                        transform.SetParent(hand[i].transform);
                        hand[i].hasCard = true;
                        break;
                    }
                }
            }

            transform.position = transform.parent.position;
        }
        else
        {
            if (transform.parent.GetComponent<HandSlot>())
            {
                transform.parent.GetComponent<HandSlot>().hasCard = false;
            }
            transform.SetParent(eventData.pointerEnter.transform);
        }

        battleManager.CheckIfReady();
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }
}
