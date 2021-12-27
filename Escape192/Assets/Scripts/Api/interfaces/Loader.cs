using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;

public interface SceneLoader
{
    void MainMenu();
    void ExitGame();
    void OpenSettings();
    void NextScene();
    void PreviousScene();
    void ReloadScene();
}

public interface MapLoader
{
    bool VerifyMap();
    void GenerateMap();
}

public interface UILoader
{
    string GetTime();

    string GetScore();
}