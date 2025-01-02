using System.Collections.Generic;
using UnityEngine;
using System;

public class ShapeManager : MonoBehaviour
{
    private GameManager _currentGameManager;
    private int _currentPoints;
    private Dictionary<string, int> _shapePointsThreshold = new Dictionary<string, int>
    {
		{"Plane", 20},
        {"Triangle", 30},
        {"Box", 40},
        {"Pentagon", Int32.MaxValue}
    };
    
    public static event Action OnShapeChanged;
	public static event Action OnLossLife;
    public static event Action<string> OnTagChanged;
    
    private void Start()
    {
    	_currentGameManager = GameObject.Find("P_LevelCore").GetComponent<GameManager>();
    	
    	string tag;
    	 
    	if (GameManager.LevelNumber < 6)
			tag = "Plane";
		else if (GameManager.LevelNumber < 11)
			tag = "Triangle";
		else if (GameManager.LevelNumber < 16)
			tag = "Box";
		else
			tag = "Pentagon";
	
		SetShape(tag);
	
    	OnShapeChanged?.Invoke();
    	OnTagChanged?.Invoke(tag);
    }
    
    private void SetShape(string tag)
    {
    	foreach (Transform child in transform)
		{
			if (child.tag == tag || child.tag == "Untagged")
				child.gameObject.SetActive(true);
			else
				child.gameObject.SetActive(false);
		}
    }
    
    private void OnEnable() 
    {
    	TriggerCollisionHandler.OnAddPoints += AddCurrentPoints;
		TriggerCollisionHandler.OnSubsLife += ChangeToPrevShape;
    }
    
    private void OnDisable() 
    {
    	TriggerCollisionHandler.OnAddPoints -= AddCurrentPoints;
		TriggerCollisionHandler.OnSubsLife -= ChangeToPrevShape;
    }
    
    private void AddCurrentPoints()
    {
    	_currentPoints++;
    	
		if (GameManager.CurrentGameMode != GameManager.GameMode.Endless) return;

        string currentShapeTag = GetActiveShapeTag();
        
        if (_shapePointsThreshold.ContainsKey(currentShapeTag))
        {
            if (_currentPoints >= _shapePointsThreshold[currentShapeTag])
	    	{
	    		ChangeToNextShape();
	    	
	    		_currentGameManager.SwitchToAdvancedMode(true);
            }
            else if (_currentPoints == _shapePointsThreshold[currentShapeTag] / 2)
            {
            	_currentGameManager.SwitchToAdvancedMode(false);
            }
        }
    }

    private string GetActiveShapeTag()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf)
                return child.tag;
        }

        return string.Empty;
    }

    private void ChangeToNextShape()
    {
		_currentPoints = 0;
		
		Transform currentActiveShape = null;

		foreach (Transform child in transform)
		{
			if (child.gameObject.activeSelf)
			{
				currentActiveShape = child;
				break;
			}
		}

		if (currentActiveShape == null) return;

		Transform[] shapes = GetComponentsInChildren<Transform>(true);

		int currentShapeIndex = Array.IndexOf(shapes, currentActiveShape);

		for (int i = currentShapeIndex + 1; i < shapes.Length; i++)
		{
			if (!shapes[i].gameObject.activeSelf)
			{
				shapes[i].gameObject.SetActive(true);
				
				currentActiveShape.gameObject.SetActive(false);

				OnTagChanged?.Invoke(GetActiveShapeTag());
				OnShapeChanged?.Invoke();
				return;
			}
		}

		for (int i = 0; i < currentShapeIndex; i++)
		{
			if (!shapes[i].gameObject.activeSelf)
			{
				shapes[i].gameObject.SetActive(true);
				currentActiveShape.gameObject.SetActive(false);

				return;
			}
		}
	}

	private void ChangeToPrevShape()
    {
		if (GameManager.LevelNumber != 0) return;

		Transform currentActiveShape = null;

		foreach (Transform child in transform)
		{
			if (child.gameObject.activeSelf)
			{
				currentActiveShape = child;
				break;
			}
		}

		if (currentActiveShape == null) return;

		Transform[] shapes = GetComponentsInChildren<Transform>(true);

		int currentShapeIndex = Array.IndexOf(shapes, currentActiveShape);

		if (currentShapeIndex == 0) return;

		for (int i = currentShapeIndex - 1; i >= 0; i--)
		{
			if (!shapes[i].gameObject.activeSelf)
			{
				shapes[i].gameObject.SetActive(true);
				
				currentActiveShape.gameObject.SetActive(false);

				_currentGameManager.SwitchToAdvancedMode(true);

				_currentPoints = 0;

				OnTagChanged?.Invoke(GetActiveShapeTag());
				OnShapeChanged?.Invoke();
				OnLossLife?.Invoke();
				return;
			}
		}
	}
}

