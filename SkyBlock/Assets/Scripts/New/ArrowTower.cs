using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTower : MonoBehaviour
{
    public GameObject bow;
    public int detectLength;
    [SerializeField] private float bowTurnCount;
    enum Dir
    {
        front, back, right, left
    }
    [Header("처음  바라보는 방향")]
    [SerializeField] Dir arrowDir;
    Vector3 dir = Vector3.zero;
  
    // Start is called before the first frame update
    void Start()
    {
        ChangeDir();
    }

    // Update is called once per frame
    void Update()
    {
       

        if (Input.GetKeyDown(KeyCode.G))
        {
            Action();
        }
    }

    void Attack()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), dir, out hit, 2.5f * detectLength))
        {
            Debug.Log(hit.collider);
            if (hit.transform.CompareTag("Player"))
                InGameManager.Inst.playerLose();
        }
    }




    public void Action()
    {
        if(InGameManager.Inst.curTurn%bowTurnCount == 0)
        {
            if (arrowDir == Dir.front)
                arrowDir = Dir.left;
            else if (arrowDir == Dir.left)
                arrowDir = Dir.back;
            else if(arrowDir == Dir.back)
                arrowDir = Dir.right;
            else if(arrowDir==Dir.right)
                arrowDir = Dir.front;

            ChangeDir();
        }
        else
            StartCoroutine(AttackDelay());
    }

    void ChangeDir()
    {
        StartCoroutine(AttackDelay());
        switch (arrowDir)
        {
            case Dir.front:
                bow.transform.LookAt(bow.transform.position + Vector3.forward);
                dir = Vector3.forward;
                return;
            case Dir.back:
                bow.transform.LookAt(bow.transform.position + Vector3.back);
                dir = Vector3.back;
                return;
            case Dir.right:
                bow.transform.LookAt(bow.transform.position + Vector3.right);
                dir = Vector3.right;
                return;
            case Dir.left:
                bow.transform.LookAt(bow.transform.position + Vector3.left);
                dir = Vector3.left;
                return;
        }
       
    }

    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(0.3f);
        Attack();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + new Vector3(0,0.5f,0), dir * 2.5f*detectLength) ;
    }
}
