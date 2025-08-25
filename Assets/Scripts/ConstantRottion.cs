using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantRottion : MonoBehaviour
{

    float rotation_speed = 200f;
    Vector3 m_EulerAngleVelocity;
    private Rigidbody rigidbodyComponent;
    private void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
        m_EulerAngleVelocity = new Vector3(0, 0, rotation_speed);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.fixedDeltaTime);
        rigidbodyComponent.MoveRotation(rigidbodyComponent.rotation * deltaRotation);
    }
}
