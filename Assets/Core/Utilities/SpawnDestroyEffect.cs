using UnityEngine;
using UnityEngine.Events;

public class SpawnDestroyEffect : MonoBehaviour
{
    [SerializeField] private GameObject _winningEffect;
    [SerializeField] private GameObject _losingEffect;
    [SerializeField] private GameObject _drawEffect;

    public event UnityAction OnWinned;
    public event UnityAction OnLosed;

    private void OnEnable() 
    {
    	ShapeManager.OnLossLife += OnDraw;
    }
    
    private void OnDisable() 
    {
    	ShapeManager.OnLossLife -= OnDraw;
    }
    
    public void OnWinning()
    {
    	OnWinned?.Invoke();
    	
	    SpawnDeathEffect(_winningEffect);
    }
    
    public void OnLosing()
    {
    	OnLosed?.Invoke();
    	
	    SpawnDeathEffect(_losingEffect);
    }

    public void OnDraw()
    {
	    SpawnDeathEffect(_drawEffect);
    }
    
    private void SpawnDeathEffect(GameObject deathEffect)
    {
    	GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
	    effect.GetComponent<Renderer>().material = GetComponent<Renderer>().material;
	
	    Destroy(gameObject);
    }
}
