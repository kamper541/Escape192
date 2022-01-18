using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;

public class Listener : MonoBehaviour
{
    JObject o = new JObject();

    private bool Activated = false;

    private static bool to_move = false;

    private static bool to_turn = false;

    private static bool to_jump = false;
    
    private static bool to_repeat = false;
    
    private static bool lastbox = false;

    private static float repeat_time = 0;

    private static int num_block;

    private static float steps;

    private static float degree;

    public static int get_num_block(){
        return num_block;
    }

    public static float get_steps(){
        return steps;
    }

    public static float get_degree(){
        return degree;
    }

    public static bool get_to_move(){
        return to_move;
    }

    public static void toggle_to_move(){
        to_move = false;
    }

    public static bool get_to_turn(){
        return to_turn;
    }

    public static void toggle_to_turn(){
        to_turn = false;
    }

    public static bool get_to_jump(){
        return to_jump;
    }

    public static void toggle_to_jump(){
        to_jump = false;
    }

    //<--- get set

    // Start is called before the first frame update
    void Start()
    {
        // print("Listening..");
        num_block = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(!Activated)
        {
            if(WebController.Get_Begin())
            {
                Activated = true;
                try
                {
                    JEnumerable<JToken> jt = WebController.Get_JToken();
                    // foreach(JToken token in jt)
                    // {
                    //     print(token);  
                    // }
                    StartCoroutine(Reading_Block(jt));
                }
                catch(Exception e)
                {
                    print(e);
                }
            }
        }
        else
        {

        }
    }


    public IEnumerator Reading_Block(JEnumerable<JToken> jt_get){
        print("start reading");
        StartCoroutine(Unpacking(jt_get));
        yield return new WaitWhile(() => lastbox == false);
        // Invoke("call_dead",2.5f);
        print("Activated");
        WebController.Set_Begin();
        Activated = false;
        print("stop reading");
    }

    public IEnumerator Unpacking(JEnumerable<JToken> jt_get){
        // print("unpacking");
        foreach(JToken token in jt_get ){
            // print((string)token["name"]);
            if((string)token["name"] == "move"){
                float val = (float)token["value"];
                steps = val;
                num_block ++;
                to_move = true;
                yield return new WaitWhile(() => to_move == true);
                steps = 0;
            }
            else if((string)token["name"] == "turn"){
                string val = (string)token["value"];
                if(val == "left")
                {
                    degree = (float)(-90.0);
                }
                else if(val == "right")
                {
                    degree = (float)90.0;
                }
                to_turn = true;
                yield return new WaitWhile(() => to_turn == true);
                degree = 0;
            }
            else if((string)token["name"] == "jump"){
                to_jump = true;
                yield return new WaitWhile(() => to_jump == true);
                steps = 1;
                to_move = true;
                yield return new WaitWhile(() => to_move == true);
                steps = 0;
            }
            else if((string)token["name"] == "repeat"){
                JEnumerable<JToken> jt = token["do"].Children();
                for(int i = 0 ; i < (int)(token["time"]) ; i++){
                    StartCoroutine(Unpacking(jt));
                    to_repeat = true;
                    yield return new WaitWhile(() => to_repeat == true);
                }
            }
            else if((string)token["name"] == "last"){
                lastbox = true;
            }
            num_block ++;
        }
        to_repeat = false;

    }
    void call_dead(){
        // Wait_Dead.dead_or_not = true;
    }

    
}
