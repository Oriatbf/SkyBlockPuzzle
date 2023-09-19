using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_E : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [SerializeField] private float AttackNum;

    private bool attackON = true;
    public LayerMask blockEnd;
    public LayerMask platForm;
    public LayerMask playerMask;
    public bool backTurn;
    [Space]
    [Header("가로로 이동시 체크")]
    public bool isWidth; //가로인가
    private Vector3 nPosition;
    public bool Move = false;
    [SerializeField]
    private bool isWall =false;

    Animator animator;
    private int TurnStac;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator= GetComponent<Animator>(); 
        TurnStac = player.GetComponent<Player>().TurnStac;
    }
    void Update()
    {


        if (TurnStac % AttackNum == 0 && TurnStac != 0 && attackON)
        {
            Attack();
        }
        
        if (TurnStac % AttackNum == AttackNum - 1 && TurnStac != 0 && !isWall)
        {
            attackON = true;
        }
      

        if (Move)
        {
            transform.position = Vector3.MoveTowards(transform.position, nPosition, 3f * Time.deltaTime);
            animator.SetBool("isWalk",true);
            StartCoroutine(moveFalse());
            if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z), transform.forward, 2, playerMask))
            {
                Attack();
            }
        }
        else
        {
            animator.SetBool("isWalk", false);
        }
           
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z), transform.forward * 2);
        Gizmos.DrawRay(transform.position + transform.forward *2 + Vector3.up*1.2f, -transform.up * 2);
    }

    private void Attack()
    {
        PlayerController.timer = 2f;
        animator.applyRootMotion = true;
        animator.SetTrigger("isAttack");
        StartCoroutine(playerDie());
    }
   

    public void GoblinMove()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z), transform.forward, out hitInfo, 2))
        {
            if (hitInfo.transform.CompareTag("Player"))
                Attack();
        }
        if (Physics.Raycast(new Vector3(transform.position.x,transform.position.y+0.3f,transform.position.z) , transform.forward, 2, blockEnd) 
            || !Physics.Raycast(transform.position + transform.forward * 2 + Vector3.up * 1.2f, -transform.up, 2, platForm))
        {
            isWall= true;
            if (!isWidth)
            {
                if (backTurn)
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    backTurn = false;
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 180, 0);
                    backTurn = true;
                }
            }

            if (isWidth)
            {

                if (backTurn)
                {
                    transform.eulerAngles = new Vector3(0, 90, 0);
                    backTurn = false;
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, -90, 0);
                    backTurn = true;
                }
            }

    
        }
        else if (hitInfo.transform == null || !hitInfo.transform.CompareTag("Player"))
        {
            if (SoundEffectManager.SFX != null)
                SoundEffectManager.PlaySoundEffect(0);

            PlayerController.timer = 1f;
            isWall = false;
            nPosition = transform.position + transform.TransformDirection(Vector3.forward) * 2f;
            StartCoroutine(WaitPlayerMove());
        }
       
        
    }

    IEnumerator WaitPlayerMove()
    {
        Move = false;
        yield return new WaitForSeconds(0.3f);
        Move = true;
    }

    IEnumerator moveFalse()
    {
        yield return new WaitForSeconds(0.7f);
        Move = false;
    }

    IEnumerator playerDie()
    {
        yield return new WaitForSeconds(1);
        player.GetComponent<Player>().Lose();
        yield return new WaitForSeconds(5f);
        player.gameObject.SetActive(false);
    }
}
