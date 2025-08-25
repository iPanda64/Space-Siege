using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private Rigidbody rigidbodyComponent;
    // Start is called before the first frame update
    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
        rigidbodyComponent.AddForce(Vector3.down * 50);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(transform.position.y < -(1080 / 140 + 1)) Destroy(gameObject);
    }
}
