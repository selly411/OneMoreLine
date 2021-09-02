using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{

    private static Score mInstance;
    private Score() { }

    // 싱글톤
     public static Score instance
     {
         get
         {
             if (mInstance == null)
             {
                 mInstance = FindObjectOfType<Score>();
                if (mInstance == null)
                {
                    GameObject container = new GameObject();
                    container.name = "ScoreContainer";
                    mInstance = container.AddComponent(typeof(Score)) as Score;
                }
             }
             return mInstance;
         }
     }
    


    // Start is called before the first frame update
    void Start()
    {
        if (this != instance)
        {
            Destroy(this);
        }
    }

    public int score
    {
        get;
        private set;
    }

    public void AddScore(int iScore)
    {
        score += iScore;
    }

    public void Reset()
    {
        score = 0;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
