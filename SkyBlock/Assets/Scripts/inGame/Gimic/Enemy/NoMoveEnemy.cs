using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoMoveEnemy : MonoBehaviour
{
    public LayerMask player;
    public LayerMask Wall;
    public GameObject playerObject;
    private int count = 0;
    [SerializeField]
    private bool isWall;
    [SerializeField]
    Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), transform.forward, 2f, Wall))
        {
            isWall= true;
        }
        else
        {
            isWall= false;
        }
      
        if (Physics.Raycast(new Vector3(transform.position.x,transform.position.y+0.5f,transform.position.z), transform.forward, 2f, player) && count ==0 && !isWall)
        {
            count++;
            animator.SetTrigger("Attack");
            InGameManager.Inst.gameEnd = true;
            
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), transform.forward * 2f);
    }

    public void AttackAnim()
    {
        InGameManager.Inst.playerLose();
    }
}
