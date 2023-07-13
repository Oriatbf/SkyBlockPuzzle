using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Horse : MonoBehaviour
{
    private Vector3 myTrans;
    public LayerMask platform;
    public LayerMask enemy;
    public Transform obstacleFindtrans;
    public Vector3 obstacleFindSize;
    public static bool rightHorseNoGO;
    public static bool leftHorseNoGO;
    public static bool backHorseNoGo;
    // Start is called before the first frame update
    void Start()
    {
        obstacleFindSize = new Vector3(4, 0.27f, 0.34f);
    }

    // Update is called once per frame
    void Update()
    {
        if(Physics.Raycast(transform.position+new Vector3(0,0.5f,0), transform.forward, 5, enemy))
        {
            Player.horseNoGo= true;
        }
        else
        {
            Player.horseNoGo= false;
        }
        if (Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), transform.right *-1, 5, enemy))
        {
           leftHorseNoGO = true;
        }
        else
        {
            leftHorseNoGO = false;
        }
        if (Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), transform.right, 5, enemy))
        {
            rightHorseNoGO = true;
        }
        else
        {
            rightHorseNoGO = false;
        }
        if (Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), transform.forward*-1, 5, enemy))
        {
            backHorseNoGo = true;
        }
        else
        {
            backHorseNoGo = false;
        }
        if (!Physics.Raycast(transform.position + new Vector3(0, 0.3f, 0), Vector3.down, 3, platform))
        {
            Destroy(gameObject);
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + new Vector3(0, 0.5f, 0), transform.forward*5);
        Gizmos.DrawRay(transform.position + new Vector3(0, 0.3f, 0), Vector3.down * 3);
    }

   
}
