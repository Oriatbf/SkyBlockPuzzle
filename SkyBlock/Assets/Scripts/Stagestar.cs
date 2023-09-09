using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Stagestar : MonoBehaviour
{
    public Text Stage;
    public GameObject player;
    private int currentStage;
    public Image[] Stars;
    public Sprite yesStar;
    public Sprite noStar;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
    }
    // Update is called once per frame
    void Update()
    {
        currentStage = player.GetComponent<MapSelectMove>().playerStagePos ;

        Stage.text = "1-" +(currentStage+1).ToString();

        if (StageManager.instance.clearStars1[currentStage])
            Stars[0].sprite= yesStar;
        else
        {
            Stars[0].sprite = noStar;
        }

        if (StageManager.instance.clearStars2[currentStage])
            Stars[1].sprite = yesStar;
        else
        {
            Stars[1].sprite = noStar;
        }

        if (StageManager.instance.clearStars3[currentStage])
            Stars[2].sprite = yesStar;
        else
        {
            Stars[2].sprite = noStar;
        }

    }
}
