using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;


public class Spider : MonoBehaviour
{
    public GameObject player;
    private Vector3 nPosition;
    public bool Move;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    
    // Update is called once per frame
    void Update()
    {
        


        if (Move)
        {
            transform.position = Vector3.MoveTowards(transform.position, nPosition, Time.deltaTime * 2f);
        }
        
    }

    private void FixedUpdate()
    {
       
        if(Physics.Raycast(transform.position , Vector3.forward, 2))
        {

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.forward * 2);
        Gizmos.DrawRay(transform.position, Vector3.left * 2);
        Gizmos.DrawRay(transform.position, Vector3.right * 2);
        Gizmos.DrawRay(transform.position, Vector3.back * 2);
    }
   

    IEnumerator position()
    {
        nPosition = transform.position + transform.TransformDirection(Vector3.forward) * 2f;
        yield return new WaitForSeconds(0.3f);
        Move = true;
    }
}
