using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    public GameObject coinPrefab; 
    public GameObject freezeBuffPrefab; 
    public GameObject invisibleBuffPrefab;
    public GameObject accelerationDeBuffPrefab;
    
    private float _coinSpawnChance; 
    private float _freezeBuffSpawnChance; 
    private float _invisibleBuffSpawnChance;
    private float _accelerationDeBuffSpawnChance;
    
    private void OnEnable() 
    {
    	gameObject.GetComponent<SpawnDestroyEffect>().OnWinned += SpawnBuffOrCoin;
    	gameObject.GetComponent<SpawnDestroyEffect>().OnLosed += SpawnDeBuff;
    }
    
    private void OnDisable() 
    {
    	gameObject.GetComponent<SpawnDestroyEffect>().OnWinned -= SpawnBuffOrCoin;
    	gameObject.GetComponent<SpawnDestroyEffect>().OnLosed -= SpawnDeBuff;
    }
    
    private void Start()
    {
        _coinSpawnChance = PlayerPrefs.GetFloat("CoinSpawnChance", 0.1f); 
        _freezeBuffSpawnChance = PlayerPrefs.GetFloat("FreezeBuffSpawnChance", 0.05f); 
        _invisibleBuffSpawnChance = PlayerPrefs.GetFloat("InvisibleBuffSpawnChance", 0.05f);
        
        _accelerationDeBuffSpawnChance = 0.1f;
    }

    private void SpawnBuffOrCoin()
    {
    	List<GameObject> potentialDrops = new List<GameObject>();
    
        if (Random.value <= _freezeBuffSpawnChance)
        {
            potentialDrops.Add(freezeBuffPrefab);
        }
        else if (Random.value <= _invisibleBuffSpawnChance)
        {
            potentialDrops.Add(invisibleBuffPrefab);
        }
        else if (Random.value <= _coinSpawnChance)
        {
            potentialDrops.Add(coinPrefab);
        }
        
        if (potentialDrops.Count > 0)
        {
        	int randomIndex = Random.Range(0, potentialDrops.Count);
        	
        	Instantiate(potentialDrops[randomIndex], transform.position, Quaternion.identity);
        }	
    }
    
    private void SpawnDeBuff()
    {
    	if (Random.value <= _accelerationDeBuffSpawnChance)
        {
            Instantiate(accelerationDeBuffPrefab, transform.position, Quaternion.identity);
        }
    }
}
