using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterSelectCamera : MonoBehaviour
{
    public static ChapterSelectCamera Inst;

    private Vector3 touchDownTrans, touchUpTrans;

    [SerializeField] Quaternion[] chapterInCamRot;

    [SerializeField] int curChapterNum;

    [SerializeField] Vector3 defaultCamRot;

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
        if (Input.GetMouseButtonDown(0))
        {
            touchDownTrans = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            touchUpTrans = Input.mousePosition;
            if (touchDownTrans.x > touchUpTrans.x && Mathf.Abs(touchDownTrans.x - touchUpTrans.x) > 300)
            {
                Debug.Log("왼쪽으로 밀기");
                touchDownTrans = Vector3.zero;
                touchUpTrans = Vector3.zero;
                if(curChapterNum < chapterCamPos.Length-1)
                {
                    curChapterNum++;
                    transform.DOMoveX(chapterCamPos[curChapterNum].x, 1.5f);
                }
                
                
            }

            if (touchDownTrans.x < touchUpTrans.x && Mathf.Abs(touchDownTrans.x - touchUpTrans.x) > 300)
            {
                Debug.Log("오른쪽으로 밀기");
                touchDownTrans = Vector3.zero;
                touchUpTrans = Vector3.zero;
                if(curChapterNum > 0)
                {
                    curChapterNum--;
                    transform.DOMoveX(chapterCamPos[curChapterNum].x, 1.5f);
                }
               
            }
        }
    }

    public void ChapterInCamMove()
    {
        transform.DOMove(chapterInCamPos[curChapterNum], 1f);
        transform.DORotateQuaternion(chapterInCamRot[curChapterNum], 1f);
    }
}
