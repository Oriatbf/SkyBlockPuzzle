using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClickpoint : MonoBehaviour
{
    [SerializeField] private LayerMask Enemy;
    [SerializeField] private GameObject player;
    public GameObject ClickPoint;
    RaycastHit hit;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
       
    }
}
