using System;
using UnityEngine;

public class InvisibleBuff : MonoBehaviour
{
    public static event Action OnInvisibleBuffApplied;
    
    private void OnDestroy()
    {
    	OnInvisibleBuffApplied?.Invoke();
    }
}
