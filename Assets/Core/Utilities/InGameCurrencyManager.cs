using UnityEngine;
using UnityEngine.Events;

public class InGameCurrencyManager : MonoBehaviour
{
	public static event UnityAction<int> OnCoinsCountChange;

    public int CurrentLevelCurrency{get => currentLevelCurrency; }

    private int currentLevelCurrency; 
    private int totalCurrency; 
    
    private void OnEnable() 
    {
    	GameManager.OnLevelEnding += EndLevel;
    	CoinAdder.OnCoinAdded += AddCurrency;
    }
    
    private void OnDisable() 
    {
    	GameManager.OnLevelEnding -= EndLevel;
    	CoinAdder.OnCoinAdded -= AddCurrency;
    }

    private void Start()
    {
        currentLevelCurrency = 0; 
        LoadTotalCurrency(); 
        
        OnCoinsCountChange?.Invoke(currentLevelCurrency);
    }

    public void AddCurrency(int amount)
    {
        currentLevelCurrency += amount;
        
        OnCoinsCountChange?.Invoke(currentLevelCurrency);
    }

    public void EndLevel()
    {
        totalCurrency += currentLevelCurrency;
        SaveTotalCurrency(); 
        currentLevelCurrency = 0; 
    }

    private void SaveTotalCurrency()
    {
        PlayerPrefs.SetInt("Currency", totalCurrency);
        PlayerPrefs.Save(); 
    }

    private void LoadTotalCurrency()
    {
        totalCurrency = PlayerPrefs.GetInt("Currency", 0); 
    }

    public int GetTotalCurrency()
    {
        return totalCurrency;
    }
}

