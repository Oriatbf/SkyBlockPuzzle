using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    Animator animator;
    public bool goDown = false;
    // Start is called before the first frame update
    void Start()
    {
        animator= GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (goDown)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 2, transform.position.z), 7f * Time.deltaTime);

        }

        if (!goDown)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 4.1f, transform.position.z), 7f * Time.deltaTime);
        }
    }
}
