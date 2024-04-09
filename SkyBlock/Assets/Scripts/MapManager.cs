using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class MapManager : MonoBehaviour
{
    public static MapManager Inst;
    public int curChapterNum, curStageNum,preStageNum;
    public GameObject player;
    Transform target;

    [SerializeField] float playerSpeed;

    [SerializeField] GameObject[] chapter1Stages;

    public bool isMoving;

    public LayerMask mapStage;
    private void Awake()
    {
        Inst = this;


    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, mapStage))
            {
                preStageNum = curStageNum;
                for(int i = 1; i < chapter1Stages.Length; i++)
                {
                    if (hit.transform == chapter1Stages[i].transform)
                    {
                        curStageNum = i;
                    }
                }
                if(curStageNum > preStageNum)
                    target = chapter1Stages[preStageNum+1].transform;
                else if(curStageNum < preStageNum)
                    target = chapter1Stages[preStageNum - 1].transform;

                Vector3 direction = target.position - player.transform.position;
                player.transform.rotation = Quaternion.LookRotation(direction);

                isMoving = true;
              
            }
        }

        if (isMoving)
        {
            if (curStageNum > preStageNum)
                FrontMove();
            else if (curStageNum < preStageNum)
                BackMove();
            

        }



    }

    private void BackMove()
    {
        
            player.transform.position = Vector3.MoveTowards(player.transform.position, target.position, playerSpeed * Time.deltaTime);

            if (Mathf.Abs(Vector3.Distance(player.transform.position, target.position)) < 3f)
            {

                preStageNum--;
                if (preStageNum == curStageNum)
                    isMoving = false;
                else
                {

                    target = chapter1Stages[preStageNum-1].transform;
                    Vector3 direction = target.position - player.transform.position;
                    player.transform.rotation = Quaternion.LookRotation(direction);
                }

            }
        
    }

    private void FrontMove()
    {
        
            player.transform.position = Vector3.MoveTowards(player.transform.position, target.position, playerSpeed * Time.deltaTime);
            if (Mathf.Abs(Vector3.Distance(player.transform.position, target.position)) < 3f)
            {
                preStageNum++;
                if (preStageNum == curStageNum)
                    isMoving = false;
                else
                {
                    
                    target = chapter1Stages[preStageNum+1].transform;
                    Vector3 direction = target.position - player.transform.position;
                    player.transform.rotation = Quaternion.LookRotation(direction);
                }

            }
        
    }
}
