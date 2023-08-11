using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorButton : MonoBehaviour
{
    public LayerMask player;
    public GameObject connectElevator;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Physics.Raycast(transform.position, transform.up, 2, player))
        {
            connectElevator.GetComponent<Elevator>().goDown = true;
        }
        else
        {
            connectElevator.GetComponent<Elevator>().goDown = false;
        }*/
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.up * 2);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("PushBlock"))
        {
            connectElevator.GetComponent<Elevator>().goDown = true;
        }
    }
}
