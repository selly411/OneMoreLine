using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ScoreGUI
public class IdleFrame : MonoBehaviour
{
    public UnityEngine.UI.Text pTextElapsTime;

    private Text mText;


    // Start is called before the first frame update
    void Start()
    {
        mText = GetComponentInChildren<Text>();
        //        InGameManager.instance.p_Event_OnElapseTime.Subscribe += OnElapseTime;
    }

    private void OnElapseTime(float fElapseTime)
    {
        pTextElapsTime.text = ((int)fElapseTime).ToString();
    }

    // Update is called once per frame
    void Update()
    {



        
          // Score 를 불러오는 코드
          // 현재 오류남. 지금 UIManager 씬에서 아래의 코드를 불러오는데
          // InGame씬으로 넘어갈 때 어디선가에서 Score를 부르는 코드가 있어서 오류 생김
        int score = Score.instance.score;
        string scoreAddZero = score.ToString();

        if (mText)
            mText.text = "Score:" + scoreAddZero;
        else
        {
            Debug.Log("########## 1 (error)mText null");
            mText = GetComponent<Text>();
            if (mText)
            {
                Debug.Log("########## 2 (error)mText ok");
            }
            else
            {
                Debug.Log("########## 2 (error)mText null");
            }
        }


    


    }
}