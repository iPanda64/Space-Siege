using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using JetBrains.Annotations;
using System.Runtime.CompilerServices;

public class PlayerModel : MonoBehaviour
{
    private float horizontalInput;
    private Rigidbody rigidbodyComponent;

    Vector3 m_EulerAngleVelocity;
    private float rotation_speed;
    private float rotation_speed_value = 400;
    int max_rotation_left = 0;
    int max_rotation_right = 0;
    int reached_normal_rotation = 0;

    public GameObject mainBulletEmitter;
    public Transform parent;
    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
    }
    void Update()
    {

        horizontalInput = Input.GetAxisRaw("Horizontal");
 
    }

    
    
    private void HorizontalRotation()
    {
        if (horizontalInput < 0)//left
        {
            if (max_rotation_left == 0) rotation_speed = rotation_speed_value;
            else rotation_speed = 0;
            reached_normal_rotation = 0;
        }
        if (horizontalInput > 0)//right
        {
            if (max_rotation_right == 0) rotation_speed = -rotation_speed_value;
            else rotation_speed = 0;
            reached_normal_rotation = 0;
        }
        if (horizontalInput == 0 && reached_normal_rotation == 0)
        {
            if (transform.localRotation.y > 0.05) rotation_speed = -rotation_speed_value;
            else if (transform.localRotation.y < -0.05) rotation_speed = rotation_speed_value;
            else
                       if (transform.localRotation.y < 0.05 || transform.localRotation.y > -0.05) { reached_normal_rotation = 1; rotation_speed = 0; }
            else reached_normal_rotation = 0;

        }
        if (transform.localRotation.y > 0.40f) max_rotation_left = 1;
        else max_rotation_left = 0;
        if (transform.localRotation.y < -0.40f) max_rotation_right = 1;
        else max_rotation_right = 0;


        m_EulerAngleVelocity = new Vector3(0, rotation_speed, 0);
        Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.fixedDeltaTime);

        rigidbodyComponent.MoveRotation(rigidbodyComponent.rotation * deltaRotation);
    }
    //Fixed update is called once every phisics update (100times/sec)
    private void FixedUpdate()
    {
        transform.localPosition = Vector3.zero; 
        HorizontalRotation();
    }
    

}
