using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDirection : MonoBehaviour
{
    public LayerMask blockEnd;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 1f, player.transform.position.z) ;
        if (Physics.Raycast(transform.position, transform.forward, 1.2f, blockEnd))
        {
            PlayerSwipe.yesFMove = false;
        }
        else
        {
            PlayerSwipe.yesFMove = true;
        }
        if (Physics.Raycast(transform.position, -transform.forward, 1.2f, blockEnd))
        {
            PlayerSwipe.yesBMove = false;
        }
        else
        {
            PlayerSwipe.yesBMove = true;
        }
        if (Physics.Raycast(transform.position, transform.right, 1.2f, blockEnd))
        {
            PlayerSwipe.yesRMove = false;
       
        }
        else
        {
            PlayerSwipe.yesRMove = true;
          
        }
        if (Physics.Raycast(transform.position, -transform.right, 1.2f, blockEnd))
        {
            PlayerSwipe.yesLMove = false;
        }
        else
        {
            PlayerSwipe.yesLMove = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * 1);
        Gizmos.DrawRay(transform.position, transform.forward * -1);
        Gizmos.DrawRay(transform.position, transform.right * 1);
        Gizmos.DrawRay(transform.position, transform.right * -1);
    }
}
