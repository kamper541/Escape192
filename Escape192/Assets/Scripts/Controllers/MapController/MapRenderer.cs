using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MapRenderer : MonoBehaviour {
    string SceneName;


    private void Start() {
        SceneName = SceneManager.GetActiveScene().name;
        // TextAsset asset = (Resources.Load(SceneName)) as TextAsset;
        TextAsset asset = (Resources.Load("Resources/Scene/test")) as TextAsset;
        foreach(char c in asset){
            print(c);
        }
    }
}