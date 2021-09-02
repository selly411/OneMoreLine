using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static Item;

public class InGameManager : CSingletonMonoBase<InGameManager>
{
    public enum EGameEvent
    {
        GameStart,
        GameClear,
        GameFail,
        GameCountDownStart,
        GameWin
    }

    public struct GameEventArg
    {
        public EGameEvent eGameEvent;

        public GameEventArg(EGameEvent eGameEvent)
        {
            this.eGameEvent = eGameEvent;
        }
    }


    public ObservableCollection<GameEventArg> p_Event_OnGameEvent { get; private set; } = new ObservableCollection<GameEventArg>();
    public ObservableCollection<float> p_Event_OnElapseTime { get; private set; } = new ObservableCollection<float>();
    
    public GameObject p_pObjectUIManager;
    float _fElpaseTime;

    void Awake()
    {
        GameObject.Instantiate(p_pObjectUIManager);
        
        DoGameStart();
    }

    void Update()
    {
        _fElpaseTime += Time.deltaTime;
        p_Event_OnElapseTime.DoNotify(_fElpaseTime);

    }

    public void DoAddItem(itemType eItemType)
    {
        Debug.Log(nameof(DoAddItem));

        switch (eItemType)
        {
            case itemType.Star:
                Score.instance.AddScore(1000);
                break;

            case itemType.Diamond:
                Score.instance.AddScore(500);
                break;
        }
    }

    public void DoPlayNextStage()
    {
        // 현재 씬의 다음씬 로드
        // 만약에 마지막 씬이면 제작진 출력
        Debug.Log(nameof(DoPlayNextStage));

        if (SceneManager.GetActiveScene().name =="Stage01")
            SceneManager.LoadScene("Stage02");
        else
            SceneManager.LoadScene("UpCreator");
    }

    public void DoGameReplay()
    {
        Debug.Log(nameof(DoGameReplay));

        OnGameStart();

        // 현재 씬 로드
        //Debug.Log(SceneManager.GetActiveScene().name);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void DoGameStart()
    {
        Debug.Log(nameof(DoGameStart));

        OnGameStart();
    }

    public void DoGameFail()
    {
        Debug.Log(nameof(DoGameFail));
        Time.timeScale = 0f;
        p_Event_OnGameEvent.DoNotify(new GameEventArg(EGameEvent.GameFail));
    }

    public void DoGameWin()
    {
        Debug.Log(nameof(DoGameWin));
        Time.timeScale = 0f;

        p_Event_OnGameEvent.DoNotify(new GameEventArg(EGameEvent.GameWin));
    }

    public void DoGameClear()
    {
        Debug.Log(nameof(DoGameClear));
        Time.timeScale = 0f;

        p_Event_OnGameEvent.DoNotify(new GameEventArg(EGameEvent.GameClear));
    }

    public void DoCountDownStart()
    {
        Debug.Log(nameof(DoCountDownStart));
        Time.timeScale = 0f;

        p_Event_OnGameEvent.DoNotify(new GameEventArg(EGameEvent.GameCountDownStart));
    }

    private void OnGameStart()
    {
        Debug.Log(nameof(OnGameStart));

        Time.timeScale = 1f;
        _fElpaseTime = 0f;

        Score.instance.Reset();
        p_Event_OnElapseTime.DoNotify(_fElpaseTime);
        p_Event_OnGameEvent.DoNotify(new GameEventArg(EGameEvent.GameStart));
    }
}
