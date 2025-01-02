using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class TriggerCollisionHandler : MonoBehaviour
{
    protected bool IsInvulnerable = false;

    public static event UnityAction OnAddPoints;
    public static event UnityAction OnSubsLife;
    public static event UnityAction<bool> OnBecomeInvulnerable;
    
    private void OnEnable() 
    {
    	InvisibleBuff.OnInvisibleBuffApplied += SetInvulnerability;

        IsInvulnerable = false;
        OnBecomeInvulnerable?.Invoke(IsInvulnerable);
    }
    
    private void OnDisable() 
    {
    	InvisibleBuff.OnInvisibleBuffApplied -= SetInvulnerability;
    }
    
    private void SetInvulnerability()
    {
    	IsInvulnerable = true;
        OnBecomeInvulnerable?.Invoke(IsInvulnerable);
    	
    	StartCoroutine(VulnerablePerDelay());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Circle") || 
            other.CompareTag("Square") || 
            other.CompareTag("Cross") ||
            other.CompareTag("Pyramid") ||
            other.CompareTag("Star"))
        {
            string otherTag = other.tag;
            string triggerTag = gameObject.tag; 
            
            SpawnDestroyEffect effectSpawner = other.gameObject.GetComponent<SpawnDestroyEffect>();

            if (IsWinner(GameManager.IsSimpleMode, triggerTag, otherTag))
            {
                OnAddPoints?.Invoke();

                if (effectSpawner)
                    effectSpawner.OnWinning();
            }
            else if (IsLoser(GameManager.IsSimpleMode, triggerTag, otherTag))
            {
            	OnSubsLife?.Invoke();

            	if (effectSpawner)
                    effectSpawner.OnLosing();
            }
            else
            { 
            	if(effectSpawner)
                    effectSpawner.OnDraw();
            }
        }
    }

    protected virtual bool IsWinner(bool IsSimpleMode, string triggerTag, string otherTag) 
    {
    	if (IsInvulnerable)
        {
            return true;
        }
        else if (IsSimpleMode)
    	{
            return (triggerTag == "Cross" && otherTag == "Cross") ||
               	   (triggerTag == "Square" && otherTag == "Square") ||
               	   (triggerTag == "Circle" && otherTag == "Circle") ||
                   (triggerTag == "Pyramid" && otherTag == "Pyramid") ||
                   (triggerTag == "Star" && otherTag == "Star");
        }
        
        return false;
    }
    
    protected virtual bool IsLoser(bool IsSimpleMode, string triggerTag, string otherTag)
    {
    	if (IsSimpleMode)
            return !IsWinner(IsSimpleMode, triggerTag, otherTag);
            
        return false;
    }
    
    private IEnumerator VulnerablePerDelay(float spawnDelay = 5f)
    {
        yield return new WaitForSeconds(spawnDelay);
        
        IsInvulnerable = false;
        OnBecomeInvulnerable?.Invoke(IsInvulnerable);
    }
}

