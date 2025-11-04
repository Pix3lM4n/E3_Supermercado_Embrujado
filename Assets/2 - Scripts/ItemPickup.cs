using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemData itemData;
    public bool isInCart = false;

    public string GetTooltipInfo()
    {
        return $"{itemData.name}\n" +
            $"{itemData.description}\n" +
            $"{itemData.price:F2}";
    }
}