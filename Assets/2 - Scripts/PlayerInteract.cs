using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerInteract : MonoBehaviour
{
    public float pickupRange = 3f;
    public Transform playerCamera;
    public Transform grabbedTransform;
    public Transform playerHands;
    public LayerMask interactionLayer;
    public int itemCounter;

    private ItemPickup lookedAtItem;

    void Update()
    {
        Debug.DrawRay(playerCamera.position, playerCamera.forward * pickupRange, Color.red);

        Ray ray = new Ray (playerCamera.position, playerCamera.forward);
        RaycastHit hit;

        if (Physics.Raycast (ray, out hit, pickupRange, interactionLayer))
        {
            ItemPickup item = hit.transform.GetComponent<ItemPickup>();
            if (item != null)
            {
                if (lookedAtItem != item)
                {
                    lookedAtItem = item;
                    TooltipManager.instance.ShowTooltip(item.GetTooltipInfo());
                }
            }
            else
            {
                if (lookedAtItem != null)
                {
                    TooltipManager.instance.HideTooltip();
                    lookedAtItem = null;
                }
            }
        }
        else
        {
            if (lookedAtItem != null)
            {
                TooltipManager.instance.HideTooltip();
                lookedAtItem = null;
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (grabbedTransform != null)
            {
                ReleaseTransform();
            }
            else
            {
                TryPickupItem();
            }
        }
    }

    void TryPickupItem()
    {
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, pickupRange, interactionLayer))
        {
            ItemPickup item = hit.transform.GetComponent<ItemPickup>();
            if (itemCounter < 10)
            {
                itemCounter++;
                GameMaster.Instance.RespawnItem(item.gameObject.tag);
            }

            if (item != null)
            {
                GrabTransform(hit.transform);
            }
        }
    }

    void GrabTransform(Transform transformToGrab)
    {
        grabbedTransform = transformToGrab;
        grabbedTransform.SetParent(playerHands);
        grabbedTransform.localPosition = Vector3.zero;
        grabbedTransform.GetComponent<Rigidbody>().isKinematic = true;
    }

    void ReleaseTransform()
    {
        grabbedTransform.GetComponent<Rigidbody>().isKinematic = false;
        grabbedTransform.SetParent(null);
        grabbedTransform = null;
    }
}
