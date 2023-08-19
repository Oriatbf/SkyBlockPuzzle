using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndSceneTurnStac : MonoBehaviour
{
    public Text End_m_TurnText;
    [SerializeField] private GameObject m_player;
    public int End_T_Stac;
    // Start is called before the first frame update
    void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        EndStac();
    }

    private void EndStac()
    {
        End_T_Stac = m_player.GetComponent<Player>().TurnStac;
        End_m_TurnText.text = End_T_Stac.ToString();
    }
}
