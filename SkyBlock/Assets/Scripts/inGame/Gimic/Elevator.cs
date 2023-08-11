using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    Animator animator;
    public bool goDown = false;
    public LayerMask player;
    public bool OnPlayer = false;
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
        if (Physics.Raycast(transform.position, transform.forward, 2, player) && !OnPlayer)
        {
            if (goDown)
            {
                OnPlayer= true;
                StartCoroutine(onPlayer());
                goDown= false;
            }
            else if (!goDown)
            {
                OnPlayer = true;
                StartCoroutine(onPlayer());
                goDown = true;
            }
        }
   
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * 2);
    }

    IEnumerator onPlayer()
    {
        yield return new WaitForSeconds(3f);
        OnPlayer= false;
    }
}
