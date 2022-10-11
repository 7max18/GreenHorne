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
            
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDisable()
    {

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
        flavorText.text = item.description;
    }
}
