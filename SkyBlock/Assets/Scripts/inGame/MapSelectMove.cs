using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MapSelectMove : MonoBehaviour
{
    Animator animator;

    private Vector3 direction;
    public float moveSpeed;
    public LayerMask mapStage;
    public int playerStagePos;
    public GameObject[] Stages;
    public Vector3 pos;
    public int targetNumber;
    public bool Move = true;
    private bool backMove = true;
    private bool isMoving; // 연속 클릭 막기
    public CloudeCam Clcm;
    // Update is called once per frame

    private void Start()
    {
        animator= GetComponent<Animator>();
        pos= transform.position;
        playerStagePos= 0;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Clcm.Go && !isMoving)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity,mapStage))
            {
                for(int i = 0; i< Stages.Length; i++)
                {
                    if (StageManager.instance.clearStage >= i && Stages[i] == hit.collider.gameObject)
                    {
                        targetNumber = i;                     
                    }
                }
              
                if(targetNumber > playerStagePos)
                {
                    playerStagePos += 1;
                    pos = Stages[playerStagePos].transform.position;
                    Move= true;
                    isMoving= true;
                }
                else if (targetNumber < playerStagePos)
                {
                    playerStagePos -= 1;
                    pos = Stages[playerStagePos].transform.position;
                    backMove = true;
                    isMoving = true;
                }
                stageNumber.instance.CurrentStageNum(targetNumber);
}
        }
        if (Move)
        {
            
            animator.SetBool("Walk", true);
            transform.position = Vector3.MoveTowards(transform.position, pos, moveSpeed * Time.deltaTime);
            direction = Stages[playerStagePos].transform.position - transform.position;
            transform.rotation = Quaternion.LookRotation(direction);
            if (Vector3.Distance(transform.position, pos) < 0.1f)
            {
                animator.SetBool("Walk", false);
                Move = false;
                playerMove();
            }
        }
        if (backMove)
        {
            animator.SetBool("Walk", true);
            transform.position = Vector3.MoveTowards(transform.position, pos, moveSpeed * Time.deltaTime);;
            direction = Stages[playerStagePos].transform.position - transform.position;
            transform.rotation = Quaternion.LookRotation(direction);
            if (Vector3.Distance(transform.position, pos) < 0.1f)
            {
                animator.SetBool("Walk", false);
                backMove = false;
                playerbackMove();
            }
        }
    }

    private void playerMove()
    {
        if (playerStagePos < targetNumber)
        {
            playerStagePos += 1;
            pos = Stages[playerStagePos].transform.position;
            Move = true;
        }
        else
        {
            isMoving = false;
        }
    }
    private void playerbackMove()
    {
        if (playerStagePos > targetNumber)
        {
            playerStagePos -= 1;
            pos = Stages[playerStagePos].transform.position;
            backMove = true;
        }
        else
        {
            isMoving = false;
        }
    }
    public void EnterInGame()
    {
        SceneManager.LoadScene(3 + playerStagePos);
    }
}
