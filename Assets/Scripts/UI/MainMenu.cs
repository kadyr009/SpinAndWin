using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public GameObject loadingScreen;
	public Slider progressBar;

	private void LoadLevel(int sceneIndex)
	{
		StartCoroutine(LoadAsync(sceneIndex));
	}

	private IEnumerator LoadAsync(int sceneIndex)
	{
		loadingScreen.SetActive(true);

		AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
		operation.allowSceneActivation = false;

		while (!operation.isDone)
		{
			float progress = Mathf.Clamp01(operation.progress / 0.9f);
			progressBar.value = progress;

			if (operation.progress >= 0.9f)
			{
				progressBar.value = 1f;

				yield return new WaitForSeconds(1f);

				operation.allowSceneActivation = true; 
			}

			yield return null;
		}
	}

    public void PlayGame(int levelNum) 
    {
    	if (levelNum == 0)
    	{
    	    GameManager.CurrentGameMode = GameManager.GameMode.Endless;
    	}
    	else if (levelNum > 0)
    	{
	    	GameManager.CurrentGameMode = GameManager.GameMode.Levels;
    	}
    	
    	GameManager.LevelNumber = levelNum;
    	
    	LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
