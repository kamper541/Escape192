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
    uint width;
    uint height;
    uint layer;
    string FolderName;
    bool Validating = false;

    List<List<char>> MatrixOfMapToCheck = new List<List<char>>();

    List<List<List<char>>> ModelLayers = new List<List<List<char>>>();
    List<List<List<char>>> DesignLayers = new List<List<List<char>>>();
    JEnumerable<JToken> jt = new JEnumerable<JToken>();


    private void Start()
    {

        // Button btn = StartButton.GetComponent<Button>();
        // btn.onClick.AddListener(TaskOnClick);

        // Test
        PlayerFacing = 'F';
        PlayerZPosition = 2;
        PlayerXPosition = 0;
        //

        SceneName = SceneManager.GetActiveScene().name;
        // TextAsset asset = (Resources.Load(SceneName)) as TextAsset;
        TextAsset asset = (Resources.Load("Scene/test1")) as TextAsset;
        TextAsset DesignAsset = (Resources.Load("Scene/dectest1")) as TextAsset;

        string TextFile = asset.ToString();
        string TextFileForDesign = DesignAsset.ToString();

        string[] Text = TextFile.Split("\n"[0]);
        string[] TextDesign = TextFileForDesign.Split("\n"[0]);

        FolderName = Text[0];
        width = uint.Parse(Text[1]);
        height = uint.Parse(Text[2]);
        layer = uint.Parse(Text[3]);
        // width * height
        Text = Text.Skip(4).ToArray();
        string MapInfo = string.Join("\n", Text);
        Text = MapInfo.Split("\n"[0]);
        // TODO: Deal With ArrayList!!!
        for (int i = 0; i < layer; i++)
        {
            List<List<char>> Row = new List<List<char>>();
            List<List<char>> DesignRow = new List<List<char>>();
            for (int j = 0; j < height; j++)
            {
                List<char> Column = new List<char>();
                List<char> DesignColumn = new List<char>();
                for (int k = 0; k < width; k++)
                {
                    Column.Add(Text[j][k]);
                    DesignColumn.Add(TextDesign[j][k]);
                }
                Column.Reverse();
                DesignColumn.Reverse();
                Row.Add(Column);
                DesignRow.Add(DesignColumn);
            }
            Row.Reverse();
            DesignRow.Reverse();
            ModelLayers.Add(Row);
            DesignLayers.Add(DesignRow);
        }
        //TODO: change if there is more than one layer
        MatrixOfMapToCheck = ModelLayers[0];
        Render();
    }

    private void Update()
    {
        if (WebController.Get_Begin() && !Validating)
        {
            jt = WebController.Get_JToken();
            Validate(jt);
        }
    }

    // void TaskOnClick()
    // {
    //     Debug.Log(PlayerXPosition + "," + PlayerZPosition);
    //     StartCoroutine(Validate(jt));
    // }

    void Render()
    {
        // Transform PreFab2 = Resources.Load($"MapDesign/Forest/{FolderName}/G2");
        int LastPrefab = 1;

        // First Layer
        for (int i = 0; i < height; i++)
        {
            int TogglePrefab = 0;
            for (int j = 0; j < width; j++)
            {
                if (j == 0)
                {
                    TogglePrefab = LastPrefab == 1 ? 0 : 1;
                    LastPrefab = TogglePrefab;
                }
                else
                {
                    int temp = TogglePrefab;
                    TogglePrefab = temp == 1 ? 0 : 1;
                }
                PreFab = (GameObject)Resources.Load($"MapDesign/{FolderName}/Ground{TogglePrefab.ToString()}");
                Instantiate(PreFab, new Vector3(i * 2, 0, j * 2), Quaternion.identity);
            }

        }

        // Second Layer
        for (int i = 0; i < layer; i++)
        {
            for (int j = 0; j < height; j++)
            {
                for (int k = 0; k < width; k++)
                {
                    if((char)DesignLayers[i][j][k] == 'P'){
                        PreFab = (GameObject)Resources.Load($"PlayerSkin/{DesignLayers[i][j][k]}");
                        Instantiate(PreFab, new Vector3(j * 2, i + 1, k * 2), Quaternion.identity);
                    }
                    else if ((char)DesignLayers[i][j][k] != '0')
                    {
                        PreFab = (GameObject)Resources.Load($"MapDesign/{FolderName}/{DesignLayers[i][j][k]}");
                        Instantiate(PreFab, new Vector3(j * 2, i + 1, k * 2), Quaternion.identity);
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
        if ((char)MatrixOfMapToCheck[PlayerXPosition][PlayerZPosition] == 'G')
        {
            print("Win");
        }
        else
        {
            print("GameOver you didn't reach the goal");
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
            else if ((char)MatrixOfMapToCheck[PlayerXPosition][PlayerZPosition] == '0')
            {
                print("GameOver you step on 0");
            }
        }
    }
}