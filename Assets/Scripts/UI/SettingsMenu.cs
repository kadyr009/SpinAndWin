using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;

    private void Start()
    {
        musicSlider.value = AudioManager.Instance.musicVolume;
        sfxSlider.value = AudioManager.Instance.sfxVolume;

        musicSlider.onValueChanged.AddListener(value => AudioManager.Instance.SetMusicVolume(value));
        sfxSlider.onValueChanged.AddListener(value => AudioManager.Instance.SetSFXVolume(value));
    }
}

