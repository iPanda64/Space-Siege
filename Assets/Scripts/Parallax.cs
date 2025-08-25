using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float scrollingSpeed;

    void FixedUpdate()
    {
        float distance = 0;
        if (transform.position.y <= -20) distance = 40;
        transform.position = new Vector3(transform.position.x , transform.position.y - Time.deltaTime * scrollingSpeed+distance, transform.position.z);
    }
}
