using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 _diffVector;
    private Transform _player;

    private void Start()
    {
        _player = FindObjectOfType<PlayerController>().transform;
        _diffVector = _player.position - transform.position;
    }
    private void Update()
    {
        transform.position = _player.position - _diffVector;
    }
}
