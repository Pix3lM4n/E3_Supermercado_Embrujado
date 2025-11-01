using UnityEngine;

public class TillScript : MonoBehaviour
{
    [SerializeField] bool isPlayerOnTrigger;
    PlayerMovement playerMovement;

    private void Awake()
    {
        playerMovement = FindFirstObjectByType<PlayerMovement>();
    }
    private void Update()
    {
        if (isPlayerOnTrigger == true && Input.GetKeyDown(KeyCode.E))
        {
            print("Show voucher");
            playerMovement.moveSpeed = 0f;
            GameMaster.Instance.isVoucherShown = true;
            isPlayerOnTrigger = false;
            GameMaster.Instance.Pay();
        }
        else if (GameMaster.Instance.isVoucherShown == true && Input.GetKeyDown(KeyCode.E))
        {
            print("Hide voucher");
            playerMovement.moveSpeed = 5f;
            GameMaster.Instance.isVoucherShown = false;
            GameMaster.Instance.Pay();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerOnTrigger = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerOnTrigger = false;
        }
    }
}
