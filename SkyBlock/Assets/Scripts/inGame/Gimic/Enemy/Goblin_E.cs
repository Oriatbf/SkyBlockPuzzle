using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_E : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [SerializeField] private float attackTurn;

    public bool attackON = false;
    public LayerMask blockEnd;
    public LayerMask platForm;
    public LayerMask playerMask;

    private Vector3 nextPosition;
    private Vector3 prePos;
    public bool isMove = false;
    [SerializeField]
    private bool isWall =false;
    private bool isUndo;


    Animator animator;

    void Start()
    {
        nextPosition= transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
        animator= GetComponent<Animator>(); 
    }
    void Update()
    {
       

        if (InGameManager.Inst.curTurn % attackTurn == 0 && !isWall)
            attackON = true;
        else
            attackON = false;

        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z), transform.forward, 2, blockEnd))
        {
            isWall = true;
        }
        else
        {
            isWall= false;
        }


       

        if (isMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, nextPosition, 3f * Time.deltaTime);
            animator.SetBool("isWalk",true);
            if (SoundEffectManager.SFX != null)
                SoundEffectManager.PlaySoundEffect(0);
            if (Mathf.Abs(Vector3.Distance(transform.position, nextPosition)) < 0.1f)
            {
                if (isUndo)
                {
                    isUndo = false;
                }
                else
                {
                    Save();
                }
                if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z), transform.forward, 2, playerMask) && attackON)
                {
                    Attack();

                }

                animator.SetBool("isWalk", false);
                isMove = false;
                transform.position= nextPosition;
            }      
        }      
    }

    private void Save()
    {
        UndoManager.Inst.SaveGoblinPos(prePos, transform.rotation,true);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z), transform.forward * 2);
        Gizmos.DrawRay(transform.position + transform.forward *2 + Vector3.up*0.8f, -transform.up * 1);
    }

    private void Attack()
    {
        animator.applyRootMotion = true;
        animator.SetTrigger("isAttack");
        InGameManager.Inst.playerLose();
    }
   

    public void GoblinMove()
    {
        prePos = nextPosition;
       
        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position + transform.forward * 2 + Vector3.up * 0.8f, -transform.up , out hitInfo,1f,platForm))
        {
            Debug.Log(hitInfo.collider);
        }
        else
        {
            isWall = true;
        }

        if (isWall)
        {
            Turn();
        }
        else
        {
            if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z), transform.forward, 2, playerMask) && attackON)
            {
                Attack();

            }
            else
            {
                nextPosition = transform.position + transform.TransformDirection(Vector3.forward) * 2f;
                isMove = true;
            }
                
        }
    }

    public void UndoPos(Vector3 pos,Quaternion rot)
    {
        nextPosition= pos;
        transform.rotation= rot;
        isMove = true;
        isUndo= true;
    }

    private void Turn()
    {
        if (transform.eulerAngles.y == 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        Save();
    }

    

   
}
