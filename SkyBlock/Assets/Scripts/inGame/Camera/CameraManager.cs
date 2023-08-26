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
    bool Zoom = false;

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

        //--------------------------------(카메라 회전)

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(timer <= 0)
            {
                timer = 1;
                transform.DORotate(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 90, transform.eulerAngles.z), 2f);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (timer <= 0)
            {
                timer = 1;
                transform.DORotate(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - 90, transform.eulerAngles.z), 2f);
            }
        }

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
                    timer = 2;

                    MainCamera.transform.DOMove(transform.position, 2f);
                    MainCamera.transform.DORotate(new Vector3(90, MainCamera.transform.rotation.y, MainCamera.transform.rotation.z), 2f);
                }

                if (hit.transform.gameObject.tag == "Player" && timer <= 0 && Zoom == true)
                {
                    Zoom = false;
                    timer = 2;

                    MainCamera.transform.DOMove(SavedPosiotion, 2f);
                    MainCamera.transform.DORotate(SavedRotation.eulerAngles, 2f);
                }
            }
        }
    }
}
