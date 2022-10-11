using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemMenuManager : MonoBehaviour
{
    public Inventory inventory;
    public Button inventoryItemPrefab;
    public GameObject scrollRectContent;
    public TextMeshProUGUI flavorText;

    private Equipment selectedItem;

    public Button yuaEquipButton;
    public Button loganEquipButton;
    public Button danEquipButton;
    public Button jimEquipButton;
    public Button unequipButton;

    // Start is called before the first frame update
    void Start()
    {
        if (inventory.needsUpdating)
        {
            foreach (Transform child in scrollRectContent.transform)
            {
                Destroy(child);
            }

            AddItemsToList();
        }

        unequipButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDisable()
    {
        DeselectItems();
    }

    void AddItemsToList()
    {
        foreach (Equipment item in inventory.equipment)
        {
            Button button = Instantiate(inventoryItemPrefab, scrollRectContent.transform);
            button.GetComponentInChildren<TextMeshProUGUI>().text = item.name;
            button.GetComponent<Button>().onClick.AddListener(delegate { SelectItem(item); });
        }
    }

    void SelectItem(Equipment item)
    {
        unequipButton.gameObject.SetActive(false);
        flavorText.text = item.description;
        selectedItem = item;

        yuaEquipButton.interactable = true;
        loganEquipButton.interactable = true;
        danEquipButton.interactable = true;
        jimEquipButton.interactable = true;

        if (item.equippedBy != null)
        {
            unequipButton.gameObject.SetActive(true);

            switch (item.equippedBy.playerCharacter)
            {
                case PartyMember.Yua:
                    yuaEquipButton.interactable = false;
                    break;
                case PartyMember.Logan:
                    loganEquipButton.interactable = false;
                    break;
                case PartyMember.Dan:
                    danEquipButton.interactable = false;
                    break;
                case PartyMember.Jim:
                    jimEquipButton.interactable = false;
                    break;
            }
        }
    }

    public void DeselectItems()
    {
        flavorText.text = "";
        yuaEquipButton.interactable = false;
        loganEquipButton.interactable = false;
        danEquipButton.interactable = false;
        jimEquipButton.interactable = false;
        unequipButton.gameObject.SetActive(false);
    }
    public void Equip(StatManager character)
    {
        switch (selectedItem.type)
        {
            case EquipmentType.Weapon:
                character.weapon = selectedItem;
                break;
            case EquipmentType.Armor:
                character.armor = selectedItem;
                break;
            case EquipmentType.Trinket:
                character.trinket = selectedItem;
                break;
                //Add logic for consumables later
        }
        if (selectedItem.equippedBy != null)
        {
            UnEquip();
        }

        character.STR += selectedItem.strEffect;
        character.DEX += selectedItem.dexEffect;
        character.INT += selectedItem.intEffect;
        character.WIS += selectedItem.wisEffect;
        character.CHA += selectedItem.chaEffect;
        character.CON += selectedItem.conEffect;

        selectedItem.equippedBy = character;

        DeselectItems();
    }

    public void UnEquip()
    {
        StatManager character = selectedItem.equippedBy;
        unequipButton.gameObject.SetActive(false);
        selectedItem.equippedBy = null;
        switch (selectedItem.type)
        {
            case EquipmentType.Weapon:
                character.weapon = null;
                break;
            case EquipmentType.Armor:
                character.armor = null;
                break;
            case EquipmentType.Trinket:
                character.trinket = null;
                break;
        }

        character.STR -= selectedItem.strEffect;
        character.DEX -= selectedItem.dexEffect;
        character.INT -= selectedItem.intEffect;
        character.WIS -= selectedItem.wisEffect;
        character.CHA -= selectedItem.chaEffect;
        character.CON -= selectedItem.conEffect;
    }
}
