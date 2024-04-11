using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InGamePlayerMove : MonoBehaviour
{
    public static InGamePlayerMove Inst;

    public bool isMoving;

    public LayerMask MoveTile, EndBlock, tileDisableLayer,PushBlockLayer,EnemyLayer;

    private Vector3 nextPosition;

    Animator animator;

    private void Awake()
    {
        animator= GetComponent<Animator>();
        Inst= this;
    }

    // Start is called before the first frame update
    void Start()
    {
        ActiveThings();
        nextPosition= transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !InGameUIManager.Inst.isChanging)
        {
            ClickMoveTile();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            ActiveThings();
        }
          
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position,nextPosition,3f*Time.deltaTime);
            animator.SetBool("Walk", true);
            if (Mathf.Abs(Vector3.Distance(transform.position, nextPosition)) < 0.1f)
            {
                transform.position = nextPosition;  
                ActiveThings();
                InGameUIManager.Inst.ChangeCoolDown();
                isMoving= false;
                animator.SetBool("Walk", false);
            }
        }
    }

    private void ClickMoveTile()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, MoveTile))
        {       
            
            MeshRenderer hitRenderer = hit.transform.GetComponent<MeshRenderer>();
            if (hitRenderer != null && hitRenderer.enabled)
            {
                InGameManager.Inst.curTurn++;
                if (nextPosition != null)
                {
                    UndoManager.Inst.SaveChangeBlock(Vector3.zero, Vector3.zero, null, null);
                    UndoManager.Inst.SavePlayerPos(nextPosition);
                }
                if (SoundEffectManager.SFX != null)
                    SoundEffectManager.PlaySoundEffect(0);

                nextPosition = hit.transform.position;
                
                transform.LookAt(new Vector3(nextPosition.x, transform.position.y, nextPosition.z));
                isMoving = true;
                DisableMoveTile();
            }
        }

    }

    private void DisableMoveTile()
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
        foreach (GameObject tile in tiles)
        {
            tile.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    public void ActiveThings()
    {
        StartCoroutine(waitActiveThings());
    }

    IEnumerator waitActiveThings()
    {
        DisableMoveTile();
        yield return new WaitForSeconds(0.03f);
        ActiveMoveTile();
        ActivePushBlock();
        ActiveEnemy();
    }

    private void ActiveMoveTile()
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
        foreach (GameObject tile in tiles)
        {
            tile.GetComponent<MeshRenderer>().enabled = false;
        }

        Collider[] moveTiles = Physics.OverlapBox(transform.position, new Vector3(2.5f, 2.5f, 2.5f),Quaternion.identity,MoveTile);
        foreach(Collider activeTiles in moveTiles)
        {
            Vector3 direction = transform.position - activeTiles.transform.position;
            RaycastHit endBlockHit;
            if (Physics.Raycast(activeTiles.transform.position, direction, out endBlockHit, 1.5f, EndBlock))
            {
                activeTiles.GetComponent<MeshRenderer>().enabled = false;

            }
            else
            {
                RaycastHit hit;
                if (Physics.Raycast(activeTiles.transform.position, Vector3.up, out hit, 5f, tileDisableLayer))
                    activeTiles.GetComponent<MeshRenderer>().enabled = false;
                else
                    activeTiles.GetComponent<MeshRenderer>().enabled = true;
            }
        }

        
    }

    private void ActivePushBlock()
    {
        Collider[] pushBlocks = Physics.OverlapBox(transform.position, new Vector3(2.5f, 2.5f, 2.5f), Quaternion.identity, PushBlockLayer);
        foreach(Collider pushBlock in pushBlocks)
        {
            RaycastHit endBlockHit;
            Vector3 direction = transform.position - pushBlock.transform.position;
            if (Physics.Raycast(pushBlock.transform.position, direction, out endBlockHit, 1.5f, EndBlock))
            {
                pushBlock.GetComponent<PushBlock>().ClickPoint.SetActive(false);

            }
            else
            {
                pushBlock.GetComponent<PushBlock>().ClickPoint.SetActive(true);

            }
        }
    }

    private void ActiveEnemy()
    {
        Collider[] enemies = Physics.OverlapBox(transform.position, new Vector3(2.5f, 2.5f, 2.5f), Quaternion.identity, EnemyLayer);
        foreach (Collider enemy in enemies)
        {
            RaycastHit endBlockHit;
            Vector3 direction = transform.position - enemy.transform.position;
            if (Physics.Raycast(enemy.transform.position, direction, out endBlockHit, 1.5f, EndBlock))
            {
                enemy.GetComponent<EnemyClickpoint>().ClickPoint.SetActive(false);

            }
            else
            {
                enemy.GetComponent<EnemyClickpoint>().ClickPoint.SetActive(true);

            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position,new Vector3(5,5,5));
    }

    public void UndoPos(Vector3 undoPos)
    {
        nextPosition= undoPos;
        isMoving = true;
    }


}
