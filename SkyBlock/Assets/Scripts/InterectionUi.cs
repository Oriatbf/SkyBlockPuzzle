using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterectionUi : MonoBehaviour
{
    public GameObject OptionCanvas;

    private void Start()
    {
        OptionCanvas.SetActive(false);
    }
    // 옵션 UI 프로그래밍
    public void optionClick()
    {
        OptionCanvas.SetActive(true);
    }

    public void O_closeGame()
    {
        Application.Quit();
    }

    public void O_close()
    {
        OptionCanvas.SetActive(false);
    }


}
