using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VictoryPopup : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Next Stage 버튼이 눌렸을 때
    public void OnNextStageButtonClick()
    {
        InGameManager.instance.DoPlayNextStage();
    }
}
