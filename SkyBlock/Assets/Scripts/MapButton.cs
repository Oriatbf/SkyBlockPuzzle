using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class MapButton : MonoBehaviour
{
    [SerializeField]
    public Transform[] buttonPrefab;
    [SerializeField]
    public Vector3[] CamareTransformMove;
    [SerializeField]
    public Vector3[] CamareTransformTrans;
    public GameObject StageGoButton;
    public GameObject player;
    public GameObject MAPButton;
    public GameObject StageStarCount;
    public GameObject MapChapterCount;
    private MapSelectPlayerMove Playermove;
    private Camera mCamera;
    public int playerPosition;
    private Animator walkAinm;
    public int selNum;
    private Vector3 direction;

    public Transform CamaraTran;
    public CloudeCam Clcam;

    Vector3 firstPos, gap;
    bool SlideWait;
    public int slideNum;

    private Vector3 targetPosition;

    public int MAPNum = 1;
    public Text MapChapterCountNum;
    public int UILeftRightNum = 0;

    void Awake()
    {
        walkAinm = GetComponent<Animator>();
    }
    private void Start()
    {
        playerPosition = 0;
        mCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        Playermove = GameObject.FindGameObjectWithTag("Player").GetComponent<MapSelectPlayerMove>();

        MAPNum = 2; //TEST
        if (MAPNum > 1)
            MapChapterCountNum.text = MAPNum - 1 + " Chapter"; 
        else if (MAPNum == 1)
            MapChapterCountNum.text = "Main";

    }
    void Update()
    {
        if(selNum == playerPosition)
            walkAinm.SetBool("Walk", false);

        if (selNum == playerPosition)
        {
            targetPosition = new Vector3(CamaraTran.transform.position.x, transform.position.y, CamaraTran.transform.position.z);
            transform.LookAt(targetPosition);

        }

        // 슬라이드
        if (Input.GetMouseButtonDown(0) || (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            SlideWait = true;
            firstPos = Input.GetMouseButtonDown(0) ? Input.mousePosition : (Vector3)Input.GetTouch(0).position;
        }

        if (Input.GetMouseButton(0) || (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved))
        {
            gap = (Input.GetMouseButton(0) ? Input.mousePosition : (Vector3)Input.GetTouch(0).position) - firstPos;
            if (gap.magnitude < 100) return;
            gap.Normalize();

            if (SlideWait)
            {
                SlideWait = false;
                // �� (�Ⱦ��̴°�)
                if (gap.y > 0 && gap.x > -0.5f && gap.x < 0.5f)
                {
                    slideNum = 1;
                }
                // �Ʒ� (�Ⱦ��̴°�)
                else if (gap.y < 0 && gap.x > -0.5f && gap.x < 0.5f)
                {
                    slideNum = 2;
                }
                // 왼
                else if (gap.x > 0 && gap.y > -0.5f && gap.y < 0.5f)
                {
                    slideNum = 4;
                }
                // 오
                else if (gap.x < 0 && gap.y > -0.5f && gap.y < 0.5f)
                {
                    slideNum = 3;
                }
                else return;
            }
        }
        else
        {
            if (slideNum != 0)
            {
                slideNum = 0;
            }
        }

        //스테이지 변경
        if(slideNum == 3 && selNum == playerPosition && Clcam.Go == true
            || UILeftRightNum == 2 && selNum == playerPosition && Clcam.Go == true)
        {
            UILeftRightNum = 0;
            selNum += 1;
            slideNum = 0;
            CameraSet();
        }
        if (slideNum == 4 && selNum != 0 && selNum == playerPosition && Clcam.Go == true
            || UILeftRightNum == 1 && selNum != 0 && selNum == playerPosition && Clcam.Go == true)
        {
            UILeftRightNum = 0;
            selNum -= 1;
            slideNum = 0;
            CameraSet();
        }

        //맵 변경
        if (slideNum == 3 && MAPNum != 4 && Clcam.Go == false
            || UILeftRightNum == 2 && MAPNum != 4 && Clcam.Go == false)
        {
            UILeftRightNum = 0;
            MAPNum += 1;
            slideNum = 0;
            Clcam.BackButtonClick();

            //타이틀 수정
            if (MAPNum > 1)
                MapChapterCountNum.text = MAPNum - 1 + " Chapter";
            else if (MAPNum == 1)
                MapChapterCountNum.text = "Main";
        }
        if (slideNum == 4 && MAPNum != 1 && Clcam.Go == false
            || UILeftRightNum == 1 && MAPNum != 1 && Clcam.Go == false)
        {
            UILeftRightNum = 0;
            MAPNum -= 1;
            slideNum = 0;
            Clcam.BackButtonClick();

            //타이틀 수정
            if (MAPNum > 1)
                MapChapterCountNum.text = MAPNum - 1 + " Chapter";
            else if (MAPNum == 1)
                MapChapterCountNum.text = "Main";
        }
        /*if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = mCamera.ScreenPointToRay(Input.mousePosition);
            Debug.Log(ray);
            Physics.Raycast(ray, out hit);
            for (int i = 0; i < buttonPrefab.Length; i++)
            {
                if (hit.transform == buttonPrefab[i])
                {
                    selNum = i;
                }
            }
        }

        else
        {
            walkAinm.SetBool("Walk", false);
        }*/
        if (playerPosition != selNum)
        {
            StageGoButton.SetActive(true);
            if (playerPosition < selNum)
            {
                Playermove.Settarget(buttonPrefab[playerPosition + 1]);
                direction = buttonPrefab[playerPosition + 1].transform.position - player.transform.position;
                if (Vector3.Distance(transform.position, buttonPrefab[playerPosition + 1].position) < 0.1f)
                {
                    playerPosition++;
                }
            }
            else
            {
                Playermove.Settarget(buttonPrefab[playerPosition - 1]);
                direction = buttonPrefab[playerPosition - 1].transform.position - player.transform.position;
                if (Vector3.Distance(transform.position, buttonPrefab[playerPosition - 1].position) < 0.1f)
                {
                    playerPosition--;
                }
            }
            player.transform.rotation = Quaternion.LookRotation(direction);
            walkAinm.SetBool("Walk", true);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }

        if(UILeftRightNum != 0)
            UILeftRightNum = 0;
    }

    public void EnterInGame()
    {
        if(selNum == 0)
        {
            LoadingScene.LoadScene("Stage1");
        }
        if (selNum == 1)
        {
            LoadingScene.LoadScene("Stage2");
        }
        if (selNum == 2)
        {
            LoadingScene.LoadScene("Stage3");
        }
        if (selNum == 3)
        {
            LoadingScene.LoadScene("Stage4");
        }
    }

    public void CameraSet()
    {
        CamaraTran.DOMove(new Vector3(CamareTransformMove[selNum].x, CamareTransformMove[selNum].y, CamareTransformMove[selNum].z), 2.5f);
        CamaraTran.DORotate(new Vector3(CamareTransformTrans[selNum].x, CamareTransformTrans[selNum].y, CamareTransformTrans[selNum].z), 2.5f);
    }

    public void UILeft()
    {
        if(UILeftRightNum == 0)
            UILeftRightNum = 1;
    }
    public void UIRight()
    {
        if (UILeftRightNum == 0)
            UILeftRightNum = 2;
    }

    public void map1()
    {
        MAPNum = 1;
        if (MAPNum > 1)
            MapChapterCountNum.text = MAPNum - 1 + " Chapter";
        
        MapChapterCountNum.text = "Main";
    }
    public void map2()
    {
        MAPNum = 2;
        MapChapterCountNum.text = MAPNum - 1 + " Chapter";
    }
    public void map3()
    {
        MAPNum = 3;
        MapChapterCountNum.text = MAPNum - 1 + " Chapter";
    }

}

//1.543608 1.440643 1.097633
