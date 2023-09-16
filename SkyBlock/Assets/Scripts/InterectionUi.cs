using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterectionUi : MonoBehaviour
{
    public GameObject OptionCanvas;
    public GameObject DogamCanvas;
    public GameObject[] DogamPages;
    public GameObject DogamClose;
    public GameObject DogamBack;
    public GameObject DogamNext;
    public static bool isOptionCanvas = false;
    public static bool isDogamCanvas = false;

    public int dogamPage = 0;
    public MapButton MapButtonSc;
    public CloudeCam Clcam;

    private void Start()
    {
        DogamBack.gameObject.SetActive(false);
        OptionCanvas.SetActive(false);
        DogamCanvas.SetActive(false);

    }


    // 옵션 UI 프로그래밍
    public void optionClick()
    {
        OptionCanvas.SetActive(true);
        isOptionCanvas= true;
    }

    public void O_closeGame()
    {
        Application.Quit();
    }

    public void O_close()
    {
        OptionCanvas.SetActive(false);
        isOptionCanvas= false;
    }

    //도감 UI
    public void D_close()
    {
        DogamCanvas.SetActive(false);
        isDogamCanvas = false;
    }
    public void D_Next()
    {
        dogamPage++;
        if(dogamPage > DogamPages.Length-1)
        {
            dogamPage = DogamPages.Length-1;
        }
        D_pageCheck();
    }
    public void D_Back()
    {
        dogamPage--;
        if (dogamPage < 0)
        {
            dogamPage = 0;
        }
        D_pageCheck();
    }

    public void D_pageCheck()
    {
        for(int i = 0; i < DogamPages.Length; i++)
        {
            if(i == dogamPage)
            {
                DogamPages[i].SetActive(true);
            }
            else
            {
                DogamPages[i].SetActive(false);
            }
        }
        if(dogamPage == DogamPages.Length-1)
        {
            DogamNext.gameObject.SetActive(false);
        }
        else
        {
            DogamNext.gameObject.SetActive(true);
        }
        if(dogamPage == 0)
        {
            DogamClose.SetActive(true);
            DogamBack.SetActive(false);
        }
        else
        {
            DogamClose.SetActive(false);
            DogamBack.SetActive(true);
        }

        
    }

    public void GoDOgamIcon()
    {
        MapButtonSc.MAPNum = 1;
        MapButtonSc.MapChapterCountNum.text = "Main";
        Clcam.BackButtonClick();
    }
}
