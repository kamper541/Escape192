using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject mapSelectionPanel;
    public GameObject[] levelSelectionPanels;

    [Header("Our STAR UI")]
    public int stars;
    public Text startText;
    public MapSelection[] mapSelections;
    public Text[] questStarsTexts;
    public Text[] lockedStarsTexts;
    public Text[] unlockStarsTexts;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this; 
        }
        else
        {
            if(instance != this)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }


    private void Update()//TODO REmove this method because we don't want to call these methods each frame
    {
        UpdateStarUI();
        UpdateLockedStarUI();
        UpdateUnLockedStarUI();
    }


    private void UpdateLockedStarUI()
    {
        for(int i = 0; i < mapSelections.Length; i++)
        {
            questStarsTexts[i].text = mapSelections[i].questNum.ToString();

            if (mapSelections[i].isUnlock == false)//If one of the Map is locked
            {
                lockedStarsTexts[i].text = stars.ToString() + "/" + mapSelections[i].endLevel * 3;
            }
        }
    }

    private void UpdateUnLockedStarUI()//TODO FIXED LATER this method
    {
        for(int i = 0; i < mapSelections.Length; i++)
        {
            unlockStarsTexts[i].text = stars.ToString() + "/" + mapSelections[i].endLevel * 3;
        }
    }

    
    private void UpdateStarUI()
    {
        startText.text = stars.ToString();
    }
    

    public void PressMapButton(int _mapIndex)
    {
        if(mapSelections[_mapIndex].isUnlock == true)//You can open this map
        {
            levelSelectionPanels[_mapIndex].gameObject.SetActive(true);
            mapSelectionPanel.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("You cannot open this map now. Please work hard to collect more stars");
        }
    }
}
