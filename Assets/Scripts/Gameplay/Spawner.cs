using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject player;

    private GameManager _gameManager;
    
    private void Awake()
    {
    	_gameManager = GameObject.Find("P_LevelCore").GetComponent<GameManager>();
    }

    private void Start()
    {
        player = Instantiate(_gameManager.Pawn, transform);
    }
}
