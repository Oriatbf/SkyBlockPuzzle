using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterSelectCamera : MonoBehaviour
{
    public static ChapterSelectCamera Inst;

    private Vector3 touchDownTrans, touchUpTrans;

    [SerializeField] Quaternion[] chapterInCamRot;


    [SerializeField] Vector3 dictionaryChapterPos;

    [SerializeField] Vector3[] chapterCamPos;
    [SerializeField] Vector3[] chapterInCamPos;

    private void Awake()
    {
        Inst = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        ChapterSwipe();
    }


   
    private void ChapterSwipe()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchDownTrans = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0) && !MainUIManager.Inst.uiOpen && !MapManager.Inst.isInChapter)
        {
            touchUpTrans = Input.mousePosition;
            if (touchDownTrans.x > touchUpTrans.x && Mathf.Abs(touchDownTrans.x - touchUpTrans.x) > 300)
            {
                Debug.Log("왼쪽으로 밀기");
                touchDownTrans = Vector3.zero;
                touchUpTrans = Vector3.zero;
                if (MapManager.Inst.curChapterNum < chapterCamPos.Length -1)
                {
                    MapManager.Inst.curChapterNum++;
                    transform.DOMoveX(chapterCamPos[MapManager.Inst.curChapterNum].x, 1.5f);
                    MapManager.Inst.ChangeChapterPlayer();
                }


            }

            if (touchDownTrans.x < touchUpTrans.x && Mathf.Abs(touchDownTrans.x - touchUpTrans.x) > 300)
            {
                Debug.Log("오른쪽으로 밀기");
                touchDownTrans = Vector3.zero;
                touchUpTrans = Vector3.zero;
                if (MapManager.Inst.curChapterNum > 1)
                {
                    MapManager.Inst.curChapterNum--;
                    transform.DOMoveX(chapterCamPos[MapManager.Inst.curChapterNum].x, 1.5f);
                    MapManager.Inst.ChangeChapterPlayer();
                }

            }
        }
    }

    public void ChapterInCamMove()
    {
        transform.DOMove(chapterInCamPos[MapManager.Inst.curChapterNum], 1f);
        transform.DORotateQuaternion(chapterInCamRot[MapManager.Inst.curChapterNum], 1f);
    }

    public void ChapterCamOut()
    {
        MainUIManager.Inst.mapPlayerMoving= false;
        transform.DOMove(chapterCamPos[MapManager.Inst.curChapterNum], 1f);
        transform.DORotateQuaternion(chapterInCamRot[0], 1f);
        MapManager.Inst.isInChapter= false;
    }

    public void GoDictionaryMap()
    {
        transform.DOMove(dictionaryChapterPos, 1f);
    }
}
