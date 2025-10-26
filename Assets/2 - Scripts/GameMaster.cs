using TMPro;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster Instance;

    [HideInInspector] public int listType;
    public TextMeshProUGUI item1, item2, item3, item4;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
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
    void ListRandomizer()
    {
        listType = Random.Range(1, 4); // 1-3 range
    }
}
