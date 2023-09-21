using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudeMove : MonoBehaviour
{
 
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(Vector3.right*speed * Time.deltaTime);

        

        if(transform.position.x >= 180)
        {
            transform.position = new Vector3(-300, transform.position.y, transform.position.z);
        }


    }
}
