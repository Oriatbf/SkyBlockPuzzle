using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator= GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.N))
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 2, transform.position.z), 7f * Time.deltaTime);

        }

        if (Input.GetKey(KeyCode.M))
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 4.1f, transform.position.z), 7f * Time.deltaTime);
        }
    }

  

   /* private void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            other.transform.position =  new Vector3(other.transform.position.x,transform.position.y, other.transform.position.z);
        }
        else
        {
            other.transform.SetParent(null);
        }
    }*/
}
