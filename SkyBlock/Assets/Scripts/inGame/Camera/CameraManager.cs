using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraManager : MonoBehaviour
{
    float timer = 0; //Ÿ�̸�

    [SerializeField] GameObject MainCamera;
    Vector3 SavedPosiotion;
    Quaternion SavedRotation;

    //Swipe ---
    bool SlideWait;
    Vector3 firstPos, gap;
    int slideNum;
    // ---

    [SerializeField] GameObject OptionCanvas;
    [SerializeField] GameObject HintCanvas;

    void Start()
    {
        SavedPosiotion = MainCamera.transform.position; // ���۽� ī�޶� ���� ��ġ�� ����
        SavedRotation = MainCamera.transform.rotation; // ���۽� ī�޶� ���� ȸ���� ����
    }

    private void Update()
    {
        //Ÿ�̸�
        if (timer > 0)
            timer -= Time.deltaTime;

        //--------------------------------(�������� ����)

        if (Input.GetMouseButtonDown(0) && OptionCanvas.activeSelf != true && HintCanvas.activeSelf != true || (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            SlideWait = true;
            firstPos = Input.GetMouseButtonDown(0) ? Input.mousePosition : (Vector3)Input.GetTouch(0).position;
        }

        if (Input.GetMouseButton(0) && OptionCanvas.activeSelf != true && HintCanvas.activeSelf != true || (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved))
        {
            gap = (Input.GetMouseButton(0) ? Input.mousePosition : (Vector3)Input.GetTouch(0).position) - firstPos;
            if (gap.magnitude < 100) return;
            gap.Normalize();

            if (SlideWait)
            {
                SlideWait = false;
                // Up
                if (gap.y > 0 && gap.x > -0.5f && gap.x < 0.5f)
                {
                    slideNum = 1;
                }
                // Down
                else if (gap.y < 0 && gap.x > -0.5f && gap.x < 0.5f)
                {
                    slideNum = 2;
                }
                // Left
                else if (gap.x > 0 && gap.y > -0.5f && gap.y < 0.5f)
                {
                    slideNum = 4;
                }
                // Right
                else if (gap.x < 0 && gap.y > -0.5f && gap.y < 0.5f)
                {
                    slideNum = 3;
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

        //--------------------------------(ī�޶� ȸ��)

        if (slideNum == 3 && timer <= 0)
        {
            timer = 1.2f;
            transform.DORotate(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - 90, transform.eulerAngles.z), 1.2f);
            if (SoundEffectManager.SFX != null)
                SoundEffectManager.PlaySoundEffect(10);

        }
        else if(slideNum == 4 && timer <= 0)
        {
            timer = 1.2f;
            transform.DORotate(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 90, transform.eulerAngles.z), 1.2f);
            if (SoundEffectManager.SFX != null)
                SoundEffectManager.PlaySoundEffect(10);
        }
    }
}
