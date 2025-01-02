using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkinSelectButton : MonoBehaviour
{
    public SkinOwner owner;
    public int skinIndex;

    private TextMeshProUGUI buttonText;

    private void OnEnable()
    {
        SkinManager.OnSkinChanged += UpdateButton;
        SkinShop.OnSkinBuyed += UpdateButton;
    }

    private void OnDisable()
    {
        SkinManager.OnSkinChanged -= UpdateButton;
        SkinShop.OnSkinBuyed -= UpdateButton;
    }

    private void Start()
    {
        buttonText = GetComponentInChildren<TextMeshProUGUI>();

		UpdateButton();
    }

    private void UpdateButton()
    {
        string skinKey = $"{owner}_skin_{skinIndex}";

        if (PlayerPrefs.GetInt(skinKey, 0) == 1)
        {
            bool isSelected = PlayerPrefs.GetInt($"{owner}_SelectedSkin", 0) == skinIndex;
            buttonText.text = isSelected ? "Selected" : "Select";
            GetComponent<Button>().interactable = !isSelected;
        }
		else if (skinIndex == 0)
		{
			PlayerPrefs.SetInt(skinKey, 1);
        	PlayerPrefs.Save();

			UpdateButton();
		}
        else
        {
            buttonText.text = "Select";
            GetComponent<Button>().interactable = false;
        }
    }
}
