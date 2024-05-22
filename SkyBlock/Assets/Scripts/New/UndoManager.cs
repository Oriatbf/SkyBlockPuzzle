using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndoManager : MonoBehaviour
{
    public static UndoManager Inst;
    public List<Vector3> playerPos = new List<Vector3>();
    public List<Vector3> playerRot = new List<Vector3>();
    public List<Vector3> firstChangeBlockPos = new List<Vector3>();
    public List<Vector3> secondChangeBlockPos = new List<Vector3>();
    public List<GameObject> firstChangeBlock= new List<GameObject>();
    public List<GameObject> secondChangeBlock= new List<GameObject>();

    public List<int> changeBlockCool = new List<int>();

    public List<Vector3>[] goblinPos;  
    public List<Quaternion>[] goblinRot;
    public List<bool>[] goblinAlive;

    public List<bool> bearAlive = new List<bool>();

    public List<GameObject> pushBlockCount = new List<GameObject>();
    public List<GameObject> goblinCount = new List<GameObject>();



    public GameObject bear,spider;

    public GameObject player;



    [SerializeField]
    private List<Vector3>[] pushBlockPos;


    public bool isUndo;
    public bool isPushBlock;
    public bool isGoblin;
    public bool isSpider;
    public bool isBear;
    public bool isArrowTower;

    private void Awake()
    {
       
        Inst = this;
        FindPushBlock();
        FindGoblin();
    }

    private void FindGoblin()
    {
        if (isGoblin)
        {
            GameObject[] goblins = GameObject.FindGameObjectsWithTag("Goblin");
            for(int i = 0; i < goblins.Length; i++)
            {
                goblinCount.Add(goblins[i]);
            }
            goblinPos = new List<Vector3>[goblinCount.Count];
            goblinRot = new List<Quaternion>[goblinCount.Count];
            goblinAlive = new List<bool>[goblinCount.Count];

            for(int i = 0; i < goblinCount.Count; i++)
            {
                goblinPos[i] = new List<Vector3>();
                goblinRot[i] = new List<Quaternion>();
                goblinAlive[i] = new List<bool>();
            }

        }
    }

    private void FindPushBlock()
    {
        if (isPushBlock)
        {
            GameObject[] pushBlock = GameObject.FindGameObjectsWithTag("PushBlock");
            for (int i = 0; i < pushBlock.Length; i++)
            {
                pushBlockCount.Add(pushBlock[i]);
            }

            pushBlockPos = new List<Vector3>[pushBlockCount.Count];
            for (int i = 0; i < pushBlockCount.Count; i++)
            {
                pushBlockPos[i] = new List<Vector3>();
            }
        }
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");


        if (isBear)
        {
            bear = GameObject.FindGameObjectWithTag("Bear");
        }

        if (isSpider)
        {
            spider = GameObject.FindGameObjectWithTag("Spider");
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.U) && !isUndo)
        {
            UndoPlayerPos();
            UndoChangeBlock();
            UndoPushBlock();
            UndoChangeBlockCool();
            UndoGoblinPos();
            UndoBearAlive();
            InGamePlayerMove.Inst.ActiveThings();
        }
    }

    public void SaveBearAlive()
    {
        if(isBear)
            bearAlive.Add(bear.activeSelf);
    }

    public void UndoBearAlive()
    {
        if (isBear)
        {
            int last = bearAlive.Count - 1;
            if (bearAlive.Count > 0)
            {
                bear.SetActive(bearAlive[last]);
                bearAlive.RemoveAt(last);
            }

        }
       
    }

    public void SaveChangeBlockCool(int cool)
    {
        changeBlockCool.Add(cool);
    }

    public void UndoChangeBlockCool()
    {
        int last = changeBlockCool.Count - 1;
        if(changeBlockCool.Count > 0)
        {
            InGameUIManager.Inst.changeCool = changeBlockCool[last];
            changeBlockCool.RemoveAt(last);
            InGameUIManager.Inst.UndoChangeCoolDown();
        }

        
    }

    public void SavePlayerPos(Vector3 pos)
    {
        playerPos.Add(pos);
        playerRot.Add(InGamePlayerMove.Inst.lookVector);
        
    }

    public void UndoPlayerPos()
    {
        if (playerPos.Count > 0)
        {
            int last = playerPos.Count - 1;
            Debug.Log(last);
            if (playerPos[last] != Vector3.zero)
            {
                InGamePlayerMove.Inst.UndoPos(playerPos[last]);
                InGameManager.Inst.curTurn--;
                isUndo = true;
            }
            playerPos.RemoveAt(last);
            playerRot.RemoveAt(last);
        }
    }

    public void SaveChangeBlock(Vector3 Fpos,Vector3 Spos, GameObject Fblock,GameObject Sblock)
    {
        firstChangeBlock.Add(Fblock);
        secondChangeBlock.Add(Sblock);
        firstChangeBlockPos.Add(Fpos);
        secondChangeBlockPos.Add(Spos);
    }

    public void UndoChangeBlock()
    {
        if (firstChangeBlockPos.Count > 0)
        {
            int last = firstChangeBlock.Count - 1;
            Debug.Log(last);
            if (firstChangeBlock[last] != null)
            {
                var Fpos = firstChangeBlockPos[last];
                var Spos = secondChangeBlockPos[last];
                firstChangeBlock[last].transform.position = Fpos;
                secondChangeBlock[last].transform.position = Spos;
            }
            Debug.Log(last);
            Debug.Log(firstChangeBlock[last]); 
            firstChangeBlock.RemoveAt(last);
            secondChangeBlock.RemoveAt(last);
            firstChangeBlockPos.RemoveAt(last);
            secondChangeBlockPos.RemoveAt(last);
        }

    }

    public void SavePushBlock(GameObject pushBlock, Vector3 pos,bool isChange)
    {
        if (isPushBlock)
        {
            if (!isChange)
            {
                for (int i = 0; i < pushBlockCount.Count; i++)
                {

                    pushBlockPos[i].Add(Vector3.zero);

                }
            }
            else
            {
                for (int i = 0; i < pushBlockCount.Count; i++)
                {
                    if (pushBlockCount[i] == pushBlock)
                    {
                        pushBlockPos[i].Add(pos);
                    }
                }
            }
        }
        
    }

    public void UndoPushBlock()
    {
        if (isPushBlock)
        {
            if (pushBlockPos[0].Count > 0)
            {
                int last = pushBlockPos[0].Count - 1;
                for (int i = 0; i < pushBlockCount.Count; i++)
                {
                    if (pushBlockPos[i][last] != Vector3.zero)
                    {
                        pushBlockCount[i].GetComponent<PushBlock>().Undo(pushBlockPos[i][last]);
                    }
                    pushBlockPos[i].RemoveAt(last);
                }
            }
        }
    }

    public void SaveGoblinPos(GameObject goblin, Vector3 pos,Quaternion rot,bool isChange)
    {
        if(isGoblin)
        {
            if (isChange)
            {
                for(int i = 0; i < goblinCount.Count; i++)
                {
                    if (goblinCount[i] == goblin)
                        goblinPos[i].Add(pos);
                }
            }
            else
            {
                for (int i = 0; i < goblinCount.Count; i++)
                {
                    
                     goblinPos[i].Add(Vector3.zero);
                }
            } 
            
            for(int i = 0; i < goblinCount.Count; i++)
            {
                if(goblin != null)
                {
                    if (goblinCount[i] == goblin)
                        goblinRot[i].Add(rot);
                }
                else
                {
                    goblinRot[i].Add(goblinCount[i].transform.rotation);
                }

                goblinAlive[i].Add(goblinCount[i].activeSelf);   
            }        
        }
      
    }

    public void UndoGoblinPos()
    {
        if (isGoblin && goblinPos[0].Count > 0)
        {  
            int last = goblinPos[0].Count - 1;
            for(int i = 0; i < goblinCount.Count; i++)
            {
                if (goblinPos[i][last] != Vector3.zero)
                {
                    goblinCount[i].GetComponent<Goblin_E>().UndoPos(goblinPos[i][last], goblinRot[i][last]);
                    goblinPos[i].RemoveAt(last);
                    goblinRot[i].RemoveAt(last);
                    goblinCount[i].SetActive(goblinAlive[i][last]);
                    goblinAlive[i].RemoveAt(last);
                }
            }            
            
        }
      
    }
}
