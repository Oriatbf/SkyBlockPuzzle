using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_E : MonoBehaviour
{
    [SerializeField] private GameObject colli;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject Red;

    [SerializeField] private float AttackNum;

    private bool attackON = true;
    public Transform hitPoint;
    public Vector3 hitPointSize;
    public LayerMask blockEnd;
    public Player PlayerSpt;
    public bool backTurn;
    [Space]
    [Header("가로로 이동시 체크")]
    public bool isWidth; //가로인가
    private Vector3 nPosition;
    private bool Move = false;

    Animator animator;
   

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator= GetComponent<Animator>(); 
    }
    void Update()
    {
        if(Player.TurnStac % AttackNum == 0 && Player.TurnStac != 0 && attackON)
        {
            Attack();
        }

        if (Player.TurnStac % AttackNum == AttackNum - 1 && Player.TurnStac != 0)
        {
            attackON = true;
            Red.gameObject.SetActive(true);
        }

        if (Move)
        {
            transform.position = Vector3.MoveTowards(transform.position, nPosition, 3f * Time.deltaTime);
            animator.SetBool("isWalk",true);
        }
           
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z), transform.forward * 2);
    }

    private void Attack()
    {
        Red.gameObject.SetActive(false);
        attackON = false;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && Player.TurnStac % AttackNum == 0 && Player.TurnStac != 0)
        {
            
            PlayerSpt.Lose();
        }
    }

    public void GoblinMove()
    {
        if (Physics.Raycast(new Vector3(transform.position.x,transform.position.y+0.3f,transform.position.z) , transform.forward, 2, blockEnd))
        {
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
        else
        {
            nPosition = transform.position + transform.TransformDirection(Vector3.forward) * 2f;
            Move = true;
           
        }
       
        
    }
}
