using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleted : MonoBehaviour
{
    // public Level level; >> uncommend when have next level
    public UnityEngine.UI.Text TimeScoreText;
    public UnityEngine.UI.Text TimeScoreSubtext;
    public UnityEngine.UI.Text HighScoreText;
    public UnityEngine.UI.Text HighScoreSubtext;
    public UnityEngine.UI.Text ScoreText;
    public UnityEngine.UI.Image[] Stars;

    public enum LevelType
    {
      TIMER,
      MOVE,
      
    };

    public int Score1Star;
    public int Score2Star;
    public int Score3Star;

    protected LevelType type;

    protected int CurrentScore;

    private int StarIdx = 0;
    private bool isGameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        SetScore(CurrentScore);

        for (int i = 0; i < Stars.Length; i++)
        {
            if (i == StarIdx)
            {
                // Stars[i].enable = true;
            }else
            {
                // Stars[i].enable = false;
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetScore(int Score)
    {
        ScoreText.text = Score.ToString();
        int VisibleStar = 0;
        if (Score >= Score1Star && Score < Score2Star) {
            VisibleStar = 1;
        }else if (Score >= Score2Star && Score < Score3Star) {
            VisibleStar = 2;
        }else if (Score >= Score3Star) {
            VisibleStar = 3;
        }

        for (int i =0;i<Stars.Length; i++) {
            if (i==VisibleStar) {
                // Stars[i].enable = true;
            }
            else{
                // Stars[i].enable = false;
            }
        }

      StarIdx = VisibleStar;  
    }

    public void SetHighScore(int HighScore)
    {
        HighScoreText.text = HighScore.ToString(); 
    }
    public void SetTimeScore(int TimeScore)
    {

    }
    public void SetTimeScore(string TimeScore)
    {
        TimeScoreText.text = TimeScore;
    }

    // public void SetLevel()  *** this Func for Scoring with Time and Codepar to Save and go next Level
    // {
    //     if (type == Level.LevelType.MOVE)
    //     {
    //         Codepar Scoring 
    //         HighScoreSubtext.text = "High Score" ;
    //     }else if(type == Level.LevelType.TIMER) {
    //         TimeScoreSubtext.text = "uesd time";
    //         HighScoreSubtext = "High Score";
    //     }
    //     {
            
    //     }
    // }

    public void OnGameWin(int Score)
    {
        isGameOver = true;
    }
    public void OnGameLose()
    {
        isGameOver = true;
    }
}
