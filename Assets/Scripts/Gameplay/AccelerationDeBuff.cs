using System;
using UnityEngine;

public class AccelerationDeBuff : MonoBehaviour
{
    public static event Action OnAccelerationDeBuffApplied;
    
    private void OnDestroy()
    {
    	OnAccelerationDeBuffApplied?.Invoke();
    }
}
