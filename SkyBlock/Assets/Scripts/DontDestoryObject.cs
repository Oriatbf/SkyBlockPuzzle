using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestoryObject : MonoBehaviour
{
    public static DontDestoryObject instance;
    private void Awake()
    {
        if(instance != gameObject && instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance= this;
            DontDestroyOnLoad(gameObject);
        }
        
    }
}
