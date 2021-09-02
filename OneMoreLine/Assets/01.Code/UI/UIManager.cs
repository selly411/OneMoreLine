using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    private static UIManager mInstance;
    public GameObject _VictoryPopup;
    public GameObject _FailPopup;
    public GameObject _CountDownPopup;



    //인스턴스를 반환하는 함수(모든소스코드에서 호출 가능)
    public static UIManager instance
    {
        get
        {
            //인스턴스가 참조되었는지 검사
            if (mInstance == null)
            {
                //인스턴스를 찾고 그 인스턴스를 참조
                mInstance = FindObjectOfType<UIManager>();
            }
            return mInstance;
        }
    }

    void Start () {
        //인스턴스가 이 클래스 자신이 아니면 삭제
        if (this != instance)
        {
            Destroy(this);
        }

        InGameManager.instance.p_Event_OnGameEvent.Subscribe_And_Listen_CurrentData += OnGameEvent;
    }
    // Update is called once per frame
    void Update()
    {
        // 스페이스바가 눌리면, 승리창
        //if (Input.GetButtonDown("Jump"))
        //{
        // //   OnFailPopup();
        //    OnVictoryPopup();
        //    Debug.Log("####### GetButtonDown - When pressed Jump");
        //}
    }

    private void OnGameEvent(InGameManager.GameEventArg obj)
    {
        switch (obj.eGameEvent)
        {
            case InGameManager.EGameEvent.GameStart:
                 _CountDownPopup.SetActive(true);
                 _VictoryPopup.SetActive(false);
                 _FailPopup.SetActive(false);
                break;

            case InGameManager.EGameEvent.GameClear:
                OnVictoryPopup();
                break;

            case InGameManager.EGameEvent.GameFail:
                OnFailPopup();
                break;
        }
    }

    public void OnVictoryPopup()
    {


        Debug.Log("########################");
        _VictoryPopup.SetActive(true);
        _FailPopup.SetActive(false);
    }

    public void OnFailPopup()
    {
        _VictoryPopup.SetActive(false);
        _FailPopup.SetActive(true);
    }
}
