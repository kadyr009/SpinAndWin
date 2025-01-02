using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class SkinShop : MonoBehaviour
{
    public SkinOwner owner;
    public CurrencyManager currencyManager;
    public int skinId;
    public int skinPrice;
    public static event UnityAction OnSkinBuyed;

    private string skinKey;
    private TextMeshProUGUI buttonText;

    private void Start()
    {
        skinKey = $"{owner}_skin_{skinId}";
        buttonText = GetComponentInChildren<TextMeshProUGUI>();

        if (PlayerPrefs.GetInt(skinKey, 0) == 1)
        {
            SetPurchased();
        }
        else
        {
            buttonText.text = skinPrice.ToString();
        }
    }

    public void BuySkin()
    {
        if (HasEnoughMoney())
        {
            currencyManager.SpendCurrency(skinPrice);
            PlayerPrefs.SetInt(skinKey, 1);
            PlayerPrefs.Save();
            SetPurchased();
            OnSkinBuyed?.Invoke();
        }
    }

    private void SetPurchased()
    {
        GetComponent<Button>().interactable = false;
        buttonText.text = "Purchased";
    }

    private bool HasEnoughMoney()
    {
        return PlayerPrefs.GetInt("Currency", 0) >= skinPrice;
    }
}
