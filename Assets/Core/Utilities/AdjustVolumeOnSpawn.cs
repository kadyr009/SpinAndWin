using UnityEngine;

public class AdjustVolumeOnSpawn : MonoBehaviour
{
    private void Start()
    {
        AudioSource audioSource = GetComponent<AudioSource>();

        if (audioSource != null)
            audioSource.volume = AudioManager.Instance.sfxVolume;
    }
}
