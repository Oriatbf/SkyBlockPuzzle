using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] LayerMask Platform;
    
    Vector3 nPosition;

    void Start()
    {
        nPosition = transform.position;
    }
    void Update()
    {
        if (Vector3.Distance(transform.position, nPosition) > 0.3f)
            Move();
        else
            transform.position = nPosition;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, Platform))
            {
                nPosition = hit.transform.position;
                transform.LookAt(new Vector3(nPosition.x, transform.position.y, nPosition.z));
            }
        }
    }

    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, nPosition, 3f * Time.deltaTime);
    }
}
