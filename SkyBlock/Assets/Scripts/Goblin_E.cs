using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_E : MonoBehaviour
{
    [SerializeField] private GameObject colli;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject Red;

    [SerializeField] private float AttackNum;

    private bool attackON = true;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        if(Player.TurnStac % AttackNum == 0 && Player.TurnStac != 0 && attackON)
        {
            Attack();
        }

        if (Player.TurnStac % AttackNum == AttackNum - 1 && Player.TurnStac != 0)
        {
            attackON = true;
            Red.gameObject.SetActive(true);
        }
    }

    private void Attack()
    {
        Red.gameObject.SetActive(false);
        attackON = false;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && Player.TurnStac % AttackNum == 0 && Player.TurnStac != 0)
        {
            Destroy(other.gameObject);
        }
    }
}
