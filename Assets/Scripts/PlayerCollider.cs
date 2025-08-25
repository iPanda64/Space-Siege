using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    float horizontalInput;
    Vector3 OriginalValue=new Vector3();
    Vector3 FinalValue = new Vector3();
    void Start()
    {
        OriginalValue =transform.localScale;
        FinalValue = transform.localScale;
        FinalValue.x= ((float)Math.Cos(200) * OriginalValue.x + 0.2f);
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
    }
    private void FixedUpdate()
    {
        if (horizontalInput != 0)
        { 
            gameObject.transform.localScale = FinalValue;
        }
        else
        { 
            gameObject.transform.localScale = OriginalValue;
        }
    }
}
