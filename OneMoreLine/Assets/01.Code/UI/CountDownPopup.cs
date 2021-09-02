using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownPopup : MonoBehaviour
{
    [DisplayName("카운트다운 하는 Text")]
    public Text pText_CountDown;

    [DisplayName("최대 카운팅 시간(초)")]
    public int iCountDownMax;

    public int intText = 0;

    float _fElpaseTime = 0.0f;

    private void OnEnable()
    {
        // CountDownMax를 UI 출력
        //pText_CountDown.text = "Timer" + pText_CountDown;
    }
    
    void Update()
    {
        // ElapseTime을 deltaTime만큼 증가
        _fElpaseTime += Time.deltaTime;

        intText = ((int)(iCountDownMax - _fElpaseTime));
        pText_CountDown.text = intText.ToString();

        if(intText < 1)
        {
            pText_CountDown.text = " ";
        }

        // CountDownMax에서 ElapseTime을 나누기 1로 한 값을 빼고 UI 출력
    }


}
