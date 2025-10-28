using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public TMP_Text inventoryText;

    void Update()
    {
        inventoryText.text = "Carrito:\n";
        foreach (ItemData item in Inventory.instance.items)
        {
            inventoryText.text += "- " + item.itemName + "\n";
        }
    }
}
