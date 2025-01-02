using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText; 
    public TextMeshProUGUI lifesText;
	public TextMeshProUGUI coinsText;
	
    private void OnEnable()
    {
        GameManager.OnPointsChanged += UpdateScoreUI;
        GameManager.OnLifeChanged += UpdateLifesUI;
        InGameCurrencyManager.OnCoinsCountChange += UpdateCoinsUI;
    }

    private void OnDisable()
    {
        GameManager.OnPointsChanged -= UpdateScoreUI;
        GameManager.OnLifeChanged -= UpdateLifesUI;
        InGameCurrencyManager.OnCoinsCountChange -= UpdateCoinsUI;
    }

    private void UpdateScoreUI(int points)
    {
        string score = "\u2666";
        scoreText.text = $"{score}: {points}";
    }

    private void UpdateLifesUI(int lifes)
    {
        string live = "\u2665";
        lifesText.text = $"{live}: {lifes}";
    }
    
    private void UpdateCoinsUI(int coins)
    {
        string coin = "\u25CF";
        coinsText.text = $"{coin}: {coins}";
    }
}

