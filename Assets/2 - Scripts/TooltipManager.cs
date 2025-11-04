using UnityEngine;
using TMPro;
public class TooltipManager : MonoBehaviour
{
    public static TooltipManager instance;
    public GameObject tooltipPanel;
    public TextMeshProUGUI tooltipText;

    private void Awake()
    {
        instance = this;
        tooltipPanel.SetActive(false);
    }
    public void ShowTooltip(string message)
    {
        tooltipText.text = message;
        tooltipPanel.SetActive(true);
    }
    public void HideTooltip()
    {
        tooltipPanel.SetActive(false);
    }
}
