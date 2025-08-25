using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowThePath : MonoBehaviour
{
    //[ReadOnlyInspectorAtribute]
    [SerializeField]
    [HideInInspector]
    private Vector3[] waypoints=new Vector3[40];
    
    [SerializeField]
    private float moveSpeed ;

    private int waypointIndex = 0;
    private int length=0;

    public float rotationSpeed;
    //[ReadOnlyInspectorAtribute]
    [SerializeField]
    [HideInInspector]
    private float rotationModifier=-90;

    public bool canShoot=false;

    int index;
    [HideInInspector] public GameObject grampa;
    [HideInInspector]public bool stop = false;

    public bool exhaust=false;

    private void Start()
    {
        exhaust = true;
        grampa = transform.parent.gameObject.GetComponent<GetText>().Grampa;
        index = transform.parent.gameObject.GetComponent<GetText>().index+ transform.parent.gameObject.GetComponent<GetText>().sumeGrupuri[transform.parent.gameObject.GetComponent<GetText>().m];//-1 inainte sa creasca index
        length = transform.parent.gameObject.GetComponent<GetText>().enemyInformation[index].PathLength;
        int x;
        int y;
        for(int j=0;j<length;++j)
        {
            x = transform.parent.gameObject.GetComponent<GetText>().coordonates_x[index, j];
            y = transform.parent.gameObject.GetComponent<GetText>().coordonates_y[index, j];
            waypoints[j] = grampa.GetComponent<Level>().waypoint_array[x, y].transform.position;
        }
        transform.position = waypoints[waypointIndex];
        RotateTowards(waypoints[waypointIndex + 1],1000,false);
    }
    IEnumerator Waiter()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(0f, 3f));
        canShoot = true;
    }
    void RotateTowards(Vector3 V,float rotationSpeed,bool endingScript)
    {
        Vector3 vectorToTarget = V - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - rotationModifier;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotationSpeed);
        if (endingScript && transform.rotation == Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotationSpeed)) 
        {
            StartCoroutine(Waiter());    
            GetComponent<FollowThePath>().enabled = false;
            stop = true;
        }
        

    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (waypointIndex <= length - 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex], moveSpeed * Time.deltaTime);
            RotateTowards(waypoints[waypointIndex], rotationSpeed,false);
            if (transform.position == waypoints[waypointIndex])
            {
                waypointIndex += 1;
            }
        }
        else
        {
            exhaust = false;
            RotateTowards(new Vector3(transform.position.x, transform.position.y - 1, 0), rotationSpeed, true);
        }
    }
}