using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public enum GameMode { Levels, Endless }
    public static GameMode CurrentGameMode = GameMode.Levels;
    public static bool IsSimpleMode = true;
    public static int LevelNumber;
    
    public GameObject Pawn;
    public BotSpawner BotSpawner;
    public PauseMenu GameMenu;
    
    public int MaxPoints {get => _maxPoints; }
    public int Points {get => _points; }
    public int Lives {get => _lives; }
    
    [SerializeField] private int _lives = 3;
    [SerializeField] private int _maxPoints = 15;
    private int _maxLives = 5;
    private int _points;
    private GameObject _currentPawn;
    private bool _isDifficultyIncreased = false;
	private Coroutine _activeCoroutine = null;
    
    public static event Action<int> OnLifeChanged;
    public static event Action<int> OnPointsChanged;
    public static event Action OnWinning;
    public static event Action OnLevelEnding;
    public static event Action<bool> OnSimpleModeChanged;
    
    private void OnEnable() 
    {
    	TriggerCollisionHandler.OnAddPoints += AddPoints;
    	TriggerCollisionHandler.OnSubsLife += DecreaseLives;
    	BotController.OnStartNormalize += StartNormalize;
    }
    
    private void OnDisable() 
    {
    	TriggerCollisionHandler.OnAddPoints -= AddPoints;
    	TriggerCollisionHandler.OnSubsLife -= DecreaseLives;
    	BotController.OnStartNormalize -= StartNormalize;
    }
    
    private void Start()
    {
        if (CurrentGameMode == GameMode.Endless)
    	    _maxPoints = Int32.MaxValue;
    	else
            _maxPoints = (Math.Clamp(LevelNumber * 10, 15, 100));
    	
    	OnLifeChanged?.Invoke(_lives);
    	OnPointsChanged?.Invoke(_points);
    	OnSimpleModeChanged?.Invoke(IsSimpleMode);
    	
    	_currentPawn = GameObject.Find("P_Player(Clone)");
    	
    	AccelerationEffectActivate.IsActive = false;
    	FreezeEffectActivate.IsActive = false;
    }
    
    private void DecreaseLives()
    {
    	_lives = Math.Clamp(--_lives, 0, _maxLives);
    	OnLifeChanged?.Invoke(_lives);
    	
    	CheckGameStatus();
    }
    
    public void SetLives(int newLives)
    {
    	_lives = Math.Clamp(newLives, 0, _maxLives);
    	OnLifeChanged?.Invoke(_lives);
    }
    
    private void AddPoints()
    {
		_points = Math.Clamp(++_points, 0, _maxPoints);
		OnPointsChanged?.Invoke(_points);

		if (_points % 5 == 0)
		{
			IncreaseGameDifficulty();
		}

		CheckGameStatus();
    }

    private void IncreaseGameDifficulty()
    {
		BotController.speed += 0.5f;
		BotSpawner.maxSpawnDelay = Mathf.Max(1f, BotSpawner.maxSpawnDelay - 0.5f);
    }

    
    private void CheckGameStatus() 
    {
		if (CurrentGameMode == GameMode.Levels && _points == _maxPoints / 2 && !_isDifficultyIncreased)
		{
			SwitchToAdvancedMode(false);
			
			_isDifficultyIncreased = true;
		}
		else if (_points >= _maxPoints)
		{
			OnWin();
		}
		else if (_lives <= 0)
		{
			OnLose();
		}
    }

    public void SwitchToAdvancedMode(bool isSimple)
    {
		IsSimpleMode = isSimple;
		OnSimpleModeChanged?.Invoke(IsSimpleMode);

		BotSpawner.StopBotSpawnCoroutine();
		
		StartCoroutine(DefrostPerDelay(BotController.speed, 3f));

		BotController.speed = 0f;
    }
    
    private void OnWin()
    {
        GameMenu.ActivateWinMenu();
        
        OnWinning?.Invoke();
    }
    
    private void OnLose()
    {
        GameMenu.ActivateLoseMenu();
    }
    
    private IEnumerator DefrostPerDelay(float currentSpeed, float spawnDelay = 5f)
    {
		yield return new WaitForSeconds(spawnDelay);
		
		if (CurrentGameMode == GameMode.Levels)
		{
			BotController.speed = 3f;
			BotSpawner.maxSpawnDelay = 3f;
		}
		else if (CurrentGameMode == GameMode.Endless)
		{
			BotController.speed = currentSpeed;
		}

		BotSpawner.StartSpawnBotsCoroutine();
    }
    
    public void CompleteLevel()
    {
		OnLevelEnding?.Invoke();
    }
    
    private void StartNormalize()
    {
		if (_activeCoroutine != null)
		{
			StopCoroutine(_activeCoroutine);
			
			_activeCoroutine = null;
		}

    	_activeCoroutine = StartCoroutine(NormalizePerDelay());
    }
    
    private static IEnumerator NormalizePerDelay(float spawnDelay = 5f)
    {
		yield return new WaitForSeconds(spawnDelay);

		BotController.speed = Math.Max(1f, BotController.speedTemp);
			
		FreezeEffectActivate.IsActive = false;
		AccelerationEffectActivate.IsActive = false;
    }
}

