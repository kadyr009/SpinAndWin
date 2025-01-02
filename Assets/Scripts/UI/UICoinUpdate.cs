using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UICoinUpdate : MonoBehaviour
{
	private TextMeshProUGUI buttonText;
	
	private void Start()
	{
		UpdateCoins();
	}
	
    private void OnEnable()
    {
        CurrencyManager.OnCoinsChanged += UpdateCoins;
    }

    private void OnDisable()
    {
        CurrencyManager.OnCoinsChanged -= UpdateCoins;
    }
    
    private void UpdateCoins()
    {
    	buttonText = gameObject.GetComponent<TextMeshProUGUI>();
    	
    	buttonText.text = "Coins: " + PlayerPrefs.GetInt("Currency", 0).ToString();
    }
}
