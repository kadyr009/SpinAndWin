using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetPriceForButton : MonoBehaviour
{
	public string text;
	
    private void OnEnable()
    {
        Shop.OnPriceChanged += UpdatePrice;
    }

    private void OnDisable()
    {
        Shop.OnPriceChanged -= UpdatePrice;
    }
    
    private void UpdatePrice(string chance, string price)
	{
	    TextMeshProUGUI buttonText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
		
		if (buttonText && chance == text)
		{
			buttonText.text = PlayerPrefs.GetInt(price, 100).ToString();
			
			if (PlayerPrefs.GetFloat(chance, 0.05f) >= 0.3f)
			{
				gameObject.GetComponent<Button>().interactable = false;

				buttonText.text = "Purchased";
			}
		}
	}
}
