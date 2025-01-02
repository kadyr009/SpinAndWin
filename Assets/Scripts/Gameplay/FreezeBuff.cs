using System;
using UnityEngine;

public class FreezeBuff : MonoBehaviour
{
    public static event Action OnFreezeBuffApplied;
    
    private void OnDestroy()
    {
    	OnFreezeBuffApplied?.Invoke();
    }
}
