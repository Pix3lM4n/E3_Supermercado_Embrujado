using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public float pickupRange = 3f;
    public Transform playerCamera;
    public Transform grabbedTransform;
    public Transform playerHands;
    public LayerMask interactionLayer;

    [HideInInspector] public ItemPickup item;
    [HideInInspector] public RaycastHit hit;

    private void Start()
    {
        item = hit.transform.GetComponent<ItemPickup>();
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
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
        if (Physics.Raycast(ray, out hit, pickupRange, interactionLayer))
        {
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
