using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        if (PlayerPrefs.HasKey("color")) SceneManager.LoadScene("LevelSelection");
        else 
        { 
            PlayerPrefs.SetString("color", "rrrrrrrrrr");
            SceneManager.LoadScene("LevelSelection");
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
