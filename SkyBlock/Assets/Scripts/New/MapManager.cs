using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    public static MapManager Inst;
    public int curChapterNum, curStageNum,preStageNum;
    public GameObject cp1Player,cp2Player,curPlayer;
    Transform target;

    [SerializeField] float playerSpeed;

    [SerializeField] GameObject[] chapter1Stages, chpater1Locks;

    public bool isMoving,isInChapter;

    public LayerMask mapStage;
    private void Awake()
    {
        Inst = this;
        curPlayer = cp1Player;
        cp2Player.SetActive(false);
        ChangeChapterPlayer();
    }

    private void Start()
    {
        for(int i = 1; i <= StageManager.Inst.clearStage; i++)
        {
            chpater1Locks[i].SetActive(false);
        }
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0) &&MainUIManager.Inst.mapPlayerMoving && isInChapter)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, mapStage))
            {
                Debug.Log("ss");
                preStageNum = curStageNum;
                for(int i = 1; i < chapter1Stages.Length; i++)
                {
                    if (hit.transform == chapter1Stages[i].transform)
                    {
                        if(i <= StageManager.Inst.clearStage+1)     //1스테이지를 클리어 했으니 clearStage : 1 + 1 헤서 2스테이지에 갈 수 있다
                        {
                            curStageNum = i;
                            if (curStageNum > preStageNum)
                                target = chapter1Stages[preStageNum + 1].transform;
                            else if (curStageNum < preStageNum)
                                target = chapter1Stages[preStageNum - 1].transform;

                            Vector3 direction = target.position - curPlayer.transform.position;
                            curPlayer.transform.rotation = Quaternion.LookRotation(direction);

                            isMoving = true;
                        }
                            
                    }
                }
               
              
            }
        }

        if (isMoving)
        {
            if (curStageNum > preStageNum)
                FrontMove();
            else if (curStageNum < preStageNum)
                BackMove();
            curPlayer.GetComponent<Animator>().SetBool("Walk", true);
            
        }
        else
        {
            curPlayer.GetComponent<Animator>().SetBool("Walk", false);
        }



    }

    private void BackMove()
    {
        
            curPlayer.transform.position = Vector3.MoveTowards(curPlayer.transform.position, target.position, playerSpeed * Time.deltaTime);

            if (Mathf.Abs(Vector3.Distance(curPlayer.transform.position, target.position)) < 3f)
            {

                preStageNum--;
                if (preStageNum == curStageNum)
                    isMoving = false;
                else
                {

                    target = chapter1Stages[preStageNum-1].transform;
                    Vector3 direction = target.position - curPlayer.transform.position;
                    curPlayer.transform.rotation = Quaternion.LookRotation(direction);
                }

            }
        
    }

    private void FrontMove()
    {
        
            curPlayer.transform.position = Vector3.MoveTowards(curPlayer.transform.position, target.position, playerSpeed * Time.deltaTime);
            if (Mathf.Abs(Vector3.Distance(curPlayer.transform.position, target.position)) < 3f)
            {
                preStageNum++;
                if (preStageNum == curStageNum)
                    isMoving = false;
                else
                {
                    
                    target = chapter1Stages[preStageNum+1].transform;
                    Vector3 direction = target.position - curPlayer.transform.position;
                    curPlayer.transform.rotation = Quaternion.LookRotation(direction);
                }

            }
        
    }
    
    public void ChangeChapterPlayer()
    {
        switch (curChapterNum)
        {
            case 1:
                cp1Player.SetActive(true);
                cp2Player.SetActive(false);
                curPlayer = cp1Player;
                curStageNum = 1;
                break;
            case 2:
                if(StageManager.Inst.clearStage >= 11)
                {
                    cp1Player.SetActive(false);
                    cp2Player.SetActive(true);
                    curPlayer = cp2Player;
                    curStageNum = 12;
                }
               
                break;
        }
    }
}
