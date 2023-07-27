using UnityEngine;

using System.Collections;
using System.Collections.Generic;

using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static UnityEditor.PlayerSettings;


public class Player : MonoBehaviour
{
    public LayerMask platform;
    public LayerMask stopMoveBlock;
    public LayerMask PushBlocks;
    public LayerMask EnemyMask;

    

    private RaycastHit PushBlockhit;
    private RaycastHit Enemyhit;

    public Image FinishPNG;
    public Sprite FinishSprite;
    public Image starPNG;
    public Sprite StarSprite;
    public Image TurnPNG;
    public Sprite TurnSprite;

    public GameObject WeaponOBJ;
    public GameObject HorseOBJ;
    public GameObject EndScreen;

    public float StarCount = 0;
    public float stageTurn;
    public static float MoveCoolTime;
    public static  float MoveCool;
    public float EndTurn; // 턴 수 제약 조건
    
    public Horse horse;

    Animator animator;
    Rigidbody rigidbody;
    
    public Transform hitPoint;

    public Vector3 hitPointSize;
    private Vector3 myTrans;

    public static bool WeaponOn = false;
    public static bool isGround;
    public bool dontMove;
    public static bool horseRiding = false;
    public static bool isPushBlock = false;
    public static bool isFrontEnemy = false;
    private bool isWin = false;

    public static bool horseNoGo = false;
    public static float TurnStac = 0;
    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().name == "TestScene2")
        {
            EndTurn = 14;
        }
        if (SceneManager.GetActiveScene().name == "TestScene")
        {
            EndTurn = 27;
        }
        EndScreen.SetActive(false);
        animator= GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        dontMove= false;
        WeaponOn= false;
        horseRiding= false;
        horseNoGo= false;
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
        if (transform.position.y <= 1f)
        {
            //Destroy(gameObject);
        }
        rigidbody.useGravity = !horseRiding;      
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Enemy"))
        {
            //Destroy(gameObject);
        }
    }

    private void Lose()
    {
        isWin = false;
        EndScreen.SetActive(true);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Goal")
        {
            isWin= true;
            Debug.Log("승리");
            EndScreen.SetActive(true);
            FinishPNG.sprite = FinishSprite;
            if (StarCount == 1)
            {
                starPNG.sprite = StarSprite;
            }
            if (StarCount == 1 && TurnStac <= EndTurn)
            {
                TurnPNG.sprite = TurnSprite;
            }
            //TurnStac= 0;
            StarCount = 0;
        }
        if (col.tag=="Weapon")
        {

            WeaponOBJ.GetComponent<Weapons>().Active();
            WeaponOn = true;

        }
        if(col.tag == "horse")
        { 
            horseRiding= true;    
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "horse")
        {
  
            horseRiding = false;

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
           
            if (collider.CompareTag("Enemy"))
            {
                Destroy(collider.gameObject);
            }
        }
      
    }
}
