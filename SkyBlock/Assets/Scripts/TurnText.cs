using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnText : MonoBehaviour
{
    public GameObject HintCanvas;
    public Text m_TurnText;
    [SerializeField]private  GameObject player;
    public static float T_Stac;
    // Start is called before the first frame update
    void Start()
    { 
        player = GameObject.FindGameObjectWithTag("Player");
       
    }

    // Update is called once per frame
    void Update()
    {
        T_Stac = Player.TurnStac;
        m_TurnText.text = T_Stac.ToString();
    }

    public void HintUIClick()
    {
        HintCanvas.SetActive(true);
    }
    public void HintUIClose()
    {
        HintCanvas.SetActive(false);
    }
}
