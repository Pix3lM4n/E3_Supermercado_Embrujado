using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public static GameMaster Instance;

    [SerializeField] bool isListCorrect; //False = list is wrong, true = list is correct
    [HideInInspector] public int listType;
    public float appleCounter, milkCounter, meatCounter, cookieCounter;
    public TextMeshProUGUI item1, item2, item3, item4;
    public ItemData appleItem, meatItem, milkItem, cookieItem;
    public Image voucherBox;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        voucherBox.enabled = false;
        ListRandomizer();
        switch (listType)
        {
            case 1:
                item1.text = "Manzana";
                item2.text = "Carne x3";
                item3.text = "Leche";
                item4.text = null;
                break;
            case 2:
                item1.text = "Manzana x2";
                item2.text = "Carne";
                item3.text = "Leche x3";
                item4.text = "Galleta";
                break;
            case 3:
                item1.text = "Manzana x3";
                item2.text = null;
                item3.text = null;
                item4.text = null;
                break;
        }
    }
    private void Update()
    {
        listType = Random.Range(1, 4); // 1-3 range
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CheckList();
        }
    }
    void CheckList() //Use with cart script to check how many items there are
    {
        switch (listType)
        {
            case 1:
                if (appleCounter == 1 && meatCounter == 3 && milkCounter == 1)
                {
                    isListCorrect = true;
                }
                break;
            case 2:
                if (appleCounter == 2 && meatCounter == 1 && milkCounter == 3 && cookieCounter == 1)
                {
                    isListCorrect = true;
                }
                break;
            case 3:
                if (appleCounter == 3)
                {
                    isListCorrect = true;
                }
                break;
        }
    }
    void Pay() //Func gets called at checkout
    {
        //float applePrice = appleItem.price * appleCounter;
        //float meatPrice = meatItem.price * meatCounter;
        //float milkPrice = milkItem.price * milkCounter;
        //float cookiPrice = cookieItem.price * cookieCounter;

        float subTotalPrice = (appleItem.price * appleCounter) + (milkItem.price * milkCounter) + (cookieItem.price * cookieCounter) + (meatItem.price * meatCounter);

        float totalDiscounts = (milkItem.price * milkCounter) + (cookieItem.price * cookieCounter) * 2f + (meatItem.price * meatCounter) * 0.5f;

        float totalToPay = subTotalPrice - totalDiscounts;
    }
    void ListRandomizer()
    {
        listType = Random.Range(1, 4); // 1-3 range
    }
    void OpenVoucher() //Add text to this func
    {
        voucherBox.enabled = true;
    }
}
