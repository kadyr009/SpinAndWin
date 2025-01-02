using UnityEngine;
using System.Collections;

public class FreezeEffectActivate : MonoBehaviour
{
	public ParticleSystem iceTrail;
    
    public static bool IsActive = false;

    private void Start()
    {
    	if (IsActive)
    		ActivateEffect();
    }
	
    private void OnEnable()
    {
    	FreezeBuff.OnFreezeBuffApplied += ActivateEffect;
    }
    
    private void OnDisable()
    {
    	FreezeBuff.OnFreezeBuffApplied -= ActivateEffect;
    }
    
    private void ActivateEffect()
    {
    	IsActive = true;
    	
        if (iceTrail != null)
    	    iceTrail.Play();
    }
}
