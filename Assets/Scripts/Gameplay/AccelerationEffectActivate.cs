using System.Collections;
using UnityEngine;

public class AccelerationEffectActivate : MonoBehaviour
{
    public ParticleSystem accelerateTrail;
    
    public static bool IsActive = false;
    
    private void Start()
    {
    	if (IsActive)
    		ActivateEffect();
    }
	
    private void OnEnable()
    {
    	AccelerationDeBuff.OnAccelerationDeBuffApplied += ActivateEffect;
    }
    
    private void OnDisable()
    {
    	AccelerationDeBuff.OnAccelerationDeBuffApplied -= ActivateEffect;
    }
    
    private void ActivateEffect()
    {
    	IsActive =  true;
    	
        if (accelerateTrail != null)
    	    accelerateTrail.Play();
    }
}
