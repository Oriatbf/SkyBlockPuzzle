using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;

    public int lastClearStage;
    public int clearStage;
    public List<bool> clearStars1 = new List<bool>();
    public List<bool> clearStars2 = new List<bool>();
    public List<bool> clearStars3 = new List<bool>();

    void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            StageClear(false, false, false);
        }
    }

    void StageClear(bool star1, bool star2, bool star3)
    {
        if (clearStage >= lastClearStage)
        {
            clearStars1[lastClearStage] = star1;
            clearStars2[lastClearStage] = star2;
            clearStars3[lastClearStage] = star3;
        }
        else
        {
            clearStage = lastClearStage;
            clearStars1.Add(star1);
            clearStars2.Add(star2);
            clearStars3.Add(star3);
        }
    }
}
