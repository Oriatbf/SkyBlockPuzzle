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

    public float slopeForce = 5;
    public float slopeRayLength = 1.5f; //나중에 지울거
    public float slopeYLen = 0.2f; //;
    public float slopeMitLen; //;
    public float slopeRotate;

    public GameObject HorseOBJ;
    Animator animator;
    Rigidbody rig;

    private RaycastHit PushBlockhit;
    private RaycastHit Enemyhit;
    public LayerMask PushBlocks;
    public LayerMask EnemyMask;
    public LayerMask MoveTiles;

    public Vector3 nPosition;


    private void Start()
    {
        animator = GetComponent<Animator>();
        rig = GetComponent<Rigidbody>();
        Goblin = GameObject.FindGameObjectsWithTag("Goblin");
        rig.freezeRotation = true;

        leftMoving = false;
        rightMoving = false;
        fowardMoving = false;
        backMoving = false;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, MoveTiles))
            {
                if (hit.transform.CompareTag("frontGo"))
                {
                    if (Player.isGround && Player.MoveCoolTime <= 0 && yesLMove)
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

                        StartCoroutine(fowardMove());
                    }
                    if (hit.transform.CompareTag("leftGo"))
                    {
                        StartCoroutine(leftMove());
                    }
                    if (hit.transform.CompareTag("rightGo"))
                    {
                        StartCoroutine(rightMove());
                    }
                    if (hit.transform.CompareTag("backGo"))
                    {
                        StartCoroutine(backMove());
                    }
                }
            }
        }
            if (Input.GetMouseButtonDown(0))
            {
                x1 = Input.mousePosition.x;
                y1 = Input.mousePosition.y;
            }

            if (Input.GetMouseButtonUp(0) && !isChangeButton)
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
                        if (Player.isGround && Player.MoveCoolTime <= 0 && yesRMove)
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
                        if (Player.isGround && Player.MoveCoolTime <= 0 && yesBMove)
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
            if (isMoving)
            {
                transform.position = Vector3.MoveTowards(transform.position, nPosition, 3f * Time.deltaTime);
            }
            Debug.DrawRay(transform.position + new Vector3(0, 0.2f, 0), transform.forward * 1.5f, Color.blue);
            Debug.DrawRay(transform.position + new Vector3(0, slopeYLen, 0) + transform.forward * slopeMitLen, transform.forward * slopeRayLength, Color.blue);
        }

        void MoveCooltime()
        {
            Player.MoveCoolTime = Player.MoveCool;
        }

        IEnumerator leftMove()
        {
            MoveGoblin();
            transform.eulerAngles = new Vector3(0, -90, 0);
            nPosition = transform.position + transform.TransformDirection(Vector3.forward) * 2f;
            isMoving = true;
            animator.SetBool("Walk", true);

            //경사로 감지
            RaycastHit hit;
            if (Physics.Raycast(transform.position + new Vector3(0, 0.2f, 0), transform.forward, out hit, 1.5f))
            {
                if (hit.transform.CompareTag("SlopePlatform"))
                {
                    nPosition += new Vector3(0, 1.2f, 0);
                    yield return new WaitForSeconds(0.2f);
                }
            }
            else if (Physics.Raycast(transform.position + new Vector3(0, -0.8f, 0) + transform.forward * -0.8f, transform.forward, out hit, 2))
            {
                if (hit.transform.CompareTag("SlopePlatform"))
                {
                    nPosition += new Vector3(0, -1f, 0);
                    yield return new WaitForSeconds(0.1f);
                }
            }
            else if (Physics.Raycast(transform.position + new Vector3(0, -0.8f, 0) + transform.forward * -1.2f, transform.forward, out hit, 2))
            {
                if (hit.transform.CompareTag("SlopePlatform"))
                {
                    nPosition += new Vector3(0, -1f, 0);
                    yield return new WaitForSeconds(0.1f);
                }
            }

            yield return new WaitForSeconds(0.7f);
            leftMoving = false;
            isMoving = false;
            animator.SetBool("Walk", false);
        }
        IEnumerator rightMove()
        {
            MoveGoblin();
            transform.eulerAngles = new Vector3(0, 90, 0);
            nPosition = transform.position + transform.TransformDirection(Vector3.forward) * 2f;
            isMoving = true;
            animator.SetBool("Walk", true);

            //경사로 감지
            RaycastHit hit;
            if (Physics.Raycast(transform.position + new Vector3(0, 0.2f, 0), transform.forward, out hit, 1.5f))
            {
                if (hit.transform.CompareTag("SlopePlatform"))
                {
                    nPosition += new Vector3(0, 1.2f, 0);
                    yield return new WaitForSeconds(0.2f);
                }
            }
            else if (Physics.Raycast(transform.position + new Vector3(0, -0.8f, 0) + transform.forward * -0.8f, transform.forward, out hit, 2))
            {
                if (hit.transform.CompareTag("SlopePlatform"))
                {
                    nPosition += new Vector3(0, -1f, 0);
                    yield return new WaitForSeconds(0.1f);
                }
            }
            else if (Physics.Raycast(transform.position + new Vector3(0, -0.8f, 0) + transform.forward * -1.2f, transform.forward, out hit, 2))
            {
                if (hit.transform.CompareTag("SlopePlatform"))
                {
                    nPosition += new Vector3(0, -1f, 0);
                    yield return new WaitForSeconds(0.1f);
                }
            }

            yield return new WaitForSeconds(.7f);
            leftMoving = false;
            isMoving = false;
            animator.SetBool("Walk", false);

        }
        IEnumerator fowardMove()
        {
            MoveGoblin();
            transform.eulerAngles = new Vector3(0, 0, 0);
            nPosition = transform.position + transform.TransformDirection(Vector3.forward) * 2f;
            isMoving = true;
            animator.SetBool("Walk", true);

            //경사로 감지
            RaycastHit hit;
            if (Physics.Raycast(transform.position + new Vector3(0, 0.2f, 0), transform.forward, out hit, 1.5f))
            {
                if (hit.transform.CompareTag("SlopePlatform"))
                {
                    nPosition += new Vector3(0, 1.2f, 0);
                    yield return new WaitForSeconds(0.2f);
                }
            }
            else if (Physics.Raycast(transform.position + new Vector3(0, -0.8f, 0) + transform.forward * -0.8f, transform.forward, out hit, 2))
            {
                if (hit.transform.CompareTag("SlopePlatform"))
                {
                    nPosition += new Vector3(0, -1f, 0);
                    yield return new WaitForSeconds(0.1f);
                }
            }
            else if (Physics.Raycast(transform.position + new Vector3(0, -0.8f, 0) + transform.forward * -1.2f, transform.forward, out hit, 2))
            {
                if (hit.transform.CompareTag("SlopePlatform"))
                {
                    nPosition += new Vector3(0, -1f, 0);
                    yield return new WaitForSeconds(0.1f);
                }
            }

            yield return new WaitForSeconds(.7f);
            leftMoving = false;
            isMoving = false;
            animator.SetBool("Walk", false);

        }
        IEnumerator backMove()
        {
            MoveGoblin();
            transform.eulerAngles = new Vector3(0, 180, 0);
            nPosition = transform.position + transform.TransformDirection(Vector3.forward) * 2f;
            isMoving = true;
            animator.SetBool("Walk", true);

            //경사로 감지
            RaycastHit hit;
            if (Physics.Raycast(transform.position + new Vector3(0, 0.2f, 0), transform.forward, out hit, 1.5f))
            {
                if (hit.transform.CompareTag("SlopePlatform"))
                {
                    nPosition += new Vector3(0, 1.2f, 0);
                    yield return new WaitForSeconds(0.2f);
                }
            }
            else if (Physics.Raycast(transform.position + new Vector3(0, -0.8f, 0) + transform.forward * -0.8f, transform.forward, out hit, 2))
            {
                if (hit.transform.CompareTag("SlopePlatform"))
                {
                    nPosition += new Vector3(0, -1f, 0);
                    yield return new WaitForSeconds(0.1f);
                }
            }
            else if (Physics.Raycast(transform.position + new Vector3(0, -0.8f, 0) + transform.forward * -1.2f, transform.forward, out hit, 2))
            {
                if (hit.transform.CompareTag("SlopePlatform"))
                {
                    nPosition += new Vector3(0, -1f, 0);
                    yield return new WaitForSeconds(0.1f);
                }
            }
            yield return new WaitForSeconds(.7f);
            leftMoving = false;
            isMoving = false;
            animator.SetBool("Walk", false);

        }

        void MoveGoblin()
        {
            int i = 0;
            for (i = 0; i < Goblin.Length; i++)
            {
                Goblin[i].GetComponent<Goblin_E>().GoblinMove();
            }
        }
    }

