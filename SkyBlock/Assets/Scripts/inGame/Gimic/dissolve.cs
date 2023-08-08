using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class dissolve : MonoBehaviour
{
  
    private float dissolveCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            dissolveCount += 1;
            if (dissolveCount >= 2)
            {
                Destroy(gameObject, 0.5f);
            }
        }
    }

}
