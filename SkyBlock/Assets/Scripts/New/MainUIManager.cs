using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUIManager : MonoBehaviour
{
    public static MainUIManager Inst;

    public bool uiOpen,mapPlayerMoving;

    public GameObject optionCanvas, dictionaryCanvas;

    [SerializeField] GameObject chapterEnterButton, stageEnterButton;

    private void Awake()
    {
        Inst= this;
        uiOpen = false;
        mapPlayerMoving = false;
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            PlayerPrefs.DeleteAll();
        }
    }

    public void OptionCanvasOpen(bool open)
    {
        optionCanvas.SetActive(open);
        uiOpen= open;
    }


    public void DictionaryCanvasOpen(bool open)
    {
        dictionaryCanvas.SetActive(open);
        uiOpen= open;
    }

    public void EnterChapter()
    {
        ChapterSelectCamera.Inst.ChapterInCamMove();
        MapManager.Inst.isInChapter= true;
        chapterEnterButton.SetActive(false);
        stageEnterButton.SetActive(true);
        mapPlayerMoving= true;
    }

    public void EnterDictionaryMap()
    {
        ChapterSelectCamera.Inst.GoDictionaryMap();
    }

    public void PressStageStart()
    {
        SceneManager.LoadScene( ++MapManager.Inst.curStageNum);
       
    }

    public void Exit()
    {
        Application.Quit();
    }

   




}
