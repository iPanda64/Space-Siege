using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelFade : MonoBehaviour
{
    bool opaque =false;
    CanvasGroup canvGroup = new CanvasGroup();
    float fadeSpeed = 2f;
    [SerializeField] GameObject darkScreen;
    [SerializeField] int lose_1_win_2;


    private void Start()
    {
        canvGroup = GetComponent<CanvasGroup>();
        if (gameObject.name == darkScreen.name) fadeSpeed = 3f;
        canvGroup.alpha = 0;
    }
    
    private void FixedUpdate()
    {
            if(!opaque)canvGroup.alpha += Time.deltaTime/fadeSpeed;
            else
                if(gameObject.name!=darkScreen.name)canvGroup.alpha -= Time.deltaTime / fadeSpeed;
                else if(lose_1_win_2==1)SceneManager.LoadScene("RestartMenu");
                        else if(lose_1_win_2==2) SceneManager.LoadScene("ContinueMenu");
        if (canvGroup.alpha ==1)opaque = true;
            if(opaque && canvGroup.alpha== 0)
            {
            darkScreen.SetActive(true);
            gameObject.SetActive(false);
                
            }
    }
}
