using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageLock : MonoBehaviour
{
    public GameObject[] StageLocker;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0;i< StageManager.instance.clearStage; i++)
        {
            StageLocker[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //StageManager.instance.StageClear(currentPlayerStage, true, takeStar, turnClear);
    }
   
}
