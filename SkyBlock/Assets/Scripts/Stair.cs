using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Stair : MonoBehaviour
{
    public GameObject Reach;
    public Vector3 reachPos;
    public bool one = true;
    public GameObject player;
    public bool isMove = false;
    public bool reach;
    // Start is called before the first frame update
    void Start()
    {
        reachPos = Reach.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        /*if (isMove)
        {
            player.transform.parent = transform;
            transform.position = Vector3.MoveTowards(transform.position, reachPos, Time.deltaTime * 2f);
            if (Vector3.Distance(transform.position, reachPos) < 0.1f)
            {
                reach = true;
                isMove = false;
            }
        }
           */
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && one)
        {
            player = other.gameObject;
            isMove = true;
            one = false;    
        }
    }
}
