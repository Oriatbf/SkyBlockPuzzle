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
                if (UndoManager.Inst.isUndo)
                {
                    
                        UndoManager.Inst.isUndo = false;
                   
                }
                else
                {
                    InGameManager.Inst.GoblinsMove();
                    InGameUIManager.Inst.ChangeCoolDown();
                }
                   
                transform.position = nextPosition;  
                ActiveThings();
                
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
                    Save();
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

    private void Save()
    {
        UndoManager.Inst.SaveChangeBlock(Vector3.zero, Vector3.zero, null, null);
        UndoManager.Inst.SaveChangeBlockCool(InGameUIManager.Inst.changeCool);
        UndoManager.Inst.SavePlayerPos(nextPosition);
        if (UndoManager.Inst.isPushBlock)
        {
            GameObject[] pushBlock = GameObject.FindGameObjectsWithTag("PushBlock");
            foreach (GameObject box in pushBlock)
                box.GetComponent<PushBlock>().Save(false);
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
        List<Collider> allMoveTile = new List<Collider>();

        Collider[] widthMoveTiles = Physics.OverlapBox(transform.position, new Vector3(0.9f, 1f, 2.5f),Quaternion.identity,MoveTile);
        Collider[] heightMoveTiles = Physics.OverlapBox(transform.position, new Vector3(2.5f, 1f, 0.9f), Quaternion.identity, MoveTile);
        foreach (Collider activeTiles in widthMoveTiles)
            allMoveTile.Add(activeTiles);
        foreach (Collider activeTiles in heightMoveTiles)
            allMoveTile.Add(activeTiles);

        foreach (Collider activeAllTiles in allMoveTile)
        {
            Vector3 direction = transform.position - activeAllTiles.transform.position;
            RaycastHit endBlockHit;
            if (Physics.Raycast(activeAllTiles.transform.position, direction, out endBlockHit, 1.5f, EndBlock))
            {
                activeAllTiles.GetComponent<MeshRenderer>().enabled = false;

            }
            else
            {
                RaycastHit hit;
                if (Physics.Raycast(activeAllTiles.transform.position, Vector3.up, out hit, 5f, tileDisableLayer))
                    activeAllTiles.GetComponent<MeshRenderer>().enabled = false;
                else
                    activeAllTiles.GetComponent<MeshRenderer>().enabled = true;
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
                if(Physics.Raycast(pushBlock.transform.position, -direction, out endBlockHit, 1.5f, EndBlock) || pushBlock.GetComponent<PushBlock>().onFloor)
                    pushBlock.GetComponent<PushBlock>().ClickPoint.gameObject.SetActive(false);
                else
                    pushBlock.GetComponent<PushBlock>().ClickPoint.gameObject.SetActive(true);

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
        Gizmos.DrawWireCube(transform.position,new Vector3(1.8f,2,5));
        Gizmos.DrawWireCube(transform.position, new Vector3(5f, 2, 1.8f));
    }

    public void UndoPos(Vector3 undoPos)
    {
        nextPosition= undoPos;
        isMoving = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Goal"))
        {
            InGameManager.Inst.playerGoal();
        }
    }

   

    


}
