using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public int level;
    public Text levelText;
    public Image image;
    //"r"=72 "g"=67
    void Start()
    {
        if (PlayerPrefs.GetString("color").ToCharArray()[level-1] =='g') image.color = Color.green;
        else image.color = Color.red;
        levelText.text=level.ToString();
    }

    public void OpenScene() 
    { 
        SceneManager.LoadScene("Level_"+level.ToString()); 
    }
    public void ChangeColor()
    {
        string tempString = PlayerPrefs.GetString("color");
        if (tempString[level - 1] == 'r') image.color = Color.red;
        else
            image.color = Color.green;
    }
}
