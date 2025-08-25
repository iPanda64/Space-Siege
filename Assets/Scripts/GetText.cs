
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;

public class GetText : MonoBehaviour
{
    //[SerializeField] Text txt_allnames, txt_randomStudent;
    string[] fileString;

    //[ReadOnlyInspectorAtribute]
    [SerializeField]
    [HideInInspector]
    char[][] fileChar=new char[200][];

    GameObject instEnemy1;
    [HideInInspector] public int deadEnemy = 0;

    //[ReadOnlyInspectorAtribute]
    [SerializeField]
    [HideInInspector]
    GameObject[] enemy = new GameObject[40];
    int enemyIndex = 0;

    //[ReadOnlyInspectorAtribute]
    [SerializeField]
    [HideInInspector]
    float timeBetweenSpawns = 0.3f;
    float spawnHeat;

    public Transform Parent;
    public GameObject Grampa;
    public GameObject[] Enemy=new GameObject[2];


    int nrGroups;

    //[ReadOnlyInspectorAtribute]
    [SerializeField]
    [HideInInspector]
    int[] nrEnemiesPerGroup=new int[40];
    public EnemyInfo[] enemyInformation=new EnemyInfo[40];
    public int[,] coordonates_x = new int[40, 40];
    public int[,] coordonates_y = new int[40, 40];
    int nrEnemies;
    [HideInInspector] public int index;
    [HideInInspector] public int maxGroup = 0;
    [HideInInspector] public int[] sumeGrupuri = new int[40];
    [HideInInspector] public int m=0;
    bool continueSpawnEnemy=false;

    bool enemiesDied;
    [SerializeField] int nrWaves;

    void ReadAsString(int txtNumber)
    {
        int level = Grampa.GetComponent<Level>().levelNumber;
        string fileName = "Wave"+txtNumber+".txt";
        string myFilePath = Application.streamingAssetsPath + "/" + "TextFiles" + "/" +"Level"+level+"/"+ fileName;
        fileString = File.ReadAllLines(myFilePath);
        for (int i = 0; i < fileString.Length; i++) fileString[i] += " ";
    }
    void StringToChar()
    {
        for (int i = 0; i < fileString.Length; i++)
        {
            char[] tempChar = fileString[i].ToCharArray();
            fileChar[i] = tempChar;
        }
    }
    int CharToNumber(int n, int i,int j)
    {
        return (n * 10 + (int)(fileChar[i][j] - '0'));
    }
    void CharToIntVariables()
    {
        int j = 0;
        while (fileChar[0][j] != ' ')
        {
            nrGroups = CharToNumber(nrGroups, 0, j);
            j++;
        }
        int nrGroupsCopy = nrGroups;
        for (j = 0; j < fileChar[1].Length; j++)
        {
            if (fileChar[1][j] != ' ') nrEnemiesPerGroup[nrGroups - nrGroupsCopy] = CharToNumber(nrEnemiesPerGroup[nrGroups - nrGroupsCopy], 1, j);
            else
            {
                nrEnemies += nrEnemiesPerGroup[nrGroups - nrGroupsCopy];
                nrGroupsCopy--;
            }
        }
        for (int i = 2; i < fileString.Length; i+=3)
        {
            index = (i - 2) / 3;
            enemyInformation[index].Type = (int)(fileChar[i][0] - '0');
            j = 0;
            while (fileChar[i + 1][j] != ' ')
            {
                enemyInformation[index].PathLength=CharToNumber(enemyInformation[index].PathLength, i+1, j);
                ++j;
            }
            int pathLengthCopy = enemyInformation[index].PathLength;
            bool yTurn=false;
            for (j = 0; j < fileChar[i+2].Length; j++)
            {
                
                if (fileChar[i + 2][j] != ' ')
                {
                    if (!yTurn) coordonates_x[index,enemyInformation[index].PathLength - pathLengthCopy]= CharToNumber(coordonates_x[index, enemyInformation[index].PathLength - pathLengthCopy],i+2,j);
                    if (yTurn) coordonates_y[index, enemyInformation[index].PathLength - pathLengthCopy] = CharToNumber(coordonates_y[index, enemyInformation[index].PathLength - pathLengthCopy], i + 2, j);
                }
                else
                {
                    if(yTurn==false)
                    {
                        yTurn = true;
                    }
                    else
                    {
                        yTurn = false;
                        pathLengthCopy--;
                    }
                }
            }
        }
    }
    void SumaAndMaxGrups()
    {
        sumeGrupuri[0] = 0;
        for (int i = 1; i < nrGroups; ++i) sumeGrupuri[i] =sumeGrupuri[i-1]+ nrEnemiesPerGroup[i-1];
        for (int i = 0; i < nrGroups; ++i) if (maxGroup < nrEnemiesPerGroup[i]) maxGroup = nrEnemiesPerGroup[i];
    }
    void Declaration(int waveNr)
    {
        ReadAsString(waveNr);
        StringToChar();
        CharToIntVariables();
        SumaAndMaxGrups();
        index = 0;
        spawnHeat = timeBetweenSpawns;
        enemiesDied = false;
    }
    void Start()
    {

        Grampa = gameObject.transform.parent.gameObject;
        Declaration(Grampa.GetComponent<Level>().actualWave+1);
    }
    void CreateEnemy(int Type)
    {
        instEnemy1 = Instantiate(Enemy[Type], new Vector3(-13,9,0), transform.rotation) as GameObject;
        instEnemy1.transform.SetParent(Parent);
        enemy[enemyIndex] = instEnemy1;
        enemyIndex++;
    }
    bool SpawnEnemy()
    {
        if (spawnHeat > 0) spawnHeat -= Time.deltaTime;
        if (spawnHeat <= 0 || continueSpawnEnemy)
        {
            if (continueSpawnEnemy) m++;
            spawnHeat = timeBetweenSpawns;
            for (int i = m; i < nrGroups; ++i)
             {
               if (nrEnemiesPerGroup[i] > 0)
                {
                   m = i;
                   CreateEnemy(enemyInformation[index + sumeGrupuri[i]].Type);
                   nrEnemiesPerGroup[i]--;
                   return true;
                }
              }
            index++;
        }
        m = 0;
        return false;
    }
    void NumberOfDeadEnemies()
    {
        for (int i = 0; i < enemyIndex; ++i)
        {
            if (enemy[i] == false)
            {
                enemiesDied = true;
                deadEnemy++;
                for (int j = i; j < enemyIndex; ++j)
                {
                    enemy[j] = enemy[j + 1];
                }
                enemyIndex--;
            }
        }
    }
    private void FixedUpdate()
    {
        if(index<=maxGroup)continueSpawnEnemy=SpawnEnemy();
        NumberOfDeadEnemies();
        if (enemyIndex == 0 && enemiesDied) Destroy(gameObject);
    }

}

