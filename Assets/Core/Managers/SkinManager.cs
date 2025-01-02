using UnityEngine;
using UnityEngine.Events;

public enum SkinOwner { Player, Bot }

[System.Serializable]
public class Skin
{
    public string skinName;
    public GameObject skinModel;
}

public class SkinManager : MonoBehaviour
{
    public SkinOwner owner;
    public Skin[] skins;
    public static event UnityAction OnSkinChanged;

    private int selectedSkinIndex;

    private void Start()
    {
        LoadSkin();
        ApplySkin(selectedSkinIndex);
    }

    public void ChangeSkin(int newIndex)
    {
        selectedSkinIndex = newIndex;
        ApplySkin(newIndex);
        SaveSkin();
    }

    private void ApplySkin(int index)
    {
        foreach (var item in skins)
        {
            item.skinModel.SetActive(false);
        }

        skins[index].skinModel.SetActive(true);
    }

    private void SaveSkin()
    {
        string key = GetSkinKey();
        PlayerPrefs.SetInt(key, selectedSkinIndex);
        PlayerPrefs.Save();
        OnSkinChanged?.Invoke();
    }

    private void LoadSkin()
    {
        string key = GetSkinKey();
        selectedSkinIndex = PlayerPrefs.GetInt(key, 0);
    }

    private string GetSkinKey()
    {
        return $"{owner}_SelectedSkin";
    }
}


