using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameUIManager : MonoBehaviour
{
    public static InGameUIManager Inst;
   [HideInInspector]public bool isUIOpen;
   [HideInInspector] public bool isChanging;

    [Header("블럭 바꾸기 관련")]
    [SerializeField] GameObject changeCancle;
    [SerializeField] Image changeCoolImage;
    [SerializeField] Text changeCoolText;
    public int changeCool;
    GameObject firstBlock, secondBlock;
    public LayerMask blockLayer;

    [Header("다른 캔버스 관련")]
    [SerializeField] GameObject optionCanvas;
    [SerializeField] GameObject hintCanvas;
    [SerializeField] GameObject endCanvas;

    [Header("게임 진행 관련")]
    [SerializeField] Text goalTurn;
    [SerializeField] Text curTurn;

    [Header("클리어 캔버스 관련")]
    [SerializeField] Text endCanvasCurTurn;
    [SerializeField] Text endCanvasGoalTurn;
    [SerializeField] Text onGoalTurnTxt,outGoalTurnText,titleTxt;
    [SerializeField] GameObject getStarImage, noStarImage,onTurnImage,outTurnImage,onClearImage,nonClearImage,nextStageBtn;




    private void Awake()
    {
        Inst = this;
        optionCanvas.SetActive(false);
        endCanvas.SetActive(false);
        hintCanvas.SetActive(false);
        
    }

    public void OpenOption(bool uiOpen)
    {
        optionCanvas.SetActive(uiOpen);
        isUIOpen = uiOpen;

    }

    private void Update()
    {
        UiUpdate();

        if (isChanging)
        {
            ClickBlock();
        }
    }

    private void UiUpdate()
    {
        goalTurn.text = InGameManager.Inst.endTurn.ToString();
        curTurn.text = InGameManager.Inst.curTurn.ToString();
    }

    private void ClickBlock()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, blockLayer))
            {
                if (firstBlock == null && secondBlock != hit.transform.gameObject)
                {
                    firstBlock = hit.transform.gameObject;
                    firstBlock.GetComponent<Block>().Click();
                }
                else if (secondBlock == null && firstBlock != hit.transform.gameObject)
                {
                    secondBlock = hit.transform.gameObject;
                    secondBlock.GetComponent<Block>().Click();
                }
                else
                {
                    if (firstBlock == hit.transform.gameObject)
                    {
                        firstBlock.GetComponent<Block>().UnClick();
                        firstBlock = null;
                    }
                    else if (secondBlock == hit.transform.gameObject)
                    {
                        secondBlock.GetComponent<Block>().UnClick();
                        secondBlock = null;
                    }
                }
            }
        }
    }

    public void ClickChangeButton()
    {
        if (!isChanging && changeCool ==0)
        {
            isChanging = true;
            changeCancle.SetActive(true);
            isUIOpen = true;
            InGamePlayerMove.Inst.DisableMoveTile();
        }
        else if(changeCool ==0 && firstBlock != null && secondBlock!= null)
        {
            ChangeFinsh();
        }

    }

    private void ChangeFinsh()
    {
        ChangeBlock();
        changeCancle.SetActive(false);
        changeCoolImage.enabled = true;
        changeCoolText.enabled = true;
        isChanging = false;
        isUIOpen = false;
        changeCool = 3;
        changeCoolImage.fillAmount = 1;
        changeCoolText.text = changeCool.ToString();
    }

    public void UndoChangeCoolDown()
    {
        if (changeCool > 0)
        {
            changeCoolImage.fillAmount = (float)changeCool / 3;
            changeCoolText.text = changeCool.ToString();
        }
        else if (changeCool == 0)
        {    
            changeCoolImage.fillAmount = 0;
            changeCoolText.text = "";
        }
    }

    public void ChangeCoolDown()
    {
        if(changeCool > 1)
        {
            changeCool--;
            changeCoolImage.fillAmount = (float)changeCool/3;
            changeCoolText.text = changeCool.ToString();
        }
        else if(changeCool == 1)
        {
            changeCool--;
            changeCoolImage.fillAmount = 0;
            changeCoolText.text = "";
        }
      
    }

    private void ChangeBlock()
    {
        SoundEffectManager.PlaySoundEffect(7);
        Vector3 firstPos = firstBlock.transform.position;
        Vector3 secPos = secondBlock.transform.position;
        Instantiate(ParticleManager.Particles[0], firstPos + new Vector3(0,1,0), Quaternion.identity);
        Instantiate(ParticleManager.Particles[0], secPos + new Vector3(0, 1, 0), Quaternion.identity);
        Save(firstPos, secPos);
        firstBlock.transform.position = secPos;
        secondBlock.transform.position = firstPos;
        firstBlock.GetComponent<Block>().UnClick();
        secondBlock.GetComponent<Block>().UnClick();
        firstBlock = null;
        secondBlock = null;
        InGamePlayerMove.Inst.ActiveThings();
    }

    private void Save(Vector3 firstPos, Vector3 secPos)
    {
        UndoManager.Inst.SaveChangeBlock(firstPos, secPos, firstBlock, secondBlock);
        UndoManager.Inst.SavePlayerPos(Vector3.zero);
        UndoManager.Inst.SaveBearAlive();

        if (UndoManager.Inst.isGoblin)
        {
            foreach (GameObject goblin in UndoManager.Inst.goblinCount)
            {
                UndoManager.Inst.SaveGoblinPos(null, Vector3.zero, goblin.transform.rotation, false);
            }
        }
            
        if (UndoManager.Inst.isPushBlock)
        {
            GameObject[] pushBlock = GameObject.FindGameObjectsWithTag("PushBlock");
            foreach (GameObject box in pushBlock)
                box.GetComponent<PushBlock>().Save(false);
        }
        SaveChangeCool();

    }

    public void SaveChangeCool()
    {
        UndoManager.Inst.SaveChangeBlockCool(changeCool);
    }

    public void ClickChangeCancle()
    {
        SoundEffectManager.PlaySoundEffect(6);
        isChanging = false;
        isUIOpen= false;
        changeCancle.SetActive(false);
        changeCoolImage.fillAmount = 0;
        changeCoolText.text = "";
        if(firstBlock != null)
        {
            firstBlock.GetComponent<Block>().UnClick();
            firstBlock = null;
        }

        if(secondBlock!= null)
        {
            secondBlock.GetComponent<Block>().UnClick();
            secondBlock = null;
        }
        InGamePlayerMove.Inst.ActiveThings();

    }

    public void OpenHint(bool uiOpen)
    {
        hintCanvas.SetActive(uiOpen);
        isUIOpen = uiOpen;
    }

    public void OpenEndCanvas(bool isPlayerAlive)
    {
        endCanvas.SetActive(true);
        endCanvasCurTurn.text = InGameManager.Inst.curTurn.ToString();
        endCanvasGoalTurn.text = InGameManager.Inst.endTurn.ToString();
        onGoalTurnTxt.text = InGameManager.Inst.endTurn.ToString();
        outGoalTurnText.text = InGameManager.Inst.endTurn.ToString();

        getStarImage.SetActive(InGameManager.Inst.getStar);
        noStarImage.SetActive(!InGameManager.Inst.getStar);
        onTurnImage.SetActive(InGameManager.Inst.isPlayerOnTurn);
        outTurnImage.SetActive(!InGameManager.Inst.isPlayerOnTurn);
        onClearImage.SetActive(true);
        nonClearImage.SetActive(false);

        if (!isPlayerAlive)
        {
            onTurnImage.SetActive(false);
            outTurnImage.SetActive(true);
            getStarImage.SetActive(false);
            noStarImage.SetActive(true);
            onClearImage.SetActive(false);
            nonClearImage.SetActive(true);
            nextStageBtn.SetActive(false);
        }
        else
        {
            InGameManager.Inst.SaveStageData();
        }

        string stageNum = "";
        if (MapManager.Inst.curChapterNum > 1)
            stageNum = (MapManager.Inst.curStageNum - 12).ToString();
        else
            stageNum = (MapManager.Inst.curStageNum).ToString();
        titleTxt.text = MapManager.Inst.curChapterNum.ToString() + " - " + stageNum + " 스테이지";

        
    }

    public void RestartScene()
    {
        int a = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(a);
    }

    public void NextScene()
    {
        int a = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(a+1);
    }

    public void MapSelectScene()
    {
        SceneManager.LoadScene("NewChapterSelect");
    }


}
