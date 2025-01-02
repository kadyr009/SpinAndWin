using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIChanceUpdate : MonoBehaviour
{
    public TextMeshProUGUI chanceText;
    public Slider chanceSlider;
    public string chanceName = "CoinSpawnChance";
    
    private void OnEnable()
    {
        Shop.OnChanceChanged += UpdateChanceText;
        Shop.OnChanceChanged += UpdateChanceSLider;
    }

    private void OnDisable()
    {
        Shop.OnChanceChanged -= UpdateChanceText;
        Shop.OnChanceChanged -= UpdateChanceSLider;
    }
    
    private void UpdateChanceText()
    {
        chanceText.text = $"Chance {PlayerPrefs.GetFloat(chanceName, 0.05f) * 100}%";
    }

    private void UpdateChanceSLider()
    {
        chanceSlider.value = PlayerPrefs.GetFloat(chanceName, 0.05f) * 3.333f;
    }
}
