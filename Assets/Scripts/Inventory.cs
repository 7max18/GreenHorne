using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Equipment> equipment = new List<Equipment>();
    public bool needsUpdating;
    // Start is called before the first frame update
    void Awake()
    {
        needsUpdating = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddItem(Equipment item)
    {
        equipment.Add(item);
        equipment.Sort();
        needsUpdating = true;   
    }

    public void RemoveItem(Equipment item)
    {
        equipment.Remove(item);
        needsUpdating = true;
    }
}
