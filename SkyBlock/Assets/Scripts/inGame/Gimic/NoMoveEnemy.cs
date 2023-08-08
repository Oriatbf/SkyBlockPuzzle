using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoMoveEnemy : MonoBehaviour
{
    public LayerMask player;
    public GameObject playerObject;
    private int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
       
        if (PlayerSwipe.isMoving)
        {
            if (Physics.Raycast(transform.position, -transform.forward, 2f, player) && count ==0)
            {
                count++;
                playerObject.GetComponent<Player>().Lose();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * -2f);
    }
}
