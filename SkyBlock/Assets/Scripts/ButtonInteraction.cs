using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInteraction : MonoBehaviour
{
    public Sprite[] buttonSprites;
    [SerializeField]
    private int SpriteNumber = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject.Find("Interaction").GetComponent<Image>().sprite = buttonSprites[SpriteNumber];
        if (Player.isPushBlock)
        {
            SpriteNumber = 2;
        }
        else if (Player.isFrontEnemy)
        {
            SpriteNumber = 1;
        }
        else
        {
            SpriteNumber= 0;
        }
    }
}
