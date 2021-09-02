using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject _myPlayer;
    private Vector3 _VPositionOffset;

    // Start is called before the first frame update
    void Start()
    {
        _VPositionOffset = transform.position - _myPlayer.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, _myPlayer.transform.position + _VPositionOffset, Time.deltaTime * 10.0f);
        //transform.position = _myPlayer.transform.position + _VPositionOffset;
    }
}
