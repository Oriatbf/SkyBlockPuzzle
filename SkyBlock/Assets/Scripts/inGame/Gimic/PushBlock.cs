using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PushBlock : MonoBehaviour
{
    [SerializeField]
    private LayerMask ArrowLayer, PushBlockFloor;
    [SerializeField]
    private LayerMask MoveTile;

    public GameObject player;
    public GameObject ClickPoint,moveTile;
    public Ease ease;
    public bool onFloor;

    [SerializeField] GameObject[] changeColorObj;
    public Material[] deafultMat;
    public Material invisableMat;

    public Vector3 prePos;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ClickPoint.gameObject.SetActive(false);
        moveTile.transform.parent = null;
    }

    private void FixedUpdate()
    {
     

    }
    private void Update()
    {
        RaycastHit pushBlockFloorHit;
        if(Physics.Raycast(transform.position, Vector3.down, out pushBlockFloorHit, 1.5f, PushBlockFloor))
        {
            ClickPoint.gameObject.SetActive(false);
            onFloor = true;
        }
       
     

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit boxClickPointHit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out boxClickPointHit, Mathf.Infinity, ArrowLayer))
            {
                
                Vector3 lookDirection = transform.position - player.transform.position;
                Debug.Log(lookDirection);
                lookDirection.y = 0f;
                player.transform.rotation = Quaternion.LookRotation(lookDirection);

                SoundEffectManager.PlaySoundEffect(2);

                blockMove();
            }
        }
        
    }

    public void blockMove()
    {
        prePos= transform.position;
        Save(true);
        Vector3 dir = transform.position - player.transform.position;
        dir.Normalize();
        dir *= 2;
        dir.y = 0f;
        StartCoroutine(BlockMoveCool());
        if(Mathf.Abs(dir.x)>0 || Mathf.Abs(dir.z) > 0)
        {
            transform.position += dir;
            
        }
        ClickPoint.SetActive(false);
        InGamePlayerMove.Inst.ActiveThings();
    }

    public void Save(bool isChange)
    {
        UndoManager.Inst.SavePushBlock(transform.gameObject, prePos, isChange);
        if (isChange)
        {
            UndoManager.Inst.SavePlayerPos(Vector3.zero);
            UndoManager.Inst.SaveChangeBlock(Vector3.zero, Vector3.zero, null, null);
            UndoManager.Inst.SaveChangeBlockCool(InGameUIManager.Inst.changeCool);
            UndoManager.Inst.SaveBearAlive();
            if (UndoManager.Inst.isGoblin)
            {
                foreach(GameObject goblin in UndoManager.Inst.goblinCount)
                {
                    UndoManager.Inst.SaveGoblinPos(null, Vector3.zero, goblin.transform.rotation, false);
                }
            }
                
        }
      
    }

    public void Undo(Vector3 pos)
    {
        moveTile.transform.parent = transform;
        transform.position = pos;
        moveTile.transform.parent = null;
        RaycastHit pushBlockFloorHit;
        if (Physics.Raycast(transform.position, Vector3.down, out pushBlockFloorHit, 1.5f, PushBlockFloor))
        {
            ClickPoint.gameObject.SetActive(false);
            onFloor = true;
        }
        else
        {
            onFloor= false;
        }
    }

    public void ChangeColor(bool changeDefaultColor)
    {
        if (changeDefaultColor)
        {
            for(int i = 0; i < changeColorObj.Length; i++)
            {
                changeColorObj[i].GetComponent<MeshRenderer>().material = deafultMat[i];
            }
        }
        else
        {
            foreach (GameObject obj in changeColorObj)
            {
                obj.GetComponent<MeshRenderer>().material = invisableMat;
            }
        }
    }

    IEnumerator BlockMoveCool()
    {
        moveTile.transform.parent = transform;
        yield return new WaitForSeconds(1);
        moveTile.transform.parent = null;
    }
}
