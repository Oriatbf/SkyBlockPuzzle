using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] LayerMask MoveTile;
    [SerializeField] Animator animator;
    [SerializeField] Player playerSc;
    public GameObject[] Goblin;
    Vector3 nPosition;
    bool isMoving;

    void Start()
    {
        Goblin = GameObject.FindGameObjectsWithTag("Goblin");
        nPosition = transform.position;
        Detect();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, nPosition) > 0.3f)
        {
            Move();
        }
        else if(isMoving)
        {
            transform.position = nPosition;
            animator.SetBool("Walk", false);
            isMoving = false;
            Detect();
            MoveGoblin();
            playerSc.TurnStac += 1; //임시
        }

        if (Input.GetMouseButtonDown(0) && isMoving == false)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, MoveTile))
            {
                nPosition = hit.transform.position;
                transform.LookAt(new Vector3(nPosition.x, transform.position.y, nPosition.z));
                isMoving = true;
            }
        }
    }

    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, nPosition, 3f * Time.deltaTime);
        animator.SetBool("Walk", true);
    }

    void Detect()
    {
        //모든 콜라이더 감지 (매쉬 안보이게)
        Collider[] Allcolliders = Physics.OverlapSphere(this.transform.position, 10000f, MoveTile);
        foreach (Collider Allcollider in Allcolliders)
        {
            MeshRenderer otherMeshRenderer = Allcollider.GetComponent<MeshRenderer>();
            otherMeshRenderer.enabled = false;
        }

        //주면 콜라이더 감지 (매쉬 보이게)
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, 2f, MoveTile);
        foreach (Collider collider in colliders)
        {
            MeshRenderer otherMeshRenderer = collider.GetComponent<MeshRenderer>();
            otherMeshRenderer.enabled = true;

            //자기 밑에있는 콜라이더(매쉬 안보이게)
            RaycastHit hitt;
            if (collider == Physics.Raycast(transform.position + Vector3.up, Vector3.down, out hitt, 3f, MoveTile))
            {
                MeshRenderer detectMeshRenderer = hitt.collider.GetComponent<MeshRenderer>();
                detectMeshRenderer.enabled = false;
            }

        }
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
