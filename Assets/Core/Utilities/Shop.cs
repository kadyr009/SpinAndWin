using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class Shop : MonoBehaviour
{
	public CurrencyManager currencyManager;
	
    private float _coinSpawnChance;
    private float _freezeBuffSpawnChance;
    private float _invisibleBuffSpawnChance;
    private float increaseAmount = 0.05f;
    private int _coinSpawnChancePrice;
    private int _freezeBuffSpawnChancePrice;
    private int _invisibleBuffSpawnChancePrice;
    private int _currency; 

    public static event UnityAction OnChanceChanged;
	public static event UnityAction<string, string> OnPriceChanged;

    private void OnEnable()
    {
        CurrencyManager.OnCoinsChanged += UpdateCurrency;
    }

    private void OnDisable()
    {
        CurrencyManager.OnCoinsChanged -= UpdateCurrency;
    }

    private void Start()
    {   
        _coinSpawnChance = PlayerPrefs.GetFloat("CoinSpawnChance", 0.05f);
        _freezeBuffSpawnChance = PlayerPrefs.GetFloat("FreezeBuffSpawnChance", 0.05f); 
        _invisibleBuffSpawnChance = PlayerPrefs.GetFloat("InvisibleBuffSpawnChance", 0.05f);
        
        _coinSpawnChancePrice = PlayerPrefs.GetInt("CoinSpawnChancePrice", 100);
        _freezeBuffSpawnChancePrice = PlayerPrefs.GetInt("FreezeBuffSpawnChancePrice", 100); 
        _invisibleBuffSpawnChancePrice = PlayerPrefs.GetInt("InvisibleBuffSpawnChancePrice", 100);
        
        _currency = PlayerPrefs.GetInt("Currency", 0);
        
        OnPriceChanged?.Invoke("CoinSpawnChance", "CoinSpawnChancePrice");
        OnPriceChanged?.Invoke("FreezeBuffSpawnChance", "FreezeBuffSpawnChancePrice");
        OnPriceChanged?.Invoke("InvisibleBuffSpawnChance", "InvisibleBuffSpawnChancePrice");
        
        OnChanceChanged?.Invoke();
    }

    public void IncreaseCoinDropChance()
    {
    	if (_currency >= _coinSpawnChancePrice)
    	{
    		currencyManager.SpendCurrency(_coinSpawnChancePrice);
    		
        	_coinSpawnChance += increaseAmount;
        	_coinSpawnChancePrice =(int)(_coinSpawnChancePrice * 1.5f); 
	
        	PlayerPrefs.SetFloat("CoinSpawnChance", _coinSpawnChance);
        	PlayerPrefs.SetInt("CoinSpawnChancePrice", _coinSpawnChancePrice);
        }

        OnPriceChanged?.Invoke("CoinSpawnChance", "CoinSpawnChancePrice");
        OnChanceChanged?.Invoke();
    }

    public void IncreaseFreezeBuffDropChance()
    {
        if (_currency >= _freezeBuffSpawnChancePrice)
    	{
    		currencyManager.SpendCurrency(_freezeBuffSpawnChancePrice);
    		
        	_freezeBuffSpawnChance += increaseAmount;
        	_freezeBuffSpawnChancePrice =(int)(_freezeBuffSpawnChancePrice * 1.5f); 
	
        	PlayerPrefs.SetFloat("FreezeBuffSpawnChance", _freezeBuffSpawnChance);
        	PlayerPrefs.SetInt("FreezeBuffSpawnChancePrice", _freezeBuffSpawnChancePrice);
        }

        OnPriceChanged?.Invoke("FreezeBuffSpawnChance", "FreezeBuffSpawnChancePrice");
        OnChanceChanged?.Invoke();
    }

    public void IncreaseInvisibleBuffDropChance()
    {
        if (_currency >= _invisibleBuffSpawnChancePrice) 
    	{
    		currencyManager.SpendCurrency(_invisibleBuffSpawnChancePrice);
    		
        	_invisibleBuffSpawnChance += increaseAmount;
        	_invisibleBuffSpawnChancePrice =(int)(_invisibleBuffSpawnChancePrice * 1.5f); 
	
        	PlayerPrefs.SetFloat("InvisibleBuffSpawnChance", _invisibleBuffSpawnChance);
        	PlayerPrefs.SetInt("InvisibleBuffSpawnChancePrice", _invisibleBuffSpawnChancePrice);
        }
        
        OnPriceChanged?.Invoke("InvisibleBuffSpawnChance", "InvisibleBuffSpawnChancePrice");
        OnChanceChanged?.Invoke();
    }

    private void UpdateCurrency()
    {
        _currency = PlayerPrefs.GetInt("Currency", 0);
    }
}
