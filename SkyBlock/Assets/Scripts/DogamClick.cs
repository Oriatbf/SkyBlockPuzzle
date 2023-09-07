using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogamClick : MonoBehaviour
{
    [SerializeField]
    private GameObject DogamUI;

    private void OnMouseDown()
    {
        if(InterectionUi.isOptionCanvas == false)
        {
            DogamUI.SetActive(true);
            InterectionUi.isDogamCanvas = true;
        }
       
    }
}
