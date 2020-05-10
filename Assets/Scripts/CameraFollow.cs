using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Vector3 diffVector;
    Transform player;
    void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
        diffVector = player.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position - diffVector;
    }
}
