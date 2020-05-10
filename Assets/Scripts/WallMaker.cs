using UnityEngine;

public class WallMaker : MonoBehaviour
{
    public Transform lastWall;
    public GameObject wallPrefab;
    Camera cam;
    PlayerController player;
    Vector3 lastPos;
    void Start()
    {
        lastPos = lastWall.position;
        cam = Camera.main;
        player = FindObjectOfType<PlayerController>();
        InvokeRepeating("CreateWalls", 0.5f,0.1f);
    }

    private void CreateWalls()
    {
        float distance = Vector3.Distance(lastPos, player.transform.position);
        if (distance > cam.orthographicSize * 2) return;
        
        Vector3 newPos;
        int rand = Random.Range(0, 11);
        if (rand <= 5)
        {
            newPos = new Vector3(lastPos.x + 0.5f, lastPos.y, lastPos.z - 0.866f);
        }
        else
        {
            newPos = new Vector3(lastPos.x+ 0.866f, lastPos.y, lastPos.z +0.5f);
        }
        GameObject newBlock = Instantiate(wallPrefab, newPos,Quaternion.Euler(0,-30,0),transform);
        newBlock.transform.GetChild(0).gameObject.SetActive(rand % 3 == 2);
        lastPos = newBlock.transform.position;
    }
}
