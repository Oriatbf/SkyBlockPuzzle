using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UIElements;

public class PlayerSwipe : MonoBehaviour
{
    public GameObject[] Goblin;
    [SerializeField]
    private float x1;
    [SerializeField]
    private float x2;
    [SerializeField]
    private float y1;
    [SerializeField]
    private float y2;

    public static bool leftMoving;
    public static bool rightMoving;
    public static bool fowardMoving;
    public static bool backMoving;
    public static bool isActive;
    public static bool isChangeButton;
    public static bool isMoving; //플레이어가 움직이는 중 일때

    public static bool yesLMove = false;
    public static bool yesRMove = false;
    public static bool yesFMove = false;
    public static bool yesBMove = false;

    public GameObject HorseOBJ;
    Animator animator;

    private RaycastHit PushBlockhit;
    private RaycastHit Enemyhit;
    public LayerMask PushBlocks;
    public LayerMask EnemyMask;


    private void Start()
    {
        animator = GetComponent<Animator>();
        Goblin = GameObject.FindGameObjectsWithTag("Goblin");
        leftMoving = false;
        rightMoving = false;
        fowardMoving = false;
        backMoving = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            x1 = Input.mousePosition.x;
            y1 = Input.mousePosition.y;
        }
        if (leftMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x - 2f, transform.position.y, transform.position.z), 2f*Time.deltaTime);
      
        }
        if (rightMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x +2f, transform.position.y, transform.position.z), 2f * Time.deltaTime);
        
        }
        if (fowardMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x , transform.position.y, transform.position.z +2), 2f * Time.deltaTime);
           
        }
        if (backMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x , transform.position.y, transform.position.z - 2f), 2f * Time.deltaTime);
          
        }
        if (Input.GetMouseButtonUp(0)&&!isChangeButton)
        {
            x2 = Input.mousePosition.x;
            y2 = Input.mousePosition.y;

            if (x1 > x2)
            {
                if (Mathf.Abs(x1 - x2) > Mathf.Abs(y2 - y1))
                {
                    Debug.Log("Left");
                    if (Player.isGround && Player.MoveCoolTime <= 0 && yesLMove)
                    {
                        if (Player.horseRiding)
                        {
                            transform.eulerAngles = new Vector3(0, -90, 0);
                            HorseOBJ.transform.eulerAngles = transform.eulerAngles;
                            transform.position += transform.forward * 2.006f * 2;
                            HorseOBJ.transform.position = new Vector3(transform.position.x, HorseOBJ.transform.position.y, transform.position.z);
                        }
                        else
                        {
                            transform.eulerAngles = new Vector3(0, -90, 0);
                            if (Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), transform.forward, out Enemyhit, 2, EnemyMask))
                            {
                                animator.SetTrigger("Attack");
                                                     
                            }
                            else if (Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), transform.forward, out PushBlockhit, 2, PushBlocks))
                            {
                                PushBlockhit.transform.GetComponent<PushBlock>().blockMove();
                               
                            }
                            else
                            {
                                StartCoroutine(leftMove());
                                leftMoving = true;
                            }
                        }





                        MoveCooltime();
                        Player.TurnStac += 1;
                    }
                }

            }

            if (x2 > x1)
            {
                if (Mathf.Abs(x1 - x2) > Mathf.Abs(y2 - y1))
                {
                   
                    Debug.Log("Right");
                    if (Player.isGround && Player.MoveCoolTime <= 0&&yesRMove)
                    {
                        if (Player.horseRiding)
                        {
                            transform.eulerAngles = new Vector3(0, 90, 0);
                            HorseOBJ.transform.eulerAngles = transform.eulerAngles;
                            transform.position += transform.forward * 2.006f * 2;
                            HorseOBJ.transform.position = new Vector3(transform.position.x, HorseOBJ.transform.position.y, transform.position.z);
                        }
                        else
                        {
                            transform.eulerAngles = new Vector3(0, 90, 0);
                            if (Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), transform.forward, out Enemyhit, 2, EnemyMask))
                            {

                                animator.SetTrigger("Attack");
                               

                            }
                            else if (Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), transform.forward, out PushBlockhit, 2, PushBlocks))
                            {
                                PushBlockhit.transform.GetComponent<PushBlock>().blockMove();
                                
                            }
                            else
                            {
                                StartCoroutine(rightMove());
                                rightMoving = true;
                            }
                        }

                        MoveCooltime();
                        Player.TurnStac += 1;
                       
                    }
                }

            }

            if (y1 > y2)
            {
                if (Mathf.Abs(x1 - x2) < Mathf.Abs(y2 - y1))
                {
                    Debug.Log("back");
                    if (Player.isGround && Player.MoveCoolTime <= 0 &&yesBMove)
                    {

                        if (Player.horseRiding)
                        {
                            transform.eulerAngles = new Vector3(0, 180, 0);
                            HorseOBJ.transform.eulerAngles = transform.eulerAngles;
                            transform.position += transform.forward * 2.006f * 2;
                            HorseOBJ.transform.position = new Vector3(transform.position.x, HorseOBJ.transform.position.y, transform.position.z);
                        }
                        else
                        {
                            transform.eulerAngles = new Vector3(0, 180, 0);
                            if (Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), transform.forward, out Enemyhit, 2, EnemyMask))
                            {

                                animator.SetTrigger("Attack");
                                

                            }
                            else if (Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), transform.forward, out PushBlockhit, 2, PushBlocks))
                            {
                                PushBlockhit.transform.GetComponent<PushBlock>().blockMove();
                                
                            }
                            else
                            {
                                StartCoroutine(backMove());
                                backMoving = true;
                            }
                        }
                        MoveCooltime();
                        Player.TurnStac += 1;
                    }
                }

            }
            if (y2 > y1)
            {
                if (Mathf.Abs(x1 - x2) < Mathf.Abs(y2 - y1))
                {
                    Debug.Log("front");
                    if (Player.isGround && Player.MoveCoolTime <= 0 && yesFMove)
                    {
                        if (Player.horseRiding)
                        {
                            transform.eulerAngles = new Vector3(0, 0, 0);
                            HorseOBJ.transform.eulerAngles = transform.eulerAngles;
                            transform.position += transform.forward * 2.006f * 2;
                            HorseOBJ.transform.position = new Vector3(transform.position.x, HorseOBJ.transform.position.y, transform.position.z);
                        }
                        else
                        {
                            transform.eulerAngles = new Vector3(0, 0, 0);
                            if (Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), transform.forward, out Enemyhit, 2, EnemyMask))
                            {
                                animator.SetTrigger("Attack");
                            
                            }
                            else if (Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), transform.forward, out PushBlockhit, 2, PushBlocks))
                            {
                                PushBlockhit.transform.GetComponent<PushBlock>().blockMove();
                               
                            }
                            else
                            {
                                StartCoroutine(fowardMove());
                                fowardMoving = true;
                            }
                        }
                        MoveCooltime();
                        Player.TurnStac += 1;
                    }
                }

            }
        }
    }

    void MoveCooltime()
    {
        Player.MoveCoolTime = Player.MoveCool;
    }

   IEnumerator leftMove()
    {
        // Goblin[0].GetComponent<Goblin_E>().GoblinMove();
        isMoving = true;
        animator.SetBool("Walk", true);
        transform.eulerAngles = new Vector3(0, -90, 0);
        yield return new WaitForSeconds(1f);
        leftMoving = false;
        isMoving = false;
        animator.SetBool("Walk", false);
    }
    IEnumerator rightMove()
    {
        //Goblin[0].GetComponent<Goblin_E>().GoblinMove();
        isMoving = true;
        animator.SetBool("Walk", true);
        transform.eulerAngles = new Vector3(0, 90, 0);
        yield return new WaitForSeconds(1f);
        rightMoving = false;
        isMoving = false;
        animator.SetBool("Walk", false);
    }
    IEnumerator fowardMove()
    {
        // Goblin[0].GetComponent<Goblin_E>().GoblinMove();
        isMoving = true;
        animator.SetBool("Walk", true);
        transform.eulerAngles = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(1f);
        fowardMoving = false;
        isMoving = false;
        animator.SetBool("Walk", false);
    }
    IEnumerator backMove()
    {
        //Goblin[0].GetComponent<Goblin_E>().GoblinMove();
        isMoving = true;
        animator.SetBool("Walk", true);
        transform.eulerAngles = new Vector3(0, 180, 0);
        yield return new WaitForSeconds(1f);
        backMoving = false;
        isMoving = false;
        animator.SetBool("Walk", false);

    }
}
