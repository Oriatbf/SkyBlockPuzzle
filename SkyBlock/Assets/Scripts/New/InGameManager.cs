using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    public static InGameManager Inst;

    public int curStageNum;

    public int endTurn,curTurn;

    public bool getStar,isPlayerOnTurn;

    private void Start()
    {
        
    }

    private void Update()
    {
        if(endTurn >= curTurn)
            isPlayerOnTurn= true;
        else
            isPlayerOnTurn= false;
    }

    public void SaveStageData()
    {
        //StageManager.Inst.StageClear(curStageNum,true,getStar,isPlayerOnTurn);
    }

    private void Awake()
    {
        Inst = this;
    }

    public void GoblinsMove()
    {
        if (UndoManager.Inst.isGoblin)
        {
            GameObject[] goblins = GameObject.FindGameObjectsWithTag("Goblin");
            foreach(GameObject goblin in goblins) 
            {
                goblin.GetComponent<Goblin_E>().GoblinMove();
            }
        }
    }

    public void playerGoal()
    {
        StartCoroutine(GoalDelay());
    }
    IEnumerator GoalDelay()
    {
        yield return new WaitForSeconds(0.2f);
        if (ParticleManager.Particles != null)
            Instantiate(ParticleManager.Particles[3], transform.position + new Vector3(0, -1.2f, 0.5f), ParticleManager.Particles[3].transform.rotation);
        yield return new WaitForSeconds(1f);
        if (SoundEffectManager.SFX != null)
            SoundEffectManager.PlaySoundEffect(9);
        yield return new WaitForSeconds(0.5f);
        InGameUIManager.Inst.OpenEndCanvas(true);
    }


    public void playerLose()
    {
        StartCoroutine(playerDie());
    }

    IEnumerator playerDie()
    {
        yield return new WaitForSeconds(1);
        InGameUIManager.Inst.OpenEndCanvas(false);
        yield return new WaitForSeconds(5f);
       
    }
}
