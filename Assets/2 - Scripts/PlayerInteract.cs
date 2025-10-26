using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public float pickupRange = 3f;
    public Transform playerCamera;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryPickupItem();
        }
    }

    void TryPickupItem()
    {
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickupRange))
        {
            ItemPickup item = hit.transform.GetComponent<ItemPickup>();
            if (item != null)
            {
                Inventory.instance.AddItem(item.itemData);
                Destroy(item.gameObject);
            }
        }
    }
}