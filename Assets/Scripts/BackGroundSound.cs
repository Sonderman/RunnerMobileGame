using UnityEngine;

public class BackGroundSound : MonoBehaviour
{
    private static BackGroundSound _instance;

    private void Start()
    {
        if (!_instance)
        {
            _instance = this;
        }else if(_instance != this)
        {
            Destroy(gameObject);
        }
       
        DontDestroyOnLoad(gameObject);
    }

}
