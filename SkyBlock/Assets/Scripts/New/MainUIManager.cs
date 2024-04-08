using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUIManager : MonoBehaviour
{
    public static MainUIManager Inst;

    public bool uiOpen = false;

    public GameObject optionCanvas, dictionaryCanvas;

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
    }




}
