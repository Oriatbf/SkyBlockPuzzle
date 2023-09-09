using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnText : MonoBehaviour
{
    public GameObject HintCanvas;
    public Text m_TurnText;
    public Text[] endStac;
    [SerializeField]private  GameObject player;
    public int TurnStac;
    private int endTurn;
    // Start is called before the first frame update
    void Start()
    { 
        player = GameObject.FindGameObjectWithTag("Player");
        
        endTurn = player.GetComponent<Player>().EndTurn;
        for(int i = 0; i < endStac.Length; i++)
        {
            endStac[i].text = player.GetComponent<Player>().EndTurn.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        TurnStac = player.GetComponent<Player>().TurnStac;
        m_TurnText.text = TurnStac.ToString() + "/"+ endTurn.ToString();
        if(TurnStac > endTurn)
        {
            m_TurnText.color = Color.red;
        }
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
