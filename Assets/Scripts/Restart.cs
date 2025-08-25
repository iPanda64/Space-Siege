using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public void OpenLevelScene()
    {
        SceneManager.LoadScene("Level_" + PlayerPrefs.GetInt("lastLevel"));
    }
    public void OpenNextLevelScene()
    {
        if(PlayerPrefs.GetInt("lastLevel")!=10) SceneManager.LoadScene("Level_" + (int)(PlayerPrefs.GetInt("lastLevel")+1));
        else SceneManager.LoadScene("Level_1");
    }
    public void OpenLevelSelectorScene()
    {
        SceneManager.LoadScene("LevelSelection");
    }
    public void OpenMainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
