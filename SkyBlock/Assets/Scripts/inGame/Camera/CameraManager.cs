using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraManager : MonoBehaviour
{
    float timer = 0; //타이머

    [SerializeField] GameObject MainCamera;
    Vector3 SavedPosiotion;
    Quaternion SavedRotation;

    //Swipe ---
    bool Zoom = false;
    bool SlideWait;
    Vector3 firstPos, gap;
    int slideNum;
    // ---

    void Start()
    {
        SavedPosiotion = MainCamera.transform.position; // 시작시 카메라 현재 위치값 저장
        SavedRotation = MainCamera.transform.rotation; // 시작시 카메라 현재 회전값 저장
    }

    private void Update()
    {
        //타이머
        if (timer > 0)
            timer -= Time.deltaTime;

        //--------------------------------(카메라 탑다운)

        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.tag == "Player" && timer <= 0 && Zoom == false)
                {
                    Zoom = true;
                    timer = 1.5f;

                    MainCamera.transform.DOMove(transform.position, 1.5f);
                    MainCamera.transform.DORotate(new Vector3(90, MainCamera.transform.rotation.y, MainCamera.transform.rotation.z), 1.5f);
                }

                if (hit.transform.gameObject.tag == "Player" && timer <= 0 && Zoom == true)
                {
                    Zoom = false;
                    timer = 1.5f;

                    MainCamera.transform.DOMove(SavedPosiotion, 1.5f);
                    MainCamera.transform.DORotate(SavedRotation.eulerAngles, 1.5f);
                }
            }
        }

        //--------------------------------(스와이프 감지)

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

        //--------------------------------(카메라 회전)

        if (slideNum == 3 && timer <= 0)
        {
            timer = 1.2f;
            transform.DORotate(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - 90, transform.eulerAngles.z), 1.2f);

        }
        else if(slideNum == 4 && timer <= 0)
        {
            timer = 1.2f;
            transform.DORotate(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 90, transform.eulerAngles.z), 1.2f);
        }
    }
}
