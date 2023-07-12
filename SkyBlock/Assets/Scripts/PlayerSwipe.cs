using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwipe : MonoBehaviour
{
    [SerializeField]
    private float x1;
    [SerializeField]
    private float x2;
    [SerializeField]
    private float y1;
    [SerializeField]
    private float y2;

    public GameObject HorseOBJ;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            x1 = Input.mousePosition.x;
            y1 = Input.mousePosition.y;
        }
        if (Input.GetMouseButtonUp(0))
        {
            x2 = Input.mousePosition.x;
            y2 = Input.mousePosition.y;

            if (x1> x2)
            {
                if(Mathf.Abs(x1 -x2) > Mathf.Abs(y2 - y1))
                {
                    Debug.Log("Left");
                    if (Player.isGround && Player.MoveCoolTime <= 0)
                    {
                        transform.eulerAngles = new Vector3(0, -90, 0);
                        if (Player.horseRiding)
                        {
                            HorseOBJ.transform.eulerAngles = transform.eulerAngles;
                        }
                        MoveCooltime();
                        Player.TurnStac += 1;
                    }
                }
                    
            }

            if (x2> x1)
            {
                if (Mathf.Abs(x1 - x2) > Mathf.Abs(y2 - y1))
                {
                    Debug.Log("Right");
                    if (Player.isGround && Player.MoveCoolTime <= 0)
                    {
                        transform.eulerAngles = new Vector3(0, 90, 0);
                        if (Player.horseRiding)
                        {
                            HorseOBJ.transform.eulerAngles = transform.eulerAngles;
                        }
                        MoveCooltime();
                        Player.TurnStac += 1;
                    }
                }
                    
            }

            if(y1 > y2)
            {
                if (Mathf.Abs(x1 - x2) < Mathf.Abs(y2 - y1))
                {
                    Debug.Log("back");
                    if (Player.isGround && Player.MoveCoolTime <= 0)
                    {
                        transform.eulerAngles = new Vector3(0,180, 0);
                        if (Player.horseRiding)
                        {
                            HorseOBJ.transform.eulerAngles = transform.eulerAngles;
                        }
                        MoveCooltime();
                        Player.TurnStac += 1;
                    }
                }
                   
            }
            if (y2 > y1)
            {
                if (Mathf.Abs(x1 - x2) < Mathf.Abs(y2 - y1))
                {
                    Debug.Log("front");
                    if (Player.isGround && Player.MoveCoolTime <= 0)
                    {
                        transform.eulerAngles = new Vector3(0, 0, 0);
                        if (Player.horseRiding)
                        {
                            HorseOBJ.transform.eulerAngles = transform.eulerAngles;
                        }
                        MoveCooltime();
                        Player.TurnStac += 1;
                    }
                }
                   
            }
        }
    }

    void MoveCooltime()
    {
        Player.MoveCoolTime = Player.MoveCool;
    }
}
