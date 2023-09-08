using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MapSelectMove : MonoBehaviour
{
    Animator animator;

    private Vector3 direction;

    public LayerMask mapStage;
    public int playerStagePos;
    public GameObject[] Stages;
    public Vector3 pos;
    public int targetNumber;
    public bool Move = true;
    private bool backMove = true;
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
        if (Input.GetMouseButtonDown(0) && Clcm.Go)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity,mapStage))
            {
                for(int i = 0; i< Stages.Length; i++)
                {
                    if (Stages[i].name == hit.collider.gameObject.name)
                    {
                        targetNumber = i;                                 
                    }
                }
              
                if(targetNumber > playerStagePos)
                {
                    playerStagePos += 1;
                    pos = Stages[playerStagePos ].transform.position;
                    Move= true;
                }
                else if (targetNumber < playerStagePos)
                {
                    playerStagePos -= 1;
                    pos = Stages[playerStagePos].transform.position;
                    backMove = true;
                }
                stageNumber.instance.CurrentStageNum(targetNumber);
}
        }
        if (Move)
        {
            animator.SetBool("Walk", true);
            transform.position = Vector3.MoveTowards(transform.position, pos, 30f * Time.deltaTime);
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
            transform.position = Vector3.MoveTowards(transform.position, pos, 30f * Time.deltaTime);
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
    }
    private void playerbackMove()
    {
        if (playerStagePos > targetNumber)
        {
            playerStagePos -= 1;
            pos = Stages[playerStagePos].transform.position;
            backMove = true;
        }
    }
    public void EnterInGame()
    {
        if (playerStagePos == 0)
        {
            LoadingScene.LoadScene("Stage1");
        }
        if (playerStagePos == 1)
        {
            LoadingScene.LoadScene("Stage2");
        }
        if (playerStagePos == 2)
        {
            LoadingScene.LoadScene("Stage3");
        }
        if (playerStagePos == 3)
        {
            LoadingScene.LoadScene("Stage4");
        }
    }
}
