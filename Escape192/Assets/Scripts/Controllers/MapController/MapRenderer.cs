using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MapRenderer : MonoBehaviour
{
    string SceneName;
    uint width;
    uint height;

    uint layer;


    private void Start()
    {
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
                    Column.Add(Text[j][k]);
                }
                Row.Add(Column);
            }
            Layers.Add(Row);
        }

        print(Layers[0][1][3]);
        // print((string)((ArrayList)Layers[0])[1]);

        // string[] layers = MapInfo.Split("~"[0]);
        // print(layers[0]);
        // foreach (string layer in layers)
        // {
        //     print(layer);
        // }
    }
}