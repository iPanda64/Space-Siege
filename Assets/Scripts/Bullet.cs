using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject hit;
    
    void CreateHit()
    {
        GameObject instHit = Instantiate(hit, transform.position, transform.rotation) as GameObject;
        Rigidbody instHitRigidbody = instHit.GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        
        if (transform.position.y > 1080 / 140 + 1 || transform.position.y < -(1080 / 140 + 1)|| transform.position.x >= 1920 / 140 + 2|| transform.position.x <= -(1920 / 140 + 2)) Destroy(gameObject);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.layer == 7 && gameObject.layer==8)
        {
            
            Destroy(gameObject);
            CreateHit();
        }
        if (other.gameObject.layer == 6 && gameObject.layer == 10)
        {

            Destroy(gameObject);
            CreateHit();
        }

    }
}
