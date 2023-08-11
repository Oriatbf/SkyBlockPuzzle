using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudeCam : MonoBehaviour
{
    public GameObject StageGoButton;
    public MapButton stageClickButton;
    public GameObject goButton;
    public GameObject backButton;
    public Transform leftCloud;
    public Transform rightCloud;

    public MapButton MapBu;

    public bool Go = false;
    private void Start()
    {

    }

    public void GoButtonClick()
    {
        Go = true;
        leftCloud.DOMove(new Vector3(211, 143.5f, 142), 2.5f);
        rightCloud.DOMove(new Vector3(-169, 178, 142), 2.5f);

        MapBu.CameraSet();
        StageGoButton.SetActive(true);
        backButton.SetActive(true);
        goButton.SetActive(false);

        MapBu.MAPButton.transform.DOLocalMove(new Vector3(0, -250, 0), 1f);
        MapBu.StageStarCount.transform.DOLocalMove(new Vector3(0, 0, 0), 1f);
        MapBu.MapChapterCount.transform.DOLocalMove(new Vector3(0, 130, 0), 1f);
    }
    public void BackButtonClick()
    {
        Go = false;
        stageClickButton.StageGoButton.SetActive(false);
        leftCloud.DOMove(new Vector3(34, 143.5f, 142), 2.5f);
        rightCloud.DOMove(new Vector3(-32, 178,142), 2.5f);
        if (MapBu.MAPNum == 1)
        {
            transform.DOMove(new Vector3(532, 136, 348), 1f);
            transform.DORotate(new Vector3(15, 180, 0), 1f);
        }
        else if (MapBu.MAPNum == 2)
        {
            transform.DOMove(new Vector3(0, 136, 348), 1f);
            transform.DORotate(new Vector3(15, 180, 0), 1f);
        }
        else if (MapBu.MAPNum == 3)
        {
            transform.DOMove(new Vector3(-532, 136, 348), 1f);
            transform.DORotate(new Vector3(15, 180, 0), 1f);
        }
        backButton.SetActive(false);
        goButton.SetActive(true);

        MapBu.MAPButton.transform.DOLocalMove(new Vector3(0, 0, 0), 1f);
        MapBu.StageStarCount.transform.DOLocalMove(new Vector3(0, 130, 0), 1f);
        MapBu.MapChapterCount.transform.DOLocalMove(new Vector3(0, 0, 0), 1f);
    }



}
