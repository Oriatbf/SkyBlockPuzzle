using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public static Tutorial instance;

    [SerializeField] private Transform[] tutorialPos;
    [SerializeField] private GameObject[] tutorials;
    [SerializeField] private int tutorialCount;


    void Awake()
    {
        instance = this;
    }

    public void TutorialPlayPos(Vector3 playerPos)
    {
        if(tutorialCount < tutorials.Length)
        {
            if (Vector3.Distance(tutorialPos[tutorialCount].position, playerPos) < 0.3f)
            {
                tutorials[tutorialCount].SetActive(true);
                tutorialCount++;
            }
        }
    }

    public void TutorialPlay()
    {
        if (tutorialCount < tutorials.Length)
        {
            tutorials[tutorialCount].SetActive(true);
            tutorialCount++;
        }
    }
}
