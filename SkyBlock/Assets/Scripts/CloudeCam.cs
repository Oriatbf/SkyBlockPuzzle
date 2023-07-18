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
        //transform.DOMove(new Vector3(44, 70, 147), 2.5f);
        //transform.DORotate(new Vector3(11, 173, 0), 2.5f);
        StageGoButton.SetActive(true);
        backButton.SetActive(true);
        goButton.SetActive(false);
    }
    public void BackButtonClick()
    {
        Go = false;
        stageClickButton.StageGoButton.SetActive(false);
        leftCloud.DOMove(new Vector3(34, 143.5f, 142), 2.5f);
        rightCloud.DOMove(new Vector3(-32, 178,142), 2.5f);
        transform.DOMove(new Vector3(4, 399, 349), 2.5f);
        transform.DORotate(new Vector3(52, 180, 0), 2.5f);
        backButton.SetActive(false);
        goButton.SetActive(true);
    }



}
