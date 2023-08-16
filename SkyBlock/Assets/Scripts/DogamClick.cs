using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogamClick : MonoBehaviour
{
    [SerializeField]
    private GameObject DogamUI;

    private void OnMouseDown()
    {
        DogamUI.SetActive(true);
    }
}
