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
    private bool backTurn = false;
   

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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
      
           
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position , transform.forward * 2);
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
            Destroy(other.gameObject);
            PlayerSpt.Lose();
        }
    }

    public void GoblinMove()
    {
        if (Physics.Raycast(transform.position , transform.forward, 2, blockEnd))
        {
            if (backTurn)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                backTurn= false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                backTurn = true;
            }
           
 
    
        }
        else
        {
            transform.position += transform.forward * 2.006f ;
        }
       
        
    }
}
