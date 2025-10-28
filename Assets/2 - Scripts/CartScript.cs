using UnityEngine;

public class CartScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        ItemPickup item = other.GetComponent<ItemPickup>();
        if (item != null && !item.isInCart)
        {
            Inventory.instance.AddItem(item.itemData);
            item.isInCart = true;

            Debug.Log($"Added {item.itemData.itemName} to cart");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ItemPickup item = other.GetComponent<ItemPickup>();
        if (item != null && item.isInCart)
        {
            Inventory.instance.RemoveItem(item.itemData);
            item.isInCart = false;

            Debug.Log($"Removed {item.itemData.itemName} from cart");
        }
    }
}
