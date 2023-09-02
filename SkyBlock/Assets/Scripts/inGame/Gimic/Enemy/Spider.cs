using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (transform.position.x < player.transform.position.x)
            {
                if (Mathf.Abs(transform.position.x) + Mathf.Abs(player.transform.position.x) < 4)
                {
                    if (transform.position.z > player.transform.position.z)
                    {
                        transform.eulerAngles = new Vector3(0, 180, 0);
                    }
                    else
                    {
                        transform.eulerAngles = new Vector3(0, 0, 0);
                    }
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 90, 0);
                }

                

            }

            StartCoroutine(position());
            
        }


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
