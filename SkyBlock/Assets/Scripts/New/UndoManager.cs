using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndoManager : MonoBehaviour
{
    public static UndoManager Inst;
    public List<Vector3> playerPos = new List<Vector3>();
    public List<Vector3> firstChangeBlockPos = new List<Vector3>();
    public List<Vector3> secondChangeBlockPos = new List<Vector3>();
    public List<GameObject> firstChangeBlock= new List<GameObject>();
    public List<GameObject> secondChangeBlock= new List<GameObject>();

    public List<int> changeBlockCool = new List<int>();

    public List<Vector3> goblinPos = new List<Vector3>();   
    public List<Quaternion> goblinRot= new List<Quaternion>();
    public List<bool> goblinAlive= new List<bool>();

    public List<bool> bearAlive = new List<bool>();

    public List<GameObject> pushBlockCount = new List<GameObject>();

    public GameObject goblin;

    public GameObject bear;



    [SerializeField]
    private List<Vector3>[] pushBlockPos;

    public bool isUndo;
    public bool isPushBlock;
    public bool isGoblin;
    public bool isBear;

    private void Awake()
    {
       
        Inst = this;
        FindPushBlock();
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
        playerPos = new List<Vector3>();
        firstChangeBlock = new List<GameObject> ();

        if (isGoblin)
        {
            goblin = GameObject.FindGameObjectWithTag("Goblin");
        }

        if (isBear)
        {
            bear = GameObject.FindGameObjectWithTag("Bear");
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

    public void SaveGoblinPos(Vector3 pos,Quaternion rot,bool isChange)
    {
        if(isGoblin)
        {
            if (isChange)
                goblinPos.Add(pos);
            else
                goblinPos.Add(Vector3.zero);

            goblinRot.Add(rot);
        }
      
    }

    public void UndoGoblinPos()
    {
        if (isGoblin)
        {
            if (goblinPos.Count > 0)
            {
                int last = goblinPos.Count - 1;
                Debug.Log(last);
                if (goblinPos[last] != Vector3.zero)
                {
                    goblin.GetComponent<Goblin_E>().UndoPos(goblinPos[last], goblinRot[last]);
                    

                }
                goblinPos.RemoveAt(last);
                goblinRot.RemoveAt(last);
            }
        }
      
    }
}
