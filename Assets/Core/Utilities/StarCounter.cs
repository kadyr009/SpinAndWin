using UnityEngine;
using UnityEngine.UI;
using System;

public class StarCounter : MonoBehaviour
{
    public GameObject[] stars = new GameObject[3];
    public GameManager CurrentManager;
    public int starsEarned = 0;
    public int level;
    
    private int _maxLives = 5; 
    private float _startTime;
    private float _completeTime;
    private float _maxTimeToGetSecondStar;
    
    private void OnEnable() 
    {
    	GameManager.OnWinning += ActivateStars;
    }
    
    private void OnDisable() 
    {
    	GameManager.OnWinning -= ActivateStars;
    }
    
    private void Start()
    {
        _startTime = Time.time;
        
        starsEarned = LoadStarsForLevel(level);
        
        if (starsEarned > 0 && level > 0)
	    ActivateStars();
	    
        if (gameObject.GetComponent<Button>())
            UnlockLevel(level);
    }
    
    private void UnlockLevel(int level)
    {
    	if (LoadStarsForLevel(level - 1) > 0 || level == 1)
    	    gameObject.GetComponent<Button>().interactable = true;
        else
            gameObject.GetComponent<Button>().interactable = false;
    }
    
    private void ActivateStars()
    {
    	foreach (var star in stars)
    	    star.SetActive(false);
    	    
    	SetStarCount();
    	
        for (int i = 0; i < starsEarned; i++)
            stars[i].SetActive(true);

	    SaveStarsForLevel();
    }
    
    private void SetStarCount()
    {
    	if (CurrentManager)
    	{
            _maxTimeToGetSecondStar = CurrentManager.MaxPoints * 3f; 
            _completeTime = Time.time;
            
            if (CurrentManager.MaxPoints == CurrentManager.Points)
                starsEarned = 1;
                
            if (_completeTime - _startTime <= _maxTimeToGetSecondStar)
                starsEarned++;
            
            if (CurrentManager.Lives == _maxLives)
                starsEarned++;
        }
    }

    private void SaveStarsForLevel()
    {
        int currentLevel = GameManager.LevelNumber > 0 ? GameManager.LevelNumber : -1;
        
        if (currentLevel > 0)
        {
            PlayerPrefs.SetInt("Level_" + currentLevel + "_Stars", Math.Max(starsEarned, LoadStarsForLevel(currentLevel)));
            PlayerPrefs.Save();
        }
    }

    public int LoadStarsForLevel(int level)
    {
        return PlayerPrefs.GetInt("Level_" + level + "_Stars", 0);
    }
}
