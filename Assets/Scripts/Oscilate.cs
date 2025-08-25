using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Oscilate : MonoBehaviour
{
    Vector3 m_EulerAngleVelocity;
    private Rigidbody rigidbodyComponent;
    [SerializeField]
    float rotation_speed_value = 200f;
    float rotation_speed;
    bool canShoot;
    int sgn;
    private void Start()
    {
        if (Random.value < 0.5f)
            sgn = 1;
        else
            sgn = -1;

        rotation_speed = sgn*rotation_speed_value;
            rigidbodyComponent = GetComponent<Rigidbody>();
        canShoot = transform.parent.GetComponent<FollowThePath>().canShoot;
    }
    void FixedUpdate()
    {
        if (canShoot)
        {
            m_EulerAngleVelocity = new Vector3(0, 0, rotation_speed);
            Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.fixedDeltaTime);
            rigidbodyComponent.MoveRotation(rigidbodyComponent.rotation * deltaRotation);
            if (transform.localEulerAngles.z > 270) rotation_speed = -rotation_speed_value;
            else if (transform.localEulerAngles.z < 90) rotation_speed = rotation_speed_value;
        }
        else canShoot = transform.parent.GetComponent<FollowThePath>().canShoot;
    }
}
