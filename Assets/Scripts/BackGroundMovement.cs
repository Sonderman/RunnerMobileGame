using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMovement : MonoBehaviour
{
   
    void Update()
    {
        GetComponent<Renderer>().material.mainTextureOffset += new Vector2(Time.deltaTime * 0.01f, 0f);
    }
}
