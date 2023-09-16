using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] LayerMask MoveTile;
    [SerializeField] LayerMask EndBlock;
    [SerializeField] LayerMask PushBlock;
    [SerializeField] LayerMask Enemy;
    [SerializeField] LayerMask PushStopBlock;
    [SerializeField] LayerMask PlayerLayer;
    [SerializeField] Animator animator;
    [SerializeField] Player playerSc;
    public GameObject[] Goblin, Spider;
    public GameObject changeEvent;
    Vector3 nPosition;

    public static float timer; // Push 0.5f, golbin walk 1f, goblin attack 2f
    bool isMoving;

    void Start()
    {
        Goblin = GameObject.FindGameObjectsWithTag("Goblin");
        Spider = GameObject.FindGameObjectsWithTag("Spider");
        nPosition = transform.position;
        Detect();
        Tutorial.instance.TutorialPlay();
    }

    void Update()
    {
        if (timer >= 0)
            timer -= Time.deltaTime;

        if (timer <= 0 && timer >= -1)
        {
            timer = -5f;
            Detect();
        }

        if (Vector3.Distance(transform.position, nPosition) > 0.3f)
        {
            Move();
        }
        else if(isMoving)
        {
            transform.position = nPosition;
            animator.SetBool("Walk", false);
            isMoving = false;
            AllMeshFalse();
            timer = 0.1f; // Detect();
            MoveGoblin();
            playerSc.TurnStac += 1;
            Tutorial.instance.TutorialPlayPos(transform.position);
        }
        if (Input.GetMouseButtonDown(0) && isMoving == false && timer <= 0 && !EventSystem.current.IsPointerOverGameObject())
        {
            Go();
        }
    }

    void Go()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, MoveTile))
        {
            MeshRenderer hitRenderer = hit.transform.GetComponent<MeshRenderer>();
            if (hitRenderer != null && hitRenderer.enabled)
            {
                nPosition = hit.transform.position;
                transform.LookAt(new Vector3(nPosition.x, transform.position.y, nPosition.z));
                changeEvent.GetComponent<ChangeBlock>().ChangeCoolStac -= 1;
                isMoving = true;

            }
        }
    }
    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, nPosition, 3f * Time.deltaTime);
        animator.SetBool("Walk", true);
    }

    public void Detect()
    {
        //COl ALL
        Collider[] Allcolliders = Physics.OverlapSphere(this.transform.position, 10000f, MoveTile);
        foreach (Collider Allcollider in Allcolliders)
        {
            MeshRenderer otherMeshRenderer = Allcollider.GetComponent<MeshRenderer>();
            otherMeshRenderer.enabled = false;
        }

        //PushBlock ALL
        Collider[] AllPushBlocks = Physics.OverlapSphere(this.transform.position, 10000f, PushBlock);
        foreach (Collider AllPushBLock in AllPushBlocks)
        {
            PushBlock PushBlockSc = AllPushBLock.GetComponent<PushBlock>();
            if (PushBlockSc != null)
                PushBlockSc.ClickPoint.gameObject.SetActive(false);
        }

        //Enemy ALL
        Collider[] AllEnemyBlocks = Physics.OverlapSphere(this.transform.position, 10000f, Enemy);
        foreach (Collider AllEnemyBLock in AllEnemyBlocks)
        {
            EnemyClickpoint EnemySc = AllEnemyBLock.GetComponent<EnemyClickpoint>();
            if (EnemySc != null)
                EnemySc.ClickPoint.gameObject.SetActive(false);
        }

        //COL 2f Sphere ON
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, 2f, MoveTile);
        foreach (Collider collider in colliders)
        {
            MeshRenderer otherMeshRenderer = collider.GetComponent<MeshRenderer>();
            otherMeshRenderer.enabled = true;

            //COL MIT OFF
            RaycastHit hit;
            if (collider == Physics.Raycast(transform.position + Vector3.up, Vector3.down, out hit, 2f, MoveTile))
            {
                MeshRenderer detectMeshRenderer = hit.collider.GetComponent<MeshRenderer>();
                detectMeshRenderer.enabled = false;
            }

            //Endblock OFF
            Vector3 direction = transform.position - collider.transform.position;
            if (Physics.Raycast(collider.transform.position, direction, out hit, 1.5f, EndBlock))
            {
                Vector3 direction2 = collider.transform.position - hit.transform.position;
                if (Physics.Raycast(hit.transform.position, direction2, out hit, 1.5f, MoveTile))
                {
                    MeshRenderer detectMeshRenderer = hit.collider.GetComponent<MeshRenderer>();
                    detectMeshRenderer.enabled = false;
                }
            }
            //Enemy
            if (collider == Physics.Raycast(collider.transform.position, Vector3.up, out hit, 5f, Enemy))
            {
                EnemyClickpoint EnemySc = hit.transform.GetComponent<EnemyClickpoint>();
                if (collider == Physics.Raycast(hit.transform.position + Vector3.up, Vector3.down, out hit, 2f, MoveTile))
                {
                    MeshRenderer detectMeshRenderer = hit.collider.GetComponent<MeshRenderer>();
                    if (detectMeshRenderer.enabled == true)
                    {
                        EnemySc.ClickPoint.gameObject.SetActive(true);
                        detectMeshRenderer.enabled = false;
                    }
                }
            }

            //PushBLock
            if (collider == Physics.Raycast(collider.transform.position, Vector3.up, out hit, 2f, PushBlock))
            {
                    if (!Physics.Raycast(hit.transform.position + new Vector3(0, 0.1f, 0), Quaternion.Euler(0, 270, 0) * new Vector3(0, 0, -1), 1.2f, PushStopBlock) &&
                    Physics.Raycast(hit.transform.position + new Vector3(0, 0.1f, 0), Quaternion.Euler(0, 90, 0) * new Vector3(0, 0, -1), 2f, PlayerLayer) ||
                     !Physics.Raycast(hit.transform.position + new Vector3(0, 0.1f, 0), Quaternion.Euler(0, 90, 0) * new Vector3(0, 0, -1), 1.2f, PushStopBlock) &&
                    Physics.Raycast(hit.transform.position + new Vector3(0, 0.1f, 0), Quaternion.Euler(0, 270, 0) * new Vector3(0, 0, -1), 2f, PlayerLayer) ||
                     !Physics.Raycast(hit.transform.position + new Vector3(0, 0.1f, 0), Quaternion.Euler(0, 180, 0) * new Vector3(0, 0, -1), 1.2f, PushStopBlock) &&
                    Physics.Raycast(hit.transform.position + new Vector3(0, 0.1f, 0), Quaternion.Euler(0, 0, 0) * new Vector3(0, 0, -1), 2f, PlayerLayer) ||
                     !Physics.Raycast(hit.transform.position + new Vector3(0, 0.1f, 0), Quaternion.Euler(0, 0, 0) * new Vector3(0, 0, -1), 1.2f, PushStopBlock) &&
                    Physics.Raycast(hit.transform.position + new Vector3(0, 0.1f, 0), Quaternion.Euler(0, 180, 0) * new Vector3(0, 0, -1), 2f, PlayerLayer))
                {
                    PushBlock PushBlockSc = hit.transform.GetComponent<PushBlock>();
                    if(hit.transform.position.y > transform.position.y)
                    {
                        if (collider == Physics.Raycast(hit.transform.position + Vector3.up, Vector3.down, out hit, 2f, MoveTile))
                        {
                            MeshRenderer detectMeshRenderer = hit.collider.GetComponent<MeshRenderer>();
                            if (detectMeshRenderer.enabled == true)
                            {
                                PushBlockSc.ClickPoint.gameObject.SetActive(true);
                                detectMeshRenderer.enabled = false;
                            }
                        }
                    }
                }
            }
        }
    }
    public void AllMeshFalse()
    {
        Collider[] Allcolliders = Physics.OverlapSphere(this.transform.position, 10000f, MoveTile);
        foreach (Collider Allcollider in Allcolliders)
        {
            MeshRenderer otherMeshRenderer = Allcollider.GetComponent<MeshRenderer>();
            otherMeshRenderer.enabled = false;
        }
    }
    void MoveGoblin()
    {
        int i = 0;
        for (i = 0; i < Goblin.Length; i++)
        {
            Goblin[i].GetComponent<Goblin_E>().GoblinMove();
        }
        for (i = 0; i < Spider.Length; i++)
        {
            Spider[i].GetComponent<Spider_E>().SpiderMove();
        }
    }

    public void GoblinDetect()
    {
        Goblin = null;
        if(GameObject.FindGameObjectsWithTag("Goblin") != null)
            Goblin = GameObject.FindGameObjectsWithTag("Goblin");
        Spider = null;
        if (GameObject.FindGameObjectsWithTag("Spider") != null)
            Goblin = GameObject.FindGameObjectsWithTag("Spider");
    }

}
