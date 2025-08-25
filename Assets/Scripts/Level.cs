using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.VisualScripting.Member;
using static UnityEngine.EventSystems.EventTrigger;

public class Level : MonoBehaviour
{
    public GameObject Player;
    
    public int[,] matrix_x = new int[7, 14];
    public int[,] matrix_y = new int[7, 14];

    public GameObject waypoint;
    public GameObject[,] waypoint_array=new GameObject[7,14];
    public Transform Parent;

    public GameObject textManager;
    public int deadEnemy;
    [HideInInspector]public GameObject instTextManager;
    
    public int nrWave;
    int nrWaveCopy;
    [HideInInspector] public int actualWave=1;
    public int levelNumber;

    public bool endingOnce = true;

    void Start()
    {
        PlayerPrefs.SetInt("lastLevel", levelNumber);
        int x = -13, y = 9;
        for (int i = 0; i <= 6; ++i)
        {
            for (int j = 0; j <= 13; ++j)
            {
                matrix_x[i, j] = x;
                matrix_y[i, j] = y;
                x += 2;
            }
            y-=2;
            x = -13;
        }

        for (int i = 0; i <= 6; ++i)
            matrix_x[i, 0] -= 2;
        for (int i = 0; i <= 6; ++i)
            matrix_x[i, 13] += 2;
        for(int j=0; j <= 13; ++j)
            matrix_y[0,j] +=2;

        for (int i = 0; i <= 6; ++i)
        {
            for (int j = 0; j <= 13; ++j)
            {
                waypoint_array[i,j] = Instantiate(waypoint, new Vector3(matrix_x[i, j], matrix_y[i,j],0), transform.rotation) as GameObject;
                waypoint_array[i,j].transform.SetParent(Parent);
            }
        }
         instTextManager = Instantiate(textManager,new Vector3(0,0,0), transform.rotation) as GameObject;
        instTextManager.transform.SetParent(Parent);
        nrWave--;
        nrWaveCopy = nrWave;
    }
    public void ChangeColor()
    {
        string tempString = PlayerPrefs.GetString("color");
        if (tempString[levelNumber-1] == 'r')
        {
            tempString = tempString.Remove(levelNumber -1, 1);
            tempString = tempString.Insert(levelNumber -1, "g");
        }
        PlayerPrefs.SetString("color", tempString);
    }
    private void FixedUpdate()
    {
        if(instTextManager==false&&nrWave!=0)
        {
            actualWave =nrWaveCopy- nrWave+1;
            nrWave--;
            instTextManager = Instantiate(textManager, new Vector3(0, 0, 0), transform.rotation) as GameObject;
            instTextManager.transform.SetParent(Parent);
        }
        if (instTextManager == false && nrWave == 0 && endingOnce==true && Player==true) 
        { 
            ChangeColor();
            LevelManager.instance.GameWon();
            endingOnce = false; 
        }
    }


}
