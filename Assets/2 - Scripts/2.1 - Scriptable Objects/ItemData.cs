using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Supermarket/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    [TextArea] public string description;
    public float price;
}
