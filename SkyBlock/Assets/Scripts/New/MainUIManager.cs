using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUIManager : MonoBehaviour
{
    public static MainUIManager Inst;

    public bool uiOpen = false;

    public GameObject optionCanvas, dictionaryCanvas;

    [SerializeField] GameObject chapterEnterButton, stageEnterButton;

    private void Awake()
    {
        
    }

    public void OptionCanvasOpen()
    {
        optionCanvas.SetActive(true);
    }

    public void DictionaryCanvasOpen()
    {
        dictionaryCanvas.SetActive(true);
    }

    public void EnterChapter()
    {
        ChapterSelectCamera.Inst.ChapterInCamMove();
        chapterEnterButton.SetActive(false);
        stageEnterButton.SetActive(true);
    }

    public void EnterDictionaryMap()
    {
        ChapterSelectCamera.Inst.GoDictionaryMap();
    }

    public void PressStageStart()
    {
        SceneManager.LoadScene(MapManager.Inst.curChapterNum + MapManager.Inst.curStageNum);
    }




}
