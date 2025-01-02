using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [Range(0, 1)] public float musicVolume = 1f;
    [Range(0, 1)] public float sfxVolume = 1f;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            SetMusicVolume(PlayerPrefs.GetFloat("MusicVolume", 1f));
            SetSFXVolume(PlayerPrefs.GetFloat("SFXVolume", 1f));
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateAudioSources("Music");
        UpdateAudioSources("SFX");
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;

        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();
        
        UpdateAudioSources("Music");
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;

        PlayerPrefs.SetFloat("SFXVolume", volume);
        PlayerPrefs.Save();

        UpdateAudioSources("SFX");
    }

    private void UpdateAudioSources(string tag)
    {
        foreach (var source in GameObject.FindGameObjectsWithTag(tag))
        {
            source.GetComponent<AudioSource>().volume = tag == "Music" ? musicVolume : sfxVolume;
        }
    }
}
