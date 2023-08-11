using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scroll : MonoBehaviour
{
    public Slider slider;
    public RectTransform[] buttonRect;
    public RectTransform[] btnImageRect;
    public MapButton MapBu;
    // Start is called before the first frame update
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i< btnImageRect.Length; i++)
        {
            Vector3 BtnTargetPos = buttonRect[i].anchoredPosition3D;
            btnImageRect[i].anchoredPosition = Vector3.Lerp(btnImageRect[i].anchoredPosition3D, BtnTargetPos, 0.25f);
        }
        if (Input.GetKey(KeyCode.F1) || MapBu.MAPNum == 1)     //슬라이드시 아이콘,바 크기변경 **코드 간결화필요**
        {
            slider.value = Mathf.Lerp(slider.value, 0f, 0.05f);
            buttonRect[0].sizeDelta = new Vector2(360, buttonRect[0].sizeDelta.y);
            buttonRect[1].sizeDelta = new Vector2(180, buttonRect[1].sizeDelta.y);
            buttonRect[2].sizeDelta = new Vector2(180, buttonRect[2].sizeDelta.y);
            buttonRect[3].sizeDelta = new Vector2(180, buttonRect[3].sizeDelta.y);
            btnImageRect[0].sizeDelta = new Vector2(180, 180);
            btnImageRect[1].sizeDelta = new Vector2(120, 120);
            btnImageRect[2].sizeDelta = new Vector2(120, 120);
            btnImageRect[3].sizeDelta = new Vector2(120, 120);

        }
        if (Input.GetKey(KeyCode.F2)  || MapBu.MAPNum == 2)
        {
            slider.value = Mathf.Lerp(slider.value, 0.25f, 0.05f);
            buttonRect[0].sizeDelta = new Vector2(180, buttonRect[0].sizeDelta.y);
            buttonRect[1].sizeDelta = new Vector2(360, buttonRect[1].sizeDelta.y);
            buttonRect[2].sizeDelta = new Vector2(180, buttonRect[2].sizeDelta.y);
            buttonRect[3].sizeDelta = new Vector2(180, buttonRect[3].sizeDelta.y);
            btnImageRect[0].sizeDelta = new Vector2(120, 120);
            btnImageRect[1].sizeDelta = new Vector2(180, 180);
            btnImageRect[2].sizeDelta = new Vector2(120, 120);
            btnImageRect[3].sizeDelta = new Vector2(120, 120);

        }
        if (Input.GetKey(KeyCode.F3) || MapBu.MAPNum == 3)
        {
            slider.value = Mathf.Lerp(slider.value, 0.5f, 0.05f);
            buttonRect[0].sizeDelta = new Vector2(180, buttonRect[0].sizeDelta.y);
            buttonRect[1].sizeDelta = new Vector2(180, buttonRect[1].sizeDelta.y);
            buttonRect[2].sizeDelta = new Vector2(360, buttonRect[2].sizeDelta.y);
            buttonRect[3].sizeDelta = new Vector2(180, buttonRect[3].sizeDelta.y);
            btnImageRect[0].sizeDelta = new Vector2(120, 120);
            btnImageRect[1].sizeDelta = new Vector2(120, 120);
            btnImageRect[2].sizeDelta = new Vector2(180, 180);
            btnImageRect[3].sizeDelta = new Vector2(120, 120);

        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            buttonRect[0].sizeDelta = new Vector2(180, buttonRect[0].sizeDelta.y);
            buttonRect[1].sizeDelta = new Vector2(180, buttonRect[1].sizeDelta.y);
            buttonRect[2].sizeDelta = new Vector2(180, buttonRect[2].sizeDelta.y);
            buttonRect[3].sizeDelta = new Vector2(360, buttonRect[3].sizeDelta.y);

        }
    }
}
