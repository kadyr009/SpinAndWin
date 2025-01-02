using UnityEngine;
using UnityEngine.Events;

public class CurrencyManager : MonoBehaviour
{
    private int currency;

    public static event UnityAction OnCoinsChanged;

    private void Start()
    {
        LoadCurrency();
    }

    public void AddCurrency(int amount)
    {
        currency += amount;
        SaveCurrency();
    }

    public void SpendCurrency(int amount)
    {
        if (currency >= amount)
        {
            currency -= amount;
            SaveCurrency();
        }
    }

    private void SaveCurrency()
    {
        PlayerPrefs.SetInt("Currency", currency);
        PlayerPrefs.Save();
        
        OnCoinsChanged?.Invoke();
    }

    private void LoadCurrency()
    {
        currency = PlayerPrefs.GetInt("Currency", 0);
        
        OnCoinsChanged?.Invoke();
    }

    public int GetCurrency()
    {
        return currency;
    }
}
