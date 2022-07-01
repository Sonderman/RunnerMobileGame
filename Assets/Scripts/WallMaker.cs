using UnityEngine;

public class WallMaker : MonoBehaviour
{
    public Transform lastWall;
    public GameObject wallPrefab;
    private Camera _cam;
    private PlayerController _player;
    private Vector3 _lastPos;

    private void Start()
    {
        _lastPos = lastWall.position;
        _cam = Camera.main;
        _player = FindObjectOfType<PlayerController>();
        InvokeRepeating("CreateWalls", 0.5f,0.1f);
    }

    private void CreateWalls()
    {
        float distance = Vector3.Distance(_lastPos, _player.transform.position);
        if (distance > _cam.orthographicSize * 2) return;
        
        Vector3 newPos;
        int rand = Random.Range(0, 11);
        if (rand <= 5)
        {
            newPos = new Vector3(_lastPos.x + 0.5f, _lastPos.y, _lastPos.z - 0.866f);
        }
        else
        {
            newPos = new Vector3(_lastPos.x+ 0.866f, _lastPos.y, _lastPos.z +0.5f);
        }
        GameObject newBlock = Instantiate(wallPrefab, newPos,Quaternion.Euler(0,-30,0),transform);
        newBlock.transform.GetChild(0).gameObject.SetActive(rand % 3 == 2);
        _lastPos = newBlock.transform.position;
    }
}
