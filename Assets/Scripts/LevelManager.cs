using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    UIManager _ui;

    private void Awake()
    {
        if (LevelManager.instance == null) instance = this;
        else Destroy(gameObject);
        
    }
    private void Start()
    {
        _ui = GetComponent<UIManager>();
    }
    public void GameOver()
    {
        if(_ui != null)
        {
            _ui.ToggleDeathPanel();
        }
    }
    public void GameWon()
    {
        if (_ui != null)
        {
            _ui.ToggleWinPanel();
        }
    }
}
