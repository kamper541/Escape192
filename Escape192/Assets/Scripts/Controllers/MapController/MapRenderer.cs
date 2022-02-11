using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;

public class MapRenderer : MonoBehaviour
{
    //TODO: Add when have player.
    [SerializeField]
    GameObject Player;

    [SerializeField]
    public Button StartButton;
    GameObject PreFab;
    // Transform PreFab = new Transform();

    int PlayerZPosition;
    int PlayerXPosition;
    char PlayerFacing;
    string SceneName;
    string FolderName;
    bool Validating = false;

    List<List<string>> MatrixOfMapToCheck = new List<List<string>>();

    List<List<List<string>>> ModelLayers = new List<List<List<string>>>();
    List<List<List<string>>> DesignLayers = new List<List<List<string>>>();
    JEnumerable<JToken> jt = new JEnumerable<JToken>();


    private void Start()
    {

        // Button btn = StartButton.GetComponent<Button>();
        // btn.onClick.AddListener(TaskOnClick);

        // Test
        PlayerFacing = 'F';
        //

        SceneName = SceneManager.GetActiveScene().name;
        // TextAsset asset = (Resources.Load("MapDesign/MapsModel/test1")) as TextAsset;
        // TextAsset DesignAsset = (Resources.Load("MapDesign/MapsDecoration/dectest1")) as TextAsset;

        TextAsset asset = (Resources.Load($"MapDesign/MapsModel/{SceneName}")) as TextAsset;
        string TextFile = asset.ToString();
        string[] Text = TextFile.Split("\n"[0]);
        uint width = uint.Parse(Text[0]);
        uint height = uint.Parse(Text[1]);
        uint layer = uint.Parse(Text[2]);
        Text = Text.Skip(3).ToArray();
        string MapInfo = string.Join("\n", Text);
        Text = MapInfo.Split("\n"[0]);
        CreateMapMatrix(layer, height, width, Text, ref ModelLayers);


        TextAsset DesignAsset = (Resources.Load($"MapDesign/MapsDecoration/{SceneName}")) as TextAsset;
        string TextFileForDesign = DesignAsset.ToString();
        Text = TextFileForDesign.Split("\n"[0]);
        FolderName = Text[0];
        width = uint.Parse(Text[1]);
        height = uint.Parse(Text[2]);
        layer = uint.Parse(Text[3]);
        Text = Text.Skip(4).ToArray();
        MapInfo = string.Join("\n", Text);
        Text = MapInfo.Split("\n"[0]);
        CreateMapMatrix(layer, height, width, Text, ref DesignLayers);

        //TODO: change if there is more than one layer
        MatrixOfMapToCheck = ModelLayers[0];
        Render(layer, height, width);
    }

    private void CreateMapMatrix(uint layer, uint height, uint width, string[] text, ref List<List<List<string>>> Layers)
    {
        int CurrentHeight = 0;
        for (int i = 0; i < layer; i++)
        {
            List<List<string>> Row = new List<List<string>>();
            for (int j = 0; j < height; j++)
            {
                List<string> Column = new List<string>();
                if (text[CurrentHeight].Length == 1)
                {
                    Column.Add(text[CurrentHeight]);
                }
                else
                {
                    string[] InRowModel = text[CurrentHeight].Split(","[0]);
                    for (int k = 0; k < width; k++)
                    {
                        Column.Add(InRowModel[k]);
                    }
                }
                CurrentHeight++;
                Column.Reverse();
                Row.Add(Column);
            }
            Row.Reverse();
            Layers.Add(Row);
        }
    }

    private void Update()
    {
        if (WebController.Get_Begin() && !Validating)
        {
            jt = WebController.Get_JToken();
            Validate(jt);
        }
    }

    void Render(uint layer, uint height, uint width)
    {
        for (int i = 0; i < layer; i++)
        {
            for (int j = 0; j < height; j++)
            {
                for (int k = 0; k < width; k++)
                {
                    if (DesignLayers[i][j][k] == "P")
                    {
                        PreFab = (GameObject)Resources.Load($"PlayerSkin/{DesignLayers[i][j][k]}");
                        Instantiate(PreFab, new Vector3(j * 2, i + 1, k * 2), Quaternion.identity);
                        PlayerZPosition = k;
                        PlayerXPosition = j;
                    }
                    else if (DesignLayers[i][j][k] == "G")
                    {
                        PreFab = (GameObject)Resources.Load($"MapDesign/MapResources/{FolderName}/{DesignLayers[i][j][k]}");
                        Instantiate(PreFab, new Vector3(j * 2, i + 1, k * 2), Quaternion.Euler (0f, 90f, 0f));
                    }
                    else if (DesignLayers[i][j][k] == "W")
                    {
                        PreFab = (GameObject)Resources.Load($"MapDesign/MapResources/{FolderName}/{DesignLayers[i][j][k]}");
                        Instantiate(PreFab, new Vector3(j * 2, i + (float)1.1, k * 2), Quaternion.Euler (90f, 0f, 0f));
                    }
                    else if (DesignLayers[i][j][k] != "0")
                    {
                        PreFab = (GameObject)Resources.Load($"MapDesign/MapResources/{FolderName}/{DesignLayers[i][j][k]}");
                        Instantiate(PreFab, new Vector3(j * 2, i + 1, k * 2), transform.rotation);
                    }
                }
            }
        }
    }

    void Validate(JEnumerable<JToken> jt)
    {
        Validating = true;
        // print(PlayerXPosition + ',' + PlayerZPosition);
        Debug.Log("Validating");
        CheckCode(jt);
        // Validating = false;
        if (MatrixOfMapToCheck[PlayerXPosition][PlayerZPosition] == "G")
        {
            print("Win");
            Listener.Win();
        }
        else
        {
            print("GameOver you didn't reach the goal");
            Listener.lose();
        }
    }

    void CheckCode(JEnumerable<JToken> jt)
    {
        foreach (JToken token in jt)
        {
            if ((string)token["name"] == "move")
            {
                int steps = (int)token["value"];
                switch (PlayerFacing)
                {
                    case 'F': PlayerXPosition += steps; break;
                    case 'B': PlayerXPosition -= steps; break;
                    case 'L': PlayerZPosition += steps; break;
                    case 'R': PlayerZPosition -= steps; break;
                }
            }
            else if ((string)token["name"] == "turn")
            {
                if ((string)token["value"] == "left")
                {
                    switch (PlayerFacing)
                    {
                        case 'F': PlayerFacing = 'L'; break;
                        case 'B': PlayerFacing = 'R'; break;
                        case 'L': PlayerFacing = 'B'; break;
                        case 'R': PlayerFacing = 'F'; break;
                    }
                }
                else if ((string)token["value"] == "right")
                {
                    switch (PlayerFacing)
                    {
                        case 'F': PlayerFacing = 'R'; break;
                        case 'B': PlayerFacing = 'L'; break;
                        case 'L': PlayerFacing = 'F'; break;
                        case 'R': PlayerFacing = 'B'; break;
                    }
                }
            }
            else if ((string)token["name"] == "jump")
            {
                //TODO: support the jump and update the player pos after jump.
                // layer++;
            }
            else if ((string)token["name"] == "repeat")
            {
                JEnumerable<JToken> InRepeat = token["do"].Children();
                for (int i = 0; i < (int)(token["time"]); i++)
                {
                    CheckCode(InRepeat);
                }
            }
            //TODO: Detect fall
            else if (MatrixOfMapToCheck[PlayerXPosition][PlayerZPosition] == "0")
            {
                print("GameOver you step on 0");
                Listener.lose();
            }
        }
    }
}