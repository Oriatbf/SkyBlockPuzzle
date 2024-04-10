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

    private void Awake()
    {
        Inst= this;
        
    }

    private void Start()
    {
        playerPos = new List<Vector3>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            UndoPlayerPos();
            UndoChangeBlock();
        }
    }

    public void SavePlayerPos(Vector3 pos)
    {
        playerPos.Add(pos);
        
    }

    public void UndoPlayerPos()
    {
        if(playerPos.Count > 0)
        {
            int last = playerPos.Count - 1;
            Debug.Log(last);
            if (playerPos[last] != Vector3.zero)
            {
                InGamePlayerMove.Inst.UndoPos(playerPos[last]);
            }            
            playerPos.Remove(playerPos[last]);
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
            int last = firstChangeBlockPos.Count - 1;
            Debug.Log(last);
            if (firstChangeBlockPos[last] != Vector3.zero)
            {
                var Fpos = firstChangeBlockPos[last];
                var Spos = secondChangeBlockPos[last];
                firstChangeBlock[last].transform.position = Fpos;
                secondChangeBlock[last].transform.position = Spos;
            }

            firstChangeBlockPos.Remove(firstChangeBlockPos[last]);
            secondChangeBlockPos.Remove(secondChangeBlockPos[last]);
            firstChangeBlock.Remove(firstChangeBlock[last]);
            secondChangeBlock.Remove(secondChangeBlock[last]);
        }

    }
}
