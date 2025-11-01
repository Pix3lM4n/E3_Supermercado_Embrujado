using System.Collections;
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

    string voucherItems;
    float writingSpeed = 0.01f;
    bool isTypeWriterFinished;

    float applePrice, meatPrice, milkPrice, cookiePrice, totalDiscounts, subTotalPrice, totalToPay;
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
        voucherItems = "=Articulos=" + "\n" + "Manzana: " + appleData.description + " - " + applePrice + "\n" + "Leche: " + milkData.description + " - " + milkPrice + "\n" + "Carne: " + meatData.description + " - " + meatPrice
                + "\n" + "Galleta: " + cookieData.description + " - " + cookiePrice + "\n" + "=Sub Total=" + "\n" + "$ " + subTotalPrice + "\n" + "=Descuentos=" + "\n" + "$ " + totalDiscounts + "\n" + "=A Pagar=" + "\n" + "$ " + totalToPay;
        //~3:30 AM. No es bueno ni pulcro, pero funciona
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
        if (isTypeWriterFinished == true)
        {
            StopCoroutine("TypWriter");
        }
    }
    public void RespawnItem(string itemTag)
    {
        switch (itemTag)
        {
            case "Apple":
                applePFClone = Instantiate(applePF, appleSpawn);
                print("Spawned apple");
                break;
            case "Meat":
                meatPFClone = Instantiate(meatPF, meatSpawn);
                print("Spawned meat");
                break;
            case "Milk":
                milkPFClone = Instantiate(milkPF, milkSpawn);
                print("Spawned milk");
                break;
            case "Cookie":
                cookiePFClone = Instantiate(cookiePF, cookieSpawn);
                print("Spawned cookie");
                break;
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
        applePrice = appleData.price * appleCounter;
        meatPrice = meatData.price * meatCounter;
        milkPrice = milkData.price * milkCounter;
        cookiePrice = cookieData.price * cookieCounter;

        subTotalPrice = applePrice + meatPrice + milkPrice + cookiePrice;
        totalDiscounts = (milkData.price * milkCounter) + (meatData.price * meatCounter) * 0.5f + (cookieData.price* cookieCounter) * 2f;
        totalToPay = subTotalPrice - totalDiscounts;

        if (isVoucherShown == true)
        {
            voucherBox.enabled = true;
            voucherText.enabled = true;
            StartCoroutine("TypeWriter");
            CheckList();
            Destroy(applePFClone.gameObject);
            Destroy(meatPFClone.gameObject);
            Destroy(milkPFClone.gameObject);
            Destroy(cookiePFClone.gameObject);
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
    IEnumerator TypeWriter()
    {
        foreach (char character in voucherItems)
        {
            voucherText.text += character;
            yield return new WaitForSeconds(writingSpeed);
        }
        isTypeWriterFinished = true;
    }
}