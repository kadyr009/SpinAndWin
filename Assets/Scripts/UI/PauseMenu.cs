using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class PauseMenu : MonoBehaviour
{
    public bool isPaused;
    public GameObject pauseGameMenu;
    public GameObject WinMenu;
    public GameObject LoseMenu;
    public GameManager CurrentGameManager;
    public InGameCurrencyManager currencyManager;
    
	private void OnEnable()
    {
        YandexGame.RewardVideoEvent += Continue;
		YandexGame.RewardVideoEvent += DoubleCoins;
    }

    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= Continue;
		YandexGame.RewardVideoEvent -= DoubleCoins;
    }

	private void Awake()
	{
		Time.timeScale = 0f;
    	isPaused = true;
	}

    private void Update()
    {
		if (Input.GetKeyDown(KeyCode.Escape))
			if (isPaused)
				Resume();
			else
				Pause();        
    }
    
    public void ActivateWinMenu()
    {
    	WinMenu.SetActive(true);
    	Time.timeScale = 0f;
    	isPaused = true;
    }
    
    public void ActivateLoseMenu()
    {
    	LoseMenu.SetActive(true);
    	Time.timeScale = 0f;
    	isPaused = true;
    }
    
    public void Resume()
    { 
    	pauseGameMenu.SetActive(false);
    	Time.timeScale = 1f;
    	isPaused = false;
    }
    
    public void Continue(int id)
    { 
		if (id != 2) return;
		
    	LoseMenu.SetActive(false);
    	Time.timeScale = 1f;
    	isPaused = false;
    	
    	CurrentGameManager.SetLives(3);
    }
    
    public void Pause()
    { 
    	pauseGameMenu.SetActive(true);
    	Time.timeScale = 0f;
    	isPaused = true;
    }
    
    public void NextLevel()
    { 
    	BotController.speed = 3f;
		GameManager.IsSimpleMode = true;
	
    	WinMenu.SetActive(false);
    	Time.timeScale = 1f;
    	isPaused = false;
    	
    	CurrentGameManager.CompleteLevel();
    	GameManager.LevelNumber++;
    	
    	SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void DoubleCoins(int id)
    {
		if (id != 3) return;

    	currencyManager.AddCurrency(currencyManager.CurrentLevelCurrency);
    }
    
    public void Restart()
    { 
    	BotController.speed = 3f;
		GameManager.IsSimpleMode = true;
	
    	LoseMenu.SetActive(false);
    	Time.timeScale = 1f;
    	isPaused = false;
    	
    	CurrentGameManager.CompleteLevel();
    	
    	SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void Menu()
    {
		BotController.speed = 3f;
		GameManager.IsSimpleMode = true;

		CurrentGameManager.CompleteLevel();
	 
    	SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    	Time.timeScale = 1f;
    	isPaused = false;
    	GameManager.LevelNumber = 0;
    }
}
