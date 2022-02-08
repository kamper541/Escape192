using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleLevel : MonoBehaviour
{
        private int levelStarsNum = 0;
        public int levelIndex;

    public void PressStarButton(int _starsNum)
    {

        levelStarsNum = _starsNum;

        //MARKER ONLY Your stars number is greater than the record, you can save the new record
        //MARKER PlayerPrefs.GetInt("Lv" + levelIndex) his default value is 0
        if(levelStarsNum > PlayerPrefs.GetInt("Lv" + levelIndex))//KEY: Lv1; Value: Stars Number
        {
            PlayerPrefs.SetInt("Lv" + levelIndex, levelStarsNum);
        }

        Debug.Log("Saving Data is " + PlayerPrefs.GetInt("Lv" + levelIndex));
    }
}
