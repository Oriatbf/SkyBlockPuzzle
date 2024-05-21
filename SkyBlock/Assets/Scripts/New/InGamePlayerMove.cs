using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class InGamePlayerMove : MonoBehaviour
{
    public static InGamePlayerMove Inst;

    public bool isMoving;

    public LayerMask MoveTile, EndBlock, tileDisableLayer,PushBlockLayer,EnemyLayer,attackPointLayer;

    private Vector3 nextPosition;
    public Vector3 lookVector;

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
        lookVector = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !InGameUIManager.Inst.isChanging)
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            else
            {
                ClickMoveTile();
            }
           
        }

        if (!isMoving)
        {
            if(Input.GetMouseButtonDown(0)) 
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit attackPointHit;

                if (Physics.Raycast(ray, out attackPointHit, Mathf.Infinity, attackPointLayer))
                {
                    
                    transform.LookAt(new Vector3(attackPointHit.transform.position.x, transform.position.y, attackPointHit.transform.position.z));
                    attackPointHit.transform.parent.gameObject.SetActive(false);
                    animator.SetTrigger("Attack");
                    
                }
            }
           
        }
          
        if (isMoving)
        {   
            transform.position = Vector3.MoveTowards(transform.position,nextPosition,3f*Time.deltaTime);
            animator.SetBool("Walk", true);
            DisableAttackClickPoint();
            DisablePushBlockClickPoint();
            if (Mathf.Abs(Vector3.Distance(transform.position, nextPosition)) < 0.15f)
            {
                if (UndoManager.Inst.isUndo)
                {
                    
                    UndoManager.Inst.isUndo = false;
                   
                }
                else
                {
                    InGameManager.Inst.GoblinsMove();
                    InGameManager.Inst.SpidersMove();
                    InGameManager.Inst.ArrowTowerTurn();
                    InGameUIManager.Inst.ChangeCoolDown();
                }
                   
                transform.position = nextPosition;  
                if(!UndoManager.Inst.isGoblin || !UndoManager.Inst.isSpider)
                    ActiveThings();
                
                isMoving= false;
                animator.SetBool("Walk", false);
                if(InGameManager.Inst.isTutorial)
                    Tutorial.instance.TutorialPlayPos(transform.position);
                
            }
        }
    }

    public void PlayerAttack()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + new Vector3(0f, 0.5f, 0f), transform.forward,out hit, 3f, EnemyLayer))
        {
            StartCoroutine(AttackDelay(hit.transform));
        }
    }

    IEnumerator AttackDelay(Transform target)
    {
        Debug.Log("실핼");
        Save();
        yield return new WaitForSeconds(0.3f);

        if (SoundEffectManager.SFX != null)
            SoundEffectManager.PlaySoundEffect(4);

        if (ParticleManager.Particles != null)
            Instantiate(ParticleManager.Particles[2], target.transform.position + Vector3.up * 0.5f, ParticleManager.Particles[2].transform.rotation);

        target.gameObject.SetActive(false);
        ActiveThings();
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
                lookVector = new Vector3(nextPosition.x, transform.position.y, nextPosition.z);
                transform.LookAt(lookVector);
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
        UndoManager.Inst.SaveBearAlive();
        if (UndoManager.Inst.isPushBlock)
        {
            GameObject[] pushBlock = GameObject.FindGameObjectsWithTag("PushBlock");
            foreach (GameObject box in pushBlock)
                box.GetComponent<PushBlock>().Save(false);
        }
    }

    public void DisableMoveTile()
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
        yield return new WaitForSeconds(0.06f);
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

        Collider[] widthMoveTiles = Physics.OverlapBox(transform.position , new Vector3(0.9f, 1f, 2.5f),Quaternion.identity,MoveTile);
        Collider[] heightMoveTiles = Physics.OverlapBox(transform.position , new Vector3(2.5f, 1f, 0.9f), Quaternion.identity, MoveTile);
        allMoveTile.AddRange(widthMoveTiles);
        allMoveTile.AddRange(heightMoveTiles);

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
        DisableAttackClickPoint();
        List<Collider> enemies = new List<Collider>();

        Collider[] widthEnemies = Physics.OverlapBox(transform.position, new Vector3(0.9f, 1f, 2.5f), Quaternion.identity, EnemyLayer);
        Collider[] heightEnemies = Physics.OverlapBox(transform.position, new Vector3(2.5f, 1f, 0.9f), Quaternion.identity, EnemyLayer);
        enemies.AddRange(widthEnemies);
        enemies.AddRange(heightEnemies);
        
        foreach (Collider enemy in enemies)
        {
            RaycastHit endBlockHit;
            Vector3 direction = transform.position - enemy.transform.position;
            if (Physics.Raycast(enemy.transform.position, direction, out endBlockHit, 1.5f, EndBlock))
            {
                enemy.GetComponent<EnemyClickpoint>().ClickPoint.SetActive(false);
                Debug.Log("false");

            }
            else
            {
                enemy.GetComponent<EnemyClickpoint>().ClickPoint.SetActive(true);
                Debug.Log("true");
            }
        }
        ActiveMoveTile();
    }

    private void DisablePushBlockClickPoint()
    {
        if (UndoManager.Inst.isPushBlock)
        {
            for (int i = 0; i < UndoManager.Inst.pushBlockCount.Count; i++)
                UndoManager.Inst.pushBlockCount[i].GetComponent<PushBlock>().ClickPoint.SetActive(false);
        }
    }

    private void DisableAttackClickPoint()
    {
        if (UndoManager.Inst.isGoblin)
        {
            foreach (GameObject goblin in UndoManager.Inst.goblinCount)
            {
                goblin.GetComponent<EnemyClickpoint>().ClickPoint.SetActive(false);
            }
        }
        if (UndoManager.Inst.isBear)
            UndoManager.Inst.bear.GetComponent<EnemyClickpoint>().ClickPoint.SetActive(false);

      
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position ,new Vector3(1.8f,2f,5));
        Gizmos.DrawWireCube(transform.position , new Vector3(5f, 2f, 1.8f));
        Gizmos.DrawRay(transform.position + new Vector3(0f, 0.5f, 0f), transform.forward * 3f);

     
    }

    public void UndoPos(Vector3 undoPos)
    {
        nextPosition= undoPos;
        isMoving = true;

    }

    private void OnTriggerEnter(Collider other)
    {
        switch(other.tag)
        {
            case "Goal":
                InGameManager.Inst.playerGoal();
                return;

            case "DamageObj":
                InGameManager.Inst.playerLose();
                Destroy(other.gameObject);
                return;
        }
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.collider.tag)
        {
          

            case "DamageObj":
                InGameManager.Inst.playerLose();
                Destroy(collision.collider.gameObject);
                return;
        }
    }






}
