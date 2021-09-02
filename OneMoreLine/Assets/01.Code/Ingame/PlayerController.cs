using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CObjectBase
{
    public bool p_bUnTouchable { get { return _pPlanet != null; } }

    [DisplayName("감지 거리")]
    public float p_fDetect = 10f;

    [DisplayName("플레이어 속도")]
    public float p_fPlayerSpeed;

    [GetComponent]
    Rigidbody2D _pRigidbody = null;

    Collider2D[] _arrCollider = new Collider2D[10];
    Planet _pPlanet;

    InGameManager _inGameManager;

    Vector2 _playerPosition;


    protected override void OnStart()
    {
        base.OnStart();
        _playerPosition = transform.position;
        InGameManager.instance.p_Event_OnGameEvent.Subscribe += OnGameEvent;
        _pRigidbody.AddForce(transform.up * p_fPlayerSpeed, ForceMode2D.Impulse);

    }

    private void OnGameEvent(InGameManager.GameEventArg obj)
    {
        if(obj.eGameEvent == InGameManager.EGameEvent.GameStart)
        {
            transform.position = _playerPosition;
            PlanetIsNull();
        }
    }

    public override void OnUpdate(float fTimeScale_Individual)
    {
        base.OnUpdate(fTimeScale_Individual);

        if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            _pPlanet = GetNearestObstacle_OrNUll();
            if (_pPlanet != null)
            {
                DoConnectPlanet(_pPlanet);
                transform.rotation = Quaternion.Euler(transform.position - _pPlanet.transform.position);
            }
        }

        if(Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.Space))
        {
            if(_pPlanet)
            {
                PlanetIsNull();
            }
        }

        Vector2 vecVelocity = _pRigidbody.velocity;
        if (vecVelocity.magnitude < p_fPlayerSpeed)
            _pRigidbody.velocity = vecVelocity.normalized * p_fPlayerSpeed;

        transform.up = _pRigidbody.velocity;
    }

    public void DoConnectPlanet(Planet pPlanet)
    {
        pPlanet.p_pDistanceJoint.connectedBody = _pRigidbody;
        if(transform.position.x > pPlanet.transform.position.x)
        {
            pPlanet.p_pDistanceJoint.anchor = new Vector2(0.3f, 0f);
        }

        if(transform.position.x < pPlanet.transform.position.x)
        {
            pPlanet.p_pDistanceJoint.anchor = new Vector2(-0.3f, 0f);
        }
    }

    //가장 가까운 곳에 있는 Obstacle 리턴
    private Planet GetNearestObstacle_OrNUll()
    {
        Vector2 vecPos = transform.position;
        float fDistanceClosest = float.MaxValue;
        Planet pPlanetClosest = null;

        int iHitCount = Physics2D.OverlapCircleNonAlloc(transform.position, 10f, _arrCollider);
        for(int i = 0; i < iHitCount; i++)
        {
            Planet pPlanet = _arrCollider[i].GetComponent<Planet>();
            if(pPlanet)
            {
                float fDistnace = Vector2.Distance(vecPos, pPlanet.transform.position);
                if(fDistnace < fDistanceClosest)
                {
                    fDistanceClosest = fDistnace;
                    pPlanetClosest = pPlanet;
                }
            }
        }

        return pPlanetClosest;
    }

    private void PlanetIsNull()
    {
        if(_pPlanet != null)
        {
            _pPlanet.p_pDistanceJoint.connectedBody = null;
            _pPlanet = null;
        }
    }
}