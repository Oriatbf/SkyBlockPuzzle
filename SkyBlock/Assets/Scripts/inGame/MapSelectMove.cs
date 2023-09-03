using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MapSelectMove : MonoBehaviour
{
    public LayerMask mapStage;
    public int playerStagePos;
    public GameObject[] Stages;
    public Vector3 pos;
    // Update is called once per frame

    private void Start()
    {
        pos= transform.position;
        playerStagePos= 0;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity,mapStage))
            {
                pos = hit.collider.transform.position;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, pos, 30f * Time.deltaTime);

    }
}
