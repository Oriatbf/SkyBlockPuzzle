using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Goblin_E : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [SerializeField] private float attackTurn;
    [Tooltip("고블린 처음 y축 회전이 90 혹은 -90일때 켜주세요")]
    public bool isWidth;

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

    private void Awake()
    {
        if (transform.eulerAngles.y == 90 || transform.eulerAngles.y == 270)
            isWidth = true;
        else
            isWidth = false;
    }

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
            InGameUIManager.Inst.changeClickAllow = false;
            transform.position = Vector3.MoveTowards(transform.position, nextPosition, 3f * Time.deltaTime);
            animator.SetBool("isWalk",true);
            if (SoundEffectManager.SFX != null)
                SoundEffectManager.PlaySoundEffect(0);
            if (Mathf.Abs(Vector3.Distance(transform.position, nextPosition)) < 0.1f)
            {
                if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z), transform.forward, 2, playerMask) && attackON && !InGameManager.Inst.gameEnd)
                {
                    Attack();

                }
                else if (isUndo)
                {
                    isUndo = false;
                    InGamePlayerMove.Inst.ActiveThings();
                }
                else
                {
                    Save();
                    InGamePlayerMove.Inst.ActiveThings();
                }

                InGameUIManager.Inst.changeClickAllow = true;
                animator.SetBool("isWalk", false);
                isMove = false;
                transform.position= nextPosition;
            }      
        }      
    }

    private void Save()
    {
       // UndoManager.Inst.SaveGoblinPos(transform.gameObject,prePos, transform.rotation,true);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z), transform.forward * 2);
        Gizmos.DrawRay(transform.position + transform.forward *2 + Vector3.up*0.8f, -transform.up * 1);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 a = player.transform.position - transform.position;
        Gizmos.DrawRay(transform.position+new Vector3(0,0.2f,0),a*5);
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
        Save();
        if (isWidth)
        {
            if(transform.eulerAngles.y == 90)
                transform.eulerAngles = new Vector3(0, 270, 0);
            else
                transform.eulerAngles = new Vector3(0, 90, 0);
        }
        else
        {
            if (transform.eulerAngles.y == 0)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }

        InGamePlayerMove.Inst.ActiveThings();

    }

  

    private void OnCollisionEnter(Collision collision)
    {
      
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PushBlock"))
        {
            if (ParticleManager.Particles != null)
                Instantiate(ParticleManager.Particles[2], transform.position + Vector3.up * 0.5f, ParticleManager.Particles[2].transform.rotation);
            gameObject.SetActive(false);
            
        }
    }





}
