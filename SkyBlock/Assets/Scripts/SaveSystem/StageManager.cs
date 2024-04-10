using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Inst;

    
    public int lastClearStage;
    public int clearStage;

    public List<bool> clearStars1 = new List<bool>();
    public List<bool> clearStars2 = new List<bool>();
    public List<bool> clearStars3 = new List<bool>();

    void Awake()
    {
        Inst = this;
        
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            StageClear(1,false, false, false);
        }

        if (Input.GetKeyDown(KeyCode.F9))
        {
            DataReset();
        }
    }

    public void DataReset()
    {
        clearStage= 0;
        lastClearStage= 0;
        clearStars1.Clear();
        clearStars2.Clear();
        clearStars3.Clear();
        DataManager.Inst.JsonSave();

    }

    public void StageClear(int takeLastClearStage, bool star1, bool star2, bool star3)
    {
        lastClearStage = takeLastClearStage;
        if (clearStage >= lastClearStage)
        {
            if(!clearStars1[lastClearStage])
                clearStars1[lastClearStage] = star1;
            if (!clearStars2[lastClearStage])
                clearStars2[lastClearStage] = star2;
            if (!clearStars3[lastClearStage])
                clearStars3[lastClearStage] = star3;
        }
        else
        {
            clearStage = lastClearStage;
            clearStars1.Add(star1);
            clearStars2.Add(star2);
            clearStars3.Add(star3);
        }
        DataManager.Inst.JsonSave();
    }

 
}
