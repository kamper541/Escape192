
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelFail : MonoBehaviour
{   
    public GameObject LeftStarGo;
    public GameObject MiddleStarGo;
    public GameObject RightStarGo;


    public int Score;


    private void Update()
    {
        ZeroStar();
    }
    private void ZeroStar()
    {
        LeftStarGo.gameObject.SetActive(true);
        MiddleStarGo.gameObject.SetActive(true);
        RightStarGo.gameObject.SetActive(true);
    }

}

