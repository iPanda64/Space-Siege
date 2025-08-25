using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    //[ReadOnlyInspectorAtribute]
    [SerializeField]
    [HideInInspector]
    float health, maxhealth = 100;
    public GameObject player;
    public Image[] healthPoints;
    public Image healthBarBackground;
    public Text healthBarText;
    void Start()
    {

        health=maxhealth;
    }
    void TextUpdater()
    {
        if(health==100) healthBarText.text = health.ToString()+" | 100";
        if(health < 100&&health>9) healthBarText.text = " "+health.ToString() + " | 100";
        if (health < 10) healthBarText.text = "  " + health.ToString() + " | 100";
    }
    void Update()
    {
        
        if (player== false) health = 0;
        else health = player.GetComponent<Player>().hp;
        if (health > maxhealth)  health = maxhealth;
        HealthBarFiller();
        TextUpdater();
    }
    bool DisplayHealthPoint(float _health,int pointNumber)
    {
        return((pointNumber*10)>=_health);
    }
    void HealthBarFiller()
    {
        for(int i=0;i<healthPoints.Length;i++)
        {
            healthPoints[i].enabled=!DisplayHealthPoint(health,i);
        }
    }
}
