using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    [SerializeField] LayerMask PlayerLayer;
    [SerializeField] LayerMask DontMoveEnd; //����� ����
    [SerializeField] LayerMask DontPushorEnemy; //�����ִ� push��, �� ����
    [SerializeField] PlayerSwipe Plsw;
    [SerializeField] Animator PlswAnimator;
    [SerializeField] MeshRenderer mer; //���̰� �Ⱥ��̰�
    int platfDirec; //������ ��ü�� ��� ��ġ���ִ��� ����

    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.up * 1);
    }*/

    void Update()
    {
        RaycastHit PlayerHit;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        //���ΰ�
        if (Physics.Raycast(transform.position + new Vector3(0, 0.2f, -1), Quaternion.Euler(0, 0, 0) * new Vector3(0, 0, -1), out PlayerHit, 1.5f, PlayerLayer)
        && !Physics.Raycast(transform.position + new Vector3(0, 0.2f, 0), Quaternion.Euler(0, 0, 0) * new Vector3(0, 0, -1), out PlayerHit, 2f, DontMoveEnd)
        && !Physics.Raycast(transform.position, Vector3.up, out PlayerHit, 1f, DontPushorEnemy))
        {
            platfDirec = 1;
            mer.enabled = true;
            if (Input.GetMouseButton(0))
            {
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, Plsw.MoveTiles) && Plsw.oneTimeActive && !PlayerSwipe.isChangeButton)
                {

                    Debug.Log("Test2");
                    PlatformMove PlatDrecCheck = hit.collider.GetComponent<PlatformMove>();
                    if (PlatDrecCheck.platfDirec == 1)
                    {

                        Debug.Log("Test3");
                        if (Player.isGround && Player.MoveCoolTime <= 0 && PlayerSwipe.yesFMove)
                        {

                            Debug.Log("Test4");
                            Plsw.oneTimeActive = false;
                            Plsw.transform.eulerAngles = new Vector3(0, 0, 0);
                            Plsw.StartCoroutine(Plsw.fowardMove());
                        }
                    }
                }
            }
        }
        //���������� ��
        else if (Physics.Raycast(transform.position + new Vector3(-1, 0.2f, 0), Quaternion.Euler(0, 90, 0) * new Vector3(0, 0, -1), out PlayerHit, 1.5f, PlayerLayer)
            && !Physics.Raycast(transform.position + new Vector3(0, 0.2f, 0), Quaternion.Euler(0, 90, 0) * new Vector3(0, 0, -1), out PlayerHit, 2f, DontMoveEnd)
            && !Physics.Raycast(transform.position, Vector3.up, out PlayerHit, 1f, DontPushorEnemy))
        {
            platfDirec = 2;
            mer.enabled = true;
            if (Input.GetMouseButton(0))
            {
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, Plsw.MoveTiles) && Plsw.oneTimeActive && !PlayerSwipe.isChangeButton)
                {
                    PlatformMove PlatDrecCheck = hit.collider.GetComponent<PlatformMove>();
                    if (PlatDrecCheck.platfDirec == 2)
                    {
                        if (Player.isGround && Player.MoveCoolTime <= 0 && PlayerSwipe.yesRMove)
                        {
                            Plsw.oneTimeActive = false;
                            Plsw.transform.eulerAngles = new Vector3(0, 90, 0);
                            Plsw.StartCoroutine(Plsw.rightMove());
                        }
                    }
                }
            }
        }
        //�Ʒ��� ��
        else if (Physics.Raycast(transform.position + new Vector3(0, 0.2f, 1), Quaternion.Euler(0, 180, 0) * new Vector3(0, 0, -1), out PlayerHit, 1.5f, PlayerLayer)
            && !Physics.Raycast(transform.position + new Vector3(0, 0.2f, 0), Quaternion.Euler(0, 180, 0) * new Vector3(0, 0, -1), out PlayerHit, 2f, DontMoveEnd)
            && !Physics.Raycast(transform.position, Vector3.up, out PlayerHit, 1f, DontPushorEnemy))
        {
            mer.enabled = true;
            platfDirec = 3;
            if (Input.GetMouseButton(0))
            {
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, Plsw.MoveTiles) && Plsw.oneTimeActive && !PlayerSwipe.isChangeButton)
                {
                    PlatformMove PlatDrecCheck = hit.collider.GetComponent<PlatformMove>();
                    if (PlatDrecCheck.platfDirec == 3)
                    {
                        if (Player.isGround && Player.MoveCoolTime <= 0 && PlayerSwipe.yesBMove)
                        {
                            Plsw.oneTimeActive = false;
                            Plsw.transform.eulerAngles = new Vector3(0, 180, 0);
                            Plsw.StartCoroutine(Plsw.backMove());


                        }
                    }
                }
            }
        }
        //�������� ��
        else if (Physics.Raycast(transform.position + new Vector3(1, 0.2f, 0), Quaternion.Euler(0, 270, 0) * new Vector3(0, 0, -1), out PlayerHit, 1.5f, PlayerLayer)
            && !Physics.Raycast(transform.position + new Vector3(0, 0.2f, 0), Quaternion.Euler(0, 270, 0) * new Vector3(0, 0, -1), out PlayerHit, 2f, DontMoveEnd)
            && !Physics.Raycast(transform.position, Vector3.up, out PlayerHit, 1f, DontPushorEnemy))
        {
            mer.enabled = true;
            platfDirec = 4;
            if (Input.GetMouseButton(0))
            {
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, Plsw.MoveTiles) && Plsw.oneTimeActive && !PlayerSwipe.isChangeButton)
                {
                    PlatformMove PlatDrecCheck = hit.collider.GetComponent<PlatformMove>();
                    if (PlatDrecCheck.platfDirec == 4)
                    {
                        if (Player.isGround && Player.MoveCoolTime <= 0 && PlayerSwipe.yesLMove)
                        {
                            Plsw.oneTimeActive = false;
                            Plsw.transform.eulerAngles = new Vector3(0, -90, 0);
                            Plsw.StartCoroutine(Plsw.leftMove());
                        }
                    }
                }
            }
        }
        else
        {
            mer.enabled = false;
            platfDirec = 0;
        }

        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(Plsw.transform.position + new Vector3(0, 0.5f, 0), Plsw.transform.forward, out Plsw.Enemyhit, 2, Plsw.EnemyMask)
                && Plsw.oneTimeActive && !PlayerSwipe.isChangeButton && Player.isGround && Player.MoveCoolTime <= 0)
            {
                Plsw.oneTimeActive = false;
                Plsw.transform.eulerAngles = new Vector3(0, -90, 0);

                PlswAnimator.SetTrigger("Attack");
                Plsw.StartCoroutine(Plsw.OneTimeActiveCool());
            }
        }
    }
}
