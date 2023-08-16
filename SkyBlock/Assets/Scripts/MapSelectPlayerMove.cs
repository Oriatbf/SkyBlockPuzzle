using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSelectPlayerMove : MonoBehaviour
{

    public Transform targetbutton;

    private void Update()
    {
        transform.position = Vector3.MoveTowards(gameObject.transform.position, targetbutton.position, 0.2f);
       
    }

    public void Settarget(Transform target)
    {
        targetbutton = target;
    }
}