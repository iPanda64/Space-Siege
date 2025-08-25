using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RotateTowardsObject : MonoBehaviour
{
    public GameObject level;
    public GameObject Object;

    public float rotationSpeed;
    float rotationModifier = -90;
    // Start is called before the first frame update
    void Start()
    {
        level = GetComponent<FollowThePath>().grampa;
        Object = level.GetComponent<Level>().Player;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!GetComponent<FollowThePath>().enabled&&Object==true)
        {
                Vector3 vectorToTarget = Object.transform.position - transform.position;
                float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - rotationModifier;
                Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotationSpeed);

        }
    }
}
