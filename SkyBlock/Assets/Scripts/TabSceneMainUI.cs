using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabSceneMainUI : MonoBehaviour
{
    public GameObject MapSelectCanvas;
    public GameObject[] MapsIcon;
    public GameObject[] RealMaps;
    public GameObject nextRightMapbutton;
    public GameObject nextLeftMapbutton;
    public int mapNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openMap()
    {
        MapSelectCanvas.SetActive(true);
    }

    public void nextMapRight()
    {
        mapNum += 1;
        if(mapNum >= MapsIcon.Length)
        {
            mapNum = MapsIcon.Length-1;
        }
        else
        {
            for (int i = 0; i < MapsIcon.Length; i++)
            {
                if (i == mapNum)
                {
                    MapsIcon[i].gameObject.SetActive(true);
                }
                else
                {
                    MapsIcon[i].gameObject.SetActive(false);
                }
            }
        } 
    }

    public void nextMapLeft()
    {
        mapNum -= 1;
        if(mapNum < 0)
        {
            mapNum= 0;
        }
        else
        {
            for (int i = 0; i < MapsIcon.Length; i++)
            {
                if (i == mapNum)
                {
                    MapsIcon[i].gameObject.SetActive(true);
                }
                else
                {
                    MapsIcon[i].gameObject.SetActive(false);
                }
            }
        } 
    }

    public void MapSelect()
    {
        for (int i = 0; i < MapsIcon.Length; i++)
        {
            if (i == mapNum)
            {
                RealMaps[i].gameObject.SetActive(true);
            }
            else
            {
                RealMaps[i].gameObject.SetActive(false);
            }
        }
        MapSelectCanvas.SetActive(false);
    }
}
