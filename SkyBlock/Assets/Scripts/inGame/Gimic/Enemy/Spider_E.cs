using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;


public class Spider_E : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [SerializeField] private float AttackNum;

    private bool attackON = true;
    public LayerMask blockEnd;
    public LayerMask playerMask;
    public bool backTurn;
    [Space]
    [Header("좌회전으로 이동시 체크")]
    public bool isRightTurn;
    private Vector3 nPosition;
    public bool Move = false;
    [SerializeField]
    private bool isWall = false;
    private int TurnStac;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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
            StartCoroutine(moveFalse());
            if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z), transform.forward, 2, playerMask))
            {
                Attack();
            }
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z), transform.forward * 2);
    }

    private void Attack()
    {
        PlayerController.timer = 2f;
        StartCoroutine(playerDie());
    }


    public void SpiderMove()
    {
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z), transform.forward, 2, playerMask))
        {
            Attack();
        }
        else if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z), transform.forward, 2, blockEnd))
        {
            isWall = true;
            if (isRightTurn)
            {
                transform.eulerAngles += new Vector3(0, 90, 0);
            }
            else
            {
                transform.eulerAngles += new Vector3(0, -90, 0);
            }


        }
        else
        {
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
        yield return new WaitForSeconds(0.6f);
        Move = false;
    }

    IEnumerator playerDie()
    {
        yield return new WaitForSeconds(1);
        player.GetComponent<Player>().Lose();
        yield return new WaitForSeconds(0.1f);
        player.gameObject.SetActive(false);
    }
}
