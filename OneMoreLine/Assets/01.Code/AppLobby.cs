using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AppLobby : MonoBehaviour
{
    public Text _textStage;
    private int _iCurrentStage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickPlay()
    {
        SceneManager.LoadScene("Stage01");
    }
   
}
