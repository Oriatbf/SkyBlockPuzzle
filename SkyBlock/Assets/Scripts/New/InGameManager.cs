using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    public static InGameManager Inst;

    public int curStageNum;

    public int endTurn,curTurn;

    public bool getStar,isPlayerOnTurn,isTutorial,gameEnd;

    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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
        StageManager.Inst.StageClear(curStageNum,true,getStar,isPlayerOnTurn);
    }

    private void Awake()
    {
        Inst = this;
    }

    public void SpidersMove()
    {
        if (UndoManager.Inst.isSpider )
        {

            GameObject[] spiders = GameObject.FindGameObjectsWithTag("Spider");
            if (spiders.Length > 0)
            {
                foreach (GameObject spider in spiders)
                {
                    spider.GetComponent<Spider_E>().Move();
                }
            }
            else
            {

                InGamePlayerMove.Inst.ActiveThings();
                UndoManager.Inst.isSpider = false;
            }
          
        }
      
        
    }

    public void GoblinsMove()
    {
        if (UndoManager.Inst.isGoblin)
        {
            GameObject[] goblins = GameObject.FindGameObjectsWithTag("Goblin");
            if(goblins.Length > 0)
            {
                foreach (GameObject goblin in goblins)
                {
                    goblin.GetComponent<Goblin_E>().GoblinMove();
                }
            }
            else
            {
                UndoManager.Inst.isGoblin= false;
                InGamePlayerMove.Inst.ActiveThings();
            }
           
        }
    }

    public void ArrowTowerTurn()
    {
        if(UndoManager.Inst.isArrowTower)
        {
            GameObject[] arrowTowers = GameObject.FindGameObjectsWithTag("ArrowTower");
            foreach (GameObject towers in arrowTowers)
            {
                towers.GetComponent<ArrowTower>().Action();
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
        player.GetComponent<Animator>().SetTrigger("Die");
        yield return new WaitForSeconds(2);
        InGameUIManager.Inst.OpenEndCanvas(false);
        yield return new WaitForSeconds(5f);
       
    }
}
