using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Newtonsoft.Json.Linq;

public class WebController : MonoBehaviour
{
    // Start is called before the first frame update
    UniWebView webView;

    [SerializeField]
    RectTransform myUITransfrom;

    public Button yourButton;

    static bool Begin = false;
    static private JEnumerable<JToken> Inside_Payload = new JEnumerable<JToken>();
    static private JEnumerable<JToken> Variable_Map = new JEnumerable<JToken>();

    public bool HasPayload = false;

    //-----Get, Set------

    public static JEnumerable<JToken> Get_JToken(){
        return Inside_Payload;
    }

    public static JEnumerable<JToken> Get_VariblesMap(){
        return Variable_Map;
    }

    public static void Set_Begin(){
        Begin = false;
    }

    public static bool Get_Begin(){
        return Begin;
    }

    public bool is_display = false;

    //-----Start Here-----

    void Start()
    {

        Button btn = yourButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);

        var webviewGameObject = new GameObject("UniWebView");
        // url = Path.Combine(Application.streamingAssetsPath, "SampleVideo_1280x720_5mb.mp4");
        // Debug.Log(Application.streamingAssetsPath);
        is_display = false;
        webView = webviewGameObject.AddComponent<UniWebView>();
        webView.Frame = new Rect(0 , 0 , Screen.width , Screen.height);
        webView.ReferenceRectTransform = myUITransfrom;
        // webView.Load("https://www.google.com");

        var indexURL = UniWebViewHelper.StreamingAssetURLForPath("webPage/public/index.html");
        var accessURL = UniWebViewHelper.StreamingAssetURLForPath("/");
        // webView.Load("https://edo-controller.web.app");
        webView.Load(indexURL , true);
        // webView.Load("localhost:5000");
        webView.CleanCache();
        UniWebView.ClearCookies();
        webView.AddUrlScheme("code");
        webView.Show();
        webView.OnPageFinished += (view , statusCode , url) => {
            if(!is_display){
            is_display = true;
            string stg_name = SceneManager.GetActiveScene().name;
            // print("last " + stg_name[stg_name.Length - 1]);
            // if((stg_name[stg_name.Length - 1] == '1') || (stg_name[stg_name.Length - 1] == '2' )){
            //     try{
            //         webView.EvaluateJavaScript("display1();", (payload) => {
            //         });
            //     }catch(Exception e){
            //         print(e);
            //     }
            // }
            // else{
            //     try{
            //         webView.EvaluateJavaScript("display2();", (payload) => {
            //         });
            //     }catch(Exception e){
            //         print(e);
            //     }
            // }
            webView.EvaluateJavaScript("displayAll();", (payload) => {

            });
        }
        };
        webView.OnShouldClose += (view) => {
            webView = null;
            return true;
        };
    }

    void TaskOnClick(){
        try{
            webView.EvaluateJavaScript("defined();", (payload)=>{
            if (payload.resultCode.Equals("0"))
            {
                try
                {
                    //new
                    Debug.Log("Clicked!");
                    JObject o = JObject.Parse(payload.data);
                    JEnumerable<JToken> jt = o["payload"].Children();
                    // JEnumerable<JToken> map = jt["Map"].Children();
                    // JEnumerable<JToken> pl = jt["code"].Children();
                    JEnumerable<JToken> map = new JEnumerable<JToken>();
                    JEnumerable<JToken> code = new JEnumerable<JToken>();
                    
                    
                    foreach(JToken token in jt){
                        if((string)token["name"] == "code"){
                            code = token["code"].Children(); 
                        }else if((string)token["name"] == "var_map"){
                            map = token["map"].Children();
                        }
                    }

                    Inside_Payload = code;
                    Variable_Map = map;
                    // trigger here
                    Begin = true;
                    // print(Begin);
                }
                catch(Exception e)
                {
                    print(e);
                }
            }
            else
            {
                Debug.Log("Something goes wrong: " + payload.data);
            }
            });
        }catch(Exception error){
            print(error);
        }
    }

    // Update is called once per frame
    void Update()
    {

        // if(LevelLoader.closing == true)
        // {
        //     // print("WebView is Closing");
        //     webView.Hide();
        //     // Destroy(this);
        // }

        
    }

    void FixedUpdate() {
        // if(webView == null){
        //     return;
        // }
        // webView.OnMessageReceived += (view, message) => {
        //     JObject o = JObject.Parse(message.Path);
        //     JEnumerable<JToken> jt = o["payload"].Children();
        //     Inside_Payload = jt;
        //     Begin = true;
        // };
        // Begin = false;
    }

    

}
