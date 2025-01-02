using UnityEngine;
using System.Collections; 

public class BotSpawner : MonoBehaviour
{
    public GameObject[] Bots; 
    public float minSpawnDelay = 1f; 
    public float maxSpawnDelay = 3f; 
    public int countOfBotsType;
    public bool IsShouldSpawn = true;
    public Coroutine SpawnBotsCoroutine = null;
    
    private MakePoints spawnPoints;
    
    private void OnEnable() 
    {
    	ShapeManager.OnShapeChanged += SetCountOfBotsType;
    }
    
    private void OnDisable() 
    {
    	ShapeManager.OnShapeChanged -= SetCountOfBotsType;
    }
    
    private void SetCountOfBotsType()
    {
    	GameObject player = GameObject.Find("P_Player(Clone)");
    	MakePoints[] allShapePoints = player.GetComponentsInChildren<MakePoints>();

        foreach (var shapePoint in allShapePoints)
        {
    	    if (shapePoint.gameObject.activeInHierarchy)
    	    {
            	countOfBotsType = shapePoint.points.Length; 
            	break; 
            }
    	}
    }

    private void Start()
    {
        spawnPoints = GetComponent<MakePoints>();
        
        SpawnBotsCoroutine = StartCoroutine(SpawnBots());
    }
    
    public void StopBotSpawnCoroutine()
    {
        if (SpawnBotsCoroutine != null)
		{
			StopCoroutine(SpawnBotsCoroutine);
		}

		SpawnBotsCoroutine = null;
    }

    public void StartSpawnBotsCoroutine()
    {
        SpawnBotsCoroutine = StartCoroutine(SpawnBots());
    }

    public IEnumerator SpawnBots()
    {
        while (IsShouldSpawn)
        {
            float spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);

            yield return new WaitForSeconds(spawnDelay);

            int randomPointIndex = Random.Range(0, spawnPoints.points.Length);
            Transform spawnPoint = spawnPoints.points[randomPointIndex];

            GameObject randomBot = Bots[Random.Range(0, countOfBotsType)];

            Instantiate(randomBot, spawnPoint.position, spawnPoint.rotation);
        }
    }
}

