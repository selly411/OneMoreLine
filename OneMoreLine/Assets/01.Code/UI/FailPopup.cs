using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FailPopup : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    // Replay 버튼이 눌렸을 때
    public void OnButtonClickReplay()
    {
        InGameManager.instance.DoGameReplay();
    }


    // MainMenu 버튼이 눌렸을 때
    public void OnButtonClickMainMenu()
    {
        Debug.Log("Click");
        SceneManager.LoadScene("MainMenu");
    }

    // Quit 버튼이 눌렸을 때
    public void OnButtonClickQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

        Application.Quit();
    }

    
    
}
