using YG; 
using UnityEngine;
using UnityEngine.UI;
using System;

public class RewardedAdForCoins : MonoBehaviour
{
    [SerializeField] private int _coinsCount;
    [SerializeField] private CurrencyManager _currencyManager;
    [SerializeField] private Button rewardButton;

    private const long rewardCooldown = 2 * 60 * 1000;
    private long lastRewardTime;

    private void OnEnable()
    {
        YandexGame.RewardVideoEvent += OnAdSuccess;
    }

    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= OnAdSuccess;
    }

    private void OnAdSuccess(int id)
    {
        if (id == 1)
        {
            _currencyManager.AddCurrency(_coinsCount); 

            lastRewardTime = YandexGame.ServerTime();
            PlayerPrefs.SetString("LastRewardTime", lastRewardTime.ToString());
        }
    }

    private void Start()
    {
        lastRewardTime = long.Parse(PlayerPrefs.GetString("LastRewardTime", "0"));
    }

    private void FixedUpdate()
    {
        ActivateButton();
    }

    private void ActivateButton()
    {
        long currentTime = YandexGame.ServerTime();

        if (!rewardButton.interactable && currentTime - lastRewardTime >= rewardCooldown)
            rewardButton.interactable = true;
    }
}

