using UnityEngine;
using UnityEngine.Events;
using System;

public class CoinAdder : MonoBehaviour
{
    public static event Action<int> OnCoinAdded;
    
    private void OnDestroy()
    {
    	OnCoinAdded?.Invoke(UnityEngine.Random.Range(3, 5));
    }
}
