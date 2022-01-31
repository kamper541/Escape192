using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{

    // public Level level; >> uncommend when have next level
    public UnityEngine.UI.Text TimeScoreText;
    public UnityEngine.UI.Text TimeScoreSubtext;
    public UnityEngine.UI.Text HighScoreText;
    public UnityEngine.UI.Text HighScoreSubtext;
    public UnityEngine.UI.Text ScoreText;
    public UnityEngine.UI.Image[] Stars;

    private int StarIdx = 0;
    private bool isGameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
