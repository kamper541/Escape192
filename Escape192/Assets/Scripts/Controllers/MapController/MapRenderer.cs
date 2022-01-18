using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Newtonsoft.Json.Linq;

public class MapRenderer : MonoBehaviour
{
    //TODO: Add when have player.
    [SerializeField]
    GameObject Player;

    uint PlayerXPosition;
    uint PlayerYPosition;
    char PlayerFacing;
    string SceneName;
    uint width;
    uint height;
    uint layer;


    private void Start()
    {
        // Test
        PlayerFacing = 'F';
        //

        SceneName = SceneManager.GetActiveScene().name;
        // TextAsset asset = (Resources.Load(SceneName)) as TextAsset;
        TextAsset asset = (Resources.Load("Scene/test")) as TextAsset;
        string TextFile = asset.ToString();

        string[] Text = TextFile.Split("\n"[0]);
        width = uint.Parse(Text[0]);
        height = uint.Parse(Text[1]);
        layer = uint.Parse(Text[2]);
        // width * height
        Text = Text.Skip(3).ToArray();
        string MapInfo = string.Join("\n", Text);
        Text = MapInfo.Split("\n"[0]);
        // TODO: Deal With ArrayList!!!
        List<List<List<char>>> Layers = new List<List<List<char>>>();
        for (int i = 0 ; i < layer ; i ++){
            List<List<char>> Row = new List<List<char>>();
            for (int j = 0 ; j < height ; j ++){
                List<char> Column = new List<char>();
                for (int k = 0 ; k < width ; k ++){
                    if(Text[j][k] == 'P'){
                        PlayerXPosition = (uint)k;
                        PlayerYPosition = (uint)j;
                    }
                    Column.Add(Text[j][k]);
                }
                Column.Reverse();
                Row.Add(Column);
            }
            Row.Reverse();
            Layers.Add(Row);
        }

        print(Layers[1][0][2]);
    }
}