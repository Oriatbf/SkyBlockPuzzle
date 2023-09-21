using UnityEngine;

using System.Collections;
using System.Collections.Generic;

using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    public LayerMask platform;
    public LayerMask stopMoveBlock;
    public LayerMask PushBlocks;
    public LayerMask EnemyMask;
    public GameObject box;
    private int currentPlayerStage;

    private RaycastHit PushBlockhit;
    private RaycastHit Enemyhit;

    public Image FinishPNG;
    public Sprite FinishSprite;
    public Image starPNG;
    public Sprite StarSprite;
    public Text TurnImageText;

    public GameObject WeaponOBJ;
    public GameObject EndScreen;
    public GameObject EndText; //게임오버 혹은 클리어시 텍스트 변경
    public GameObject NextStageUI; //게임오버시 버튼 인터렉터블 OnOFF

    public float StarCount = 0;
    public static float MoveCoolTime;
    public static  float MoveCool;
    public int EndTurn; // 턴 수 제약 조건
    
    [SerializeField]
    private Animator animator;
    
    public Transform hitPoint;

    public Vector3 hitPointSize;
    private Vector3 myTrans;

    public static bool isGround;
    public bool dontMove;
    public static bool isPushBlock = false;
    public static bool isFrontEnemy = false;
    private bool isWin = false;
    private bool takeStar = false;
    private bool turnClear = false;
    [SerializeField]
    private bool isStair = false;
    public int TurnStac = 0;

    public PlayerController PlayerconSc;
    // Start is called before the first frame update

    void Start()
    {
        EndText.GetComponent<Text>().text = "1-" + (stageNumber.instance.stageNum+1).ToString() + "스테이지";
        currentPlayerStage = stageNumber.instance.stageNum;
        EndScreen.SetActive(false);
        dontMove= false;
    }
    private void FixedUpdate()
    {

        if (Physics.Raycast(transform.position + new Vector3(0,0.3f,0), Vector3.down, 3, platform))
        {
            isGround = true;
        }
        else
        {
            isGround = false;
            //Destroy(gameObject);
            //Lose();
        }

       
    }

    // Update is called once per frame
    void Update()
    {
      
        myTrans = transform.position;
        //isGround = false;

        if (Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), transform.forward, 2, stopMoveBlock) || Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), transform.forward, 2, platform))   //dontgo 레이어
        {
            dontMove= true;
        }
        else
        {
            dontMove = false;
        }
        if (MoveCoolTime > 0)
        {
            MoveCoolTime -= Time.deltaTime;
        }

        //rigidbody.useGravity = !horseRiding;
        if (isStair)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(5,1,4), Time.deltaTime * 2f);
        }
    }


  

    public void Lose()
    {
        if (SoundEffectManager.SFX != null)
            SoundEffectManager.PlaySoundEffect(4);

        if (ParticleManager.Particles != null)
            Instantiate(ParticleManager.Particles[4], transform.position + Vector3.up * 0.5f, ParticleManager.Particles[4].transform.rotation);

        PlayerController.DetectOff = true;
        animator.SetTrigger("Die");
        isWin = false;
        //Instantiate(box,transform.position,box.transform.rotation);
        EndText.GetComponent<Text>().text = "GameOver";
        NextStageUI.GetComponent<Button>().interactable = false;
        StartCoroutine(EndScreenOn());
    }

    IEnumerator EndScreenOn()
    {
        yield return new WaitForSeconds(2);
        EndScreen.SetActive(true);
    } 

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Goal")
        {
            isWin= true;
            PlayerController.DetectOff = true;
            StartCoroutine(GoalDelay());
            if (SoundEffectManager.SFX != null)
                SoundEffectManager.PlaySoundEffect(8);

            FinishPNG.sprite = FinishSprite;
            if (StarCount == 1)
            {
                takeStar = true;
                starPNG.sprite = StarSprite;
            }
            if (StarCount == 1 && TurnStac < EndTurn)
            {
                turnClear= true;
                TurnImageText.color = new Color(255 / 255.0f, 192 / 255.0f, 25 / 255.0f, 1.0f);
            }

            StageManager.instance.StageClear(currentPlayerStage, true, takeStar, turnClear);
            StageManager.instance.StageClear(currentPlayerStage+1, false, false, false);
            StarCount = 0;
        }
      
      

        if (col.CompareTag("stairDown"))
        {
            isStair= true;
        }
        
    }
   

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(hitPoint.position, hitPointSize);
        Gizmos.DrawRay(transform.position + new Vector3(0, 0.5f, 0), transform.forward*2);
        Gizmos.DrawRay(transform.position + new Vector3(0, 0.3f, 0), Vector3.down * 3);
    }


    void MoveCooltime()
    {
        MoveCoolTime = MoveCool;
    }

    public void Attack()
    {
        Collider[] colliders = Physics.OverlapBox(hitPoint.position, hitPointSize);
        foreach(Collider collider in colliders)
        {
            if (collider.CompareTag("Goblin") || collider.CompareTag("Spider") || collider.CompareTag("Destroy"))
            {
                StartCoroutine(AttackDelay(collider));
                animator.SetTrigger("Attack");
            }
              
        }
      
    }

    IEnumerator AttackDelay(Collider collider)
    {
        yield return new WaitForSeconds(0.3f);

        if (SoundEffectManager.SFX != null)
            SoundEffectManager.PlaySoundEffect(4);

        if (ParticleManager.Particles != null)
            Instantiate(ParticleManager.Particles[2], collider.transform.position + Vector3.up * 0.5f, ParticleManager.Particles[2].transform.rotation);

        collider.gameObject.SetActive(false);
        PlayerconSc.GoblinDetect();
        PlayerconSc.Detect();
    }

    IEnumerator GoalDelay()
    {
        yield return new WaitForSeconds(0.2f);
        if (ParticleManager.Particles != null)
            Instantiate(ParticleManager.Particles[3], transform.position + new Vector3(0, -1.2f, 0.5f), ParticleManager.Particles[3].transform.rotation);
        yield return new WaitForSeconds(1f);
        if (SoundEffectManager.SFX != null)
            SoundEffectManager.PlaySoundEffect(9);
        yield return new WaitForSeconds(0.5f);
        EndScreen.SetActive(true);
    }
}
