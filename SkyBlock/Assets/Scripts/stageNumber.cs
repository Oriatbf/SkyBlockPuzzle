using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stageNumber : MonoBehaviour
{
    public int stageNum;
    public static stageNumber instance;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CurrentStageNum(int playerStage)
    {
        stageNum = playerStage;
    }
}
