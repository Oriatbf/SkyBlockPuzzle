using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowGoblin : MonoBehaviour
{
    public GameObject[] AttackCollider;
    public GameObject player;
    private int ColliderNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < AttackCollider.Length; i++)
        {
            AttackCollider[i].gameObject.SetActive(false);
        }
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x > player.transform.position.x) //왼쪽 판정
        {
            if(Mathf.Abs(transform.position.x) + Mathf.Abs(player.transform.position.x) < 8 )
            {
                if (transform.position.z > player.transform.position.z)
                {
                    transform.eulerAngles = new Vector3(0, 180, 0);
                    ColliderNum = 2;
                    turnCollider();
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    ColliderNum = 3;
                    turnCollider();
                }
              
            }
            else
            {
                transform.eulerAngles = new Vector3(0, -90, 0);
                ColliderNum = 0;
                turnCollider();
            }
           
        }
        else if(transform.position.x < player.transform.position.x) //오른쪽 판정
        {
            if (Mathf.Abs(transform.position.x) + Mathf.Abs(player.transform.position.x) < 8)
            {
                if (transform.position.z > player.transform.position.z)
                {
                    transform.eulerAngles = new Vector3(0, 180, 0);
                    ColliderNum = 2;
                    turnCollider();
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    ColliderNum = 3;
                    turnCollider();
                }

            }
            else
            {
                transform.eulerAngles = new Vector3(0, 90, 0);
                ColliderNum = 1;
                turnCollider();
            }
            
        }
      
       
    }

    private void turnCollider()
    {
        for(int i = 0;i< AttackCollider.Length; i++)
        {
            if (i == ColliderNum)
            {
                AttackCollider[i].gameObject.SetActive(true);
            }
            else
            {
                AttackCollider[i].gameObject.SetActive(false);
            }
        }
    }
}
