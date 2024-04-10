using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIManager : MonoBehaviour
{
    public static InGameUIManager Inst;
   [HideInInspector]public bool isUIOpen;
   [HideInInspector] public bool isChanging;

    [Header("블럭 바꾸기 관련")]
    [SerializeField] GameObject changeCancle;
    [SerializeField] Image changeCoolImage;
    [SerializeField] Text changeCoolText;
    int changeCool;
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
        Vector3 firstPos = firstBlock.transform.position;
        Vector3 secPos = secondBlock.transform.position;
        UndoManager.Inst.SaveChangeBlock(firstPos, secPos,firstBlock, secondBlock);
        UndoManager.Inst.SavePlayerPos(Vector3.zero);
        firstBlock.transform.position = secPos;
        secondBlock.transform.position = firstPos;
        firstBlock.GetComponent<Block>().UnClick();
        secondBlock.GetComponent<Block>().UnClick();
        firstBlock = null;
        secondBlock = null;
        InGamePlayerMove.Inst.ActiveThings();
    }

    public void ClickChangeCancle()
    {
        isChanging= false;
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
        
    
    }

    public void OpenHint(bool uiOpen)
    {
        hintCanvas.SetActive(uiOpen);
        isUIOpen = uiOpen;
    }

    public void OpenEndCanvas()
    {
        endCanvas.SetActive(true);
        endCanvasCurTurn.text = InGameManager.Inst.curTurn.ToString();
        endCanvasGoalTurn.text = InGameManager.Inst.endTurn.ToString();
    }


}
