using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public List<ItemData> items = new List<ItemData>();

    void Awake()
    {
        instance = this;
    }

    public void AddItem(ItemData item)
    {
        items.Add(item);
        Debug.Log("Picked up: " + item.itemName);
    }
}
