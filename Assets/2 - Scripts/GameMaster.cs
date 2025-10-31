using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public static GameMaster Instance;

    #region Variables
    [Header("List Variables")]
    public int isListCorrect; //0 = normal, 1 = list is correct, 2 = list is wrong
    [HideInInspector] public int listType;

    [Header("Item Counters")]
    public float appleCounter;
    public float milkCounter;
    public float meatCounter;
    public float cookieCounter;

    [Header("UI")]
    public Image listBox;
    public TextMeshProUGUI listText;
    public Image voucherBox;
    public TextMeshProUGUI voucherText;
    public bool isVoucherShown;

    [Header("Item Data")]
    public ItemData appleData;
    public ItemData meatData;
    public ItemData milkData;
    public ItemData cookieData;

    [Header("Prefabs")]
    public GameObject applePF;
    public GameObject meatPF;
    public GameObject milkPF;
    public GameObject cookiePF;

    [Header("Spawn")]
    [HideInInspector] GameObject applePFClone, meatPFClone, milkPFClone, cookiePFClone;
    public Transform appleSpawn;
    public Transform meatSpawn;
    public Transform milkSpawn;
    public Transform cookieSpawn;
    PlayerInteract playerInteract;
    #endregion

    private void Awake()
    {
        Instance = this;
        playerInteract = FindFirstObjectByType<PlayerInteract>();
    }
    private void Start()
    {
        voucherBox.enabled = false;
        listBox.enabled = false;

        listText.enabled = false;
        voucherText.enabled = false;

        ListRandomizer();
        switch (listType)
        {
            case 1:
                listText.text = "Manzana" + "\n" + "Carne x3" + "\n" + "Leche";
                break;
            case 2:
                listText.text = "Manzana x2" + "\n" + "Carne" + "\n" + "Leche x3" + "\n" + "Galleta";
                break;
            case 3:
                listText.text = "Manzana x3";
                break;
        }

        applePFClone = Instantiate(applePF, appleSpawn);
        meatPFClone = Instantiate(meatPF, meatSpawn);
        milkPFClone = Instantiate(milkPF, milkSpawn);
        cookiePFClone = Instantiate(cookiePF, cookieSpawn);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) //Open list panel
        {
            if (listBox.enabled == false)
            {
                listBox.enabled = true;
                listText.enabled = true;
            }
            else
            {
                listBox.enabled = false;
                listText.enabled = false;
            }
        }

        if (playerInteract.grabbedTransform != null)
        {
            switch (playerInteract.item.gameObject.tag)
            {
                case "Apple":
                    applePFClone = Instantiate(applePF, appleSpawn);
                    break;
                case "Meat":
                    meatPFClone = Instantiate(meatPF, meatSpawn);
                    break;
                case "Milk":
                    milkPFClone = Instantiate(milkPF, milkSpawn);
                    break;
                case "Cookie":
                    cookiePFClone = Instantiate(cookiePF, cookieSpawn);
                    break;
            }
        }
    }
    void CheckList() //Use with cart script to check how many items there are
    {
        switch (listType)
        {
            case 1:
                if (appleCounter == 1 && meatCounter == 3 && milkCounter == 1)
                {
                    isListCorrect = 1;
                }
                else
                {
                    isListCorrect = 2;
                }
                    break;
            case 2:
                if (appleCounter == 2 && meatCounter == 1 && milkCounter == 3 && cookieCounter == 1)
                {
                    isListCorrect = 1;
                }
                else
                {
                    isListCorrect = 2;
                }
                    break;
            case 3:
                if (appleCounter == 3)
                {
                    isListCorrect = 1;
                }
                else
                {
                    isListCorrect = 2;
                }
                break;
        }
    }
    public void Pay() //Func gets called at checkout
    {
        //float applePrice = appleItem.price * appleCounter;
        //float meatPrice = meatItem.price * meatCounter;
        //float milkPrice = milkItem.price * milkCounter;
        //float cookiPrice = cookieItem.price * cookieCounter;

        float subTotalPrice = (appleData.price * appleCounter) + (milkData.price * milkCounter) + (cookieData.price * cookieCounter) + (meatData.price * meatCounter);
        float totalDiscounts = (milkData.price * milkCounter) + (cookieData.price * cookieCounter) * 2f + (meatData.price * meatCounter) * 0.5f;
        float totalToPay = subTotalPrice - totalDiscounts;

        if (isVoucherShown == true)
        {
            voucherBox.enabled = true;
            voucherText.text = "=Articulos=" + "\n" + "Manzana: " + appleCounter + appleData.price + "\n" + "Leche: " + milkCounter + milkData.price + "\n" + "Carne: " + meatCounter + meatData.price + "\n" +
                "Galleta: " + cookieCounter + cookieData.price + "\n" + "=Sub Total=" + "\n" + "$ " + subTotalPrice + "\n" + "=Descuentos=" + "$ " + totalDiscounts + "\n" + "=A Pagar=" + "$ " + totalToPay;
        }
        
        if (isVoucherShown == false)
        {
            voucherBox.enabled = false;
            voucherText.text = null;
        }
    }
    void ListRandomizer()
    {
        listType = Random.Range(1, 4); // 1-3 range
    }
}