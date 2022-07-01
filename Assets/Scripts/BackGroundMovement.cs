using UnityEngine;

public class BackGroundMovement : MonoBehaviour
{
    private void Update()
    {
        GetComponent<Renderer>().material.mainTextureOffset += new Vector2(Time.deltaTime * 0.01f, 0f);
    }
}
