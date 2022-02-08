using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScoringManager : MonoBehaviour
{
    [Header("Our Star UI")]
    public bool isShowStars = false;
    public GameObject LeftStarGo;
    public GameObject MiddleStarGo;
    public GameObject RightStarGo;

    [Header("Our Score UI")]
    public int Score;
    public Text ScoreText;


    private void Update()
    {
        UpdateScoreUI();
        UpdateStarStatus();
    }

    private void UpdateStarStatus()
    {
        if(Score > 10 && Score <=20)
        {
            LeftStarGo.gameObject.SetActive(false);
            MiddleStarGo.gameObject.SetActive(true);
            RightStarGo.gameObject.SetActive(true);

        }
        
        else if(Score > 20 && Score <=30)

        {
            LeftStarGo.gameObject.SetActive(false);
            MiddleStarGo.gameObject.SetActive(false);
            RightStarGo.gameObject.SetActive(true);

        }else if(Score > 30)

        {
            LeftStarGo.gameObject.SetActive(false);
            MiddleStarGo.gameObject.SetActive(false);
            RightStarGo.gameObject.SetActive(false);

        }else
        {
            LeftStarGo.gameObject.SetActive(true);
            MiddleStarGo.gameObject.SetActive(true);
            RightStarGo.gameObject.SetActive(true);
        }

    }

    private void UpdateScoreUI()
    {
        ScoreText.text = Score.ToString();
    }
}
