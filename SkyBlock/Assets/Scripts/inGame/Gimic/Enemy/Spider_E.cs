using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Spider_E : MonoBehaviour
{
    public LayerMask obstacleMask,playerMask;
    Vector3 nextPos;
    bool isMove;
    bool emptyBlock;
    InGamePlayerMove playerMove;

    Vector3 upPos = new Vector3(0, 0.3f, 0);

    private void Start()
    {
        nextPos= transform.position;
        playerMove = InGamePlayerMove.Inst;
    }

    private void Update()
    {
        if (isMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, nextPos, 3f * Time.deltaTime);

            if (Mathf.Abs(Vector3.Distance(transform.position, nextPos)) < 0.1f)
            {
                if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z), transform.forward, 2, playerMask))
                {
                    Debug.Log("맞음");
                    InGameManager.Inst.playerLose();

                }
                else
                {

                    playerMove.ActiveThings();
                }

               



                isMove = false;
                transform.position = nextPos;
            }
        }

       
    }

    public void Move()
    {
       if(Physics.Raycast(transform.position + upPos, transform.forward, 1.7f, obstacleMask) || FindEmptyBlock(transform.forward) == true)
        {
            if(Physics.Raycast(transform.position + upPos, transform.right, 1.7f, obstacleMask) || FindEmptyBlock(transform.right) == true)
            {
                if (Physics.Raycast(transform.position + upPos, -transform.right, 1.7f, obstacleMask) || FindEmptyBlock(-transform.right) == true)
                {
                    Debug.Log("뒤로이동");
                    nextPos = transform.position + transform.TransformDirection(Vector3.back) * 2f;
                    Debug.Log(nextPos);
                    transform.LookAt(nextPos);

                }
                else
                {
                    Debug.Log("왼로이동");
                    nextPos = transform.position + transform.TransformDirection(Vector3.left) * 2f;
                    transform.LookAt(nextPos);
                }
            }
            else
            {
                Debug.Log("오른쪽로이동");
                nextPos = transform.position + transform.TransformDirection(Vector3.right) * 2f;
                transform.LookAt(nextPos);
            }
        }
        else
        {
            nextPos = transform.position + transform.TransformDirection(Vector3.forward) * 2f;
            
        }

        isMove = true;
    }

    bool FindEmptyBlock(Vector3 dir)
    {
        if(!Physics.Raycast(transform.position + dir * 2 + Vector3.up * 0.8f,Vector3.down, 1.5f))
        {
            return true;
        }
        else
        {
            return false;
        }     
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + upPos, transform.forward*1.5f);
        Gizmos.DrawRay(transform.position + upPos, transform.forward * -1.5f);
        Gizmos.DrawRay(transform.position + upPos, transform.right * 1.5f);
        Gizmos.DrawRay(transform.position + upPos, transform.right * -1.5f);
        Gizmos.DrawRay(transform.position + transform.forward * 2 + Vector3.up * 0.8f, Vector3.down * 1.5f);
    }
}
