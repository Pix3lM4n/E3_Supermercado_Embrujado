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
            switch (item.itemData.description)
            {
                case "Frutas":
                    GameMaster.Instance.appleCounter++;
                    break;
                case "Carnes":
                    GameMaster.Instance.meatCounter++;
                    break;
                case "Lacteos":
                    GameMaster.Instance.milkCounter++;
                    break;
                case "Panaderia":
                    GameMaster.Instance.cookieCounter++;
                    break;
            }

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
            switch (item.itemData.description)
            {
                case "Frutas":
                    GameMaster.Instance.appleCounter--;
                    break;
                case "Carnes":
                    GameMaster.Instance.meatCounter--;
                    break;
                case "Lacteos":
                    GameMaster.Instance.milkCounter--;
                    break;
                case "Panaderia":
                    GameMaster.Instance.cookieCounter--;
                    break;
            }

            Debug.Log($"Removed {item.itemData.itemName} from cart");
        }
    }
}
