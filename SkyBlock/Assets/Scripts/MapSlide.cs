using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSlide : MonoBehaviour
{
    bool SlideWait;
    public int slideNum;
    Vector3 firstPos, gap;
    void Update()
    {
        // ������
        if (Input.GetMouseButtonDown(0) || (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            SlideWait = true;
            firstPos = Input.GetMouseButtonDown(0) ? Input.mousePosition : (Vector3)Input.GetTouch(0).position;
        }

        if (Input.GetMouseButton(0) || (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved))
        {
            gap = (Input.GetMouseButton(0) ? Input.mousePosition : (Vector3)Input.GetTouch(0).position) - firstPos;
            if (gap.magnitude < 100) return;
            gap.Normalize();

            if (SlideWait)
            {
                SlideWait = false;
                // ��
                if (gap.y > 0 && gap.x > -0.5f && gap.x < 0.5f)
                {
                    slideNum = 1;
                }

                // �Ʒ�
                else if (gap.y < 0 && gap.x > -0.5f && gap.x < 0.5f)
                {
                    slideNum = 2;
                }
                // ������
                else if (gap.x > 0 && gap.y > -0.5f && gap.y < 0.5f)
                {
                    slideNum = 3;
                }
                // ����
                else if (gap.x < 0 && gap.y > -0.5f && gap.y < 0.5f)
                {
                    slideNum = 4;
                }
                else return;
            }
        }
        else
        {
            if (slideNum != 0)
            {
                slideNum = 0;
            }
        }
    }
}
