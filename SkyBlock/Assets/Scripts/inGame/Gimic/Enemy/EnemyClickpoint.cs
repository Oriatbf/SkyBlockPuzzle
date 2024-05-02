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
        if (Input.GetMouseButtonDown(0) && ClickPoint.gameObject.activeSelf)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, Enemy))
            {
                
                Vector3 lookDirection = transform.position - player.transform.position;
                lookDirection.y = 0f;
                player.transform.rotation = Quaternion.LookRotation(lookDirection);
                
                hit.transform.GetComponent<EnemyClickpoint>().ClickPoint.SetActive(false);

            }
        }
    }
}
