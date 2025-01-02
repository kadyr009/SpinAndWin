using UnityEngine;

public class ButtonInteraction : MonoBehaviour
{
    private PlayerController _playerController;
    private Spawner _spawner;

    private void Start()
    {
        _spawner = GameObject.Find("P_Spawner").GetComponent<Spawner>();
    }

    public void Rotate(string direction)
    {
        if (_playerController == null && _spawner != null)
            _playerController = _spawner.player.GetComponent<PlayerController>();

        _playerController.StartRotate(direction);
    }
}
