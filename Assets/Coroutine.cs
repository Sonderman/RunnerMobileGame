using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coroutine : MonoBehaviour
{
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveToTarget(target,2));
    }
    IEnumerator MoveToTarget(Transform target,float moveSpeed)
    {
        while (transform.position != target.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * moveSpeed);
            yield return null;
        }
        Debug.Log("DONE");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
