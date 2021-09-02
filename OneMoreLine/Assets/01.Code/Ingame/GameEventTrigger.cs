using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InGameManager;

public class GameEventTrigger : MonoBehaviour
{
    public EGameEvent eGameEvent;
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController pController = other.gameObject.GetComponent<PlayerController>(); //bool 로 무적을 추가하기 planet이 없을때 무적이면리턴 게임페일에
        if (pController == null)
            return;

        switch (eGameEvent)
        {
            case EGameEvent.GameClear:
                InGameManager.instance.DoGameClear();
                break;
            case EGameEvent.GameFail:

                if(GetComponent<Planet>() != null)
                    InGameManager.instance.DoGameFail();

                if(pController.p_bUnTouchable == false)
                    InGameManager.instance.DoGameFail();

                break;
        }
    }

}
