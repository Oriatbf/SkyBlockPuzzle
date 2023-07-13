using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwipe : MonoBehaviour
{
    [SerializeField]
    private float x1;
    [SerializeField]
    private float x2;
    [SerializeField]
    private float y1;
    [SerializeField]
    private float y2;

    public GameObject HorseOBJ;
    Animator animator;

    private RaycastHit PushBlockhit;
    private RaycastHit Enemyhit;
    public LayerMask PushBlocks;
    public LayerMask EnemyMask;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            x1 = Input.mousePosition.x;
            y1 = Input.mousePosition.y;
        }
        if (Input.GetMouseButtonUp(0))
        {
            x2 = Input.mousePosition.x;
            y2 = Input.mousePosition.y;

            if (x1 > x2)
            {
                if (Mathf.Abs(x1 - x2) > Mathf.Abs(y2 - y1))
                {
                    Debug.Log("Left");
                    if (Player.isGround && Player.MoveCoolTime <= 0 && !Horse.leftHorseNoGO)
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
                                StartCoroutine("AttackCorutine");                       
                            }
                            else if (Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), transform.forward, out PushBlockhit, 2, PushBlocks))
                            {
                                PushBlockhit.transform.GetComponent<PushBlock>().blockMove();
                                StartCoroutine("PushCorutine");
                            }
                            else
                            {
                                transform.eulerAngles = new Vector3(0, -90, 0);
                                transform.position += transform.forward * 2.006f;
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
                    if (Player.isGround && Player.MoveCoolTime <= 0 && !Horse.rightHorseNoGO)
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
                                StartCoroutine("AttackCorutine");

                            }
                            else if (Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), transform.forward, out PushBlockhit, 2, PushBlocks))
                            {
                                PushBlockhit.transform.GetComponent<PushBlock>().blockMove();
                                StartCoroutine("PushCorutine");
                            }
                            else
                            {
                                transform.eulerAngles = new Vector3(0, 90, 0);
                                transform.position += transform.forward * 2.006f;
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
                    if (Player.isGround && Player.MoveCoolTime <= 0 && !Horse.backHorseNoGo)
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
                                StartCoroutine("AttackCorutine");

                            }
                            else if (Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), transform.forward, out PushBlockhit, 2, PushBlocks))
                            {
                                PushBlockhit.transform.GetComponent<PushBlock>().blockMove();
                                StartCoroutine("PushCorutine");
                            }
                            else
                            {
                                transform.eulerAngles = new Vector3(0, 180, 0);
                                transform.position += transform.forward * 2.006f;
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
                    if (Player.isGround && Player.MoveCoolTime <= 0 && !Player.horseNoGo)
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
                                StartCoroutine("AttackCorutine");
                            }
                            else if (Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), transform.forward, out PushBlockhit, 2, PushBlocks))
                            {
                                PushBlockhit.transform.GetComponent<PushBlock>().blockMove();
                                StartCoroutine("PushCorutine");
                            }
                            else
                            {
                                transform.eulerAngles = new Vector3(0, 0, 0);
                                transform.position += transform.forward * 2.006f;
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

    IEnumerator AttackCorutine()
    {

        yield return new WaitForSeconds(0.6f);
        transform.position += transform.forward * 2.006f;
    }

    IEnumerator PushCorutine()
    {

        yield return new WaitForSeconds(0.5f);
        transform.position += transform.forward * 2.006f;
    }

}
