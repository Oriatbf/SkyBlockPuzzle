using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterectionUi : MonoBehaviour
{
    public GameObject OptionCanvas;
    public Image DogamIcon;
    public GameObject DogamCanvas;
    public GameObject[] DogamPages;
    public GameObject DogamClose;
    public GameObject DogamBack;
    public GameObject DogamNext;
    public Sprite GoMain;
    public Sprite GoDogam;
    public bool isDogamScene = false;
    public static bool isOptionCanvas = false;
    public static bool isDogamCanvas = false;
    public Text DogamText;

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
        if (SoundEffectManager.SFX != null)
            SoundEffectManager.PlaySoundEffect(11);
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
        if (SoundEffectManager.SFX != null)
            SoundEffectManager.PlaySoundEffect(12);
        DogamCanvas.SetActive(false);
        dogamPage = 0;
        for (int i = 1; i < DogamPages.Length; i++)
        {     
            DogamPages[i].SetActive(false);
        }
        DogamPages[0].SetActive(true);
        DogamBack.SetActive(false);
        DogamNext.gameObject.SetActive(true);
        isDogamCanvas = false;
    }
    public void D_Next()
    {
        if (SoundEffectManager.SFX != null)
            SoundEffectManager.PlaySoundEffect(12);
        dogamPage++;
        if(dogamPage > DogamPages.Length-1)
        {
            dogamPage = DogamPages.Length-1;
        }
        D_pageCheck();
    }
    public void D_Back()
    {
        if (SoundEffectManager.SFX != null)
            SoundEffectManager.PlaySoundEffect(12);
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
          
            DogamBack.SetActive(true);
        }

        
    }

    public void GoDOgamIcon()
    {
        if (!isDogamScene)
        {
            MapButtonSc.MAPNum = 1;
            MapButtonSc.MapChapterCountNum.text = "Main";
            Clcam.BackButtonClick();
            DogamIcon.sprite = GoMain;
            isDogamScene = true;
            DogamText.text = "맵 선택";
        }
        else
        {
            MapButtonSc.MAPNum = 5;
            MapButtonSc.MapChapterCountNum.text = "Stage 1";
            Clcam.BackButtonClick();
            DogamIcon.sprite = GoDogam;
            isDogamScene = false;
            DogamText.text = "도감";
        }
       
    }
    
}
