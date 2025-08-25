using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exhaust : MonoBehaviour
{
    float verticalInput;
    public GameObject gameObjectToMove;
    public GameObject parent;
    public bool ePlayer;
    void Update()
    {
        if (ePlayer)
        {
            verticalInput = Input.GetAxis("Vertical");
            if (verticalInput > 0)
            {
                gameObject.GetComponent<ParticleSystem>().enableEmission = true;
            }
            else
            if (verticalInput <= 0)
            {
                gameObject.GetComponent<ParticleSystem>().enableEmission = false;
            }
        }
        else
        {
            gameObject.GetComponent<ParticleSystem>().enableEmission = parent.GetComponent<FollowThePath>().exhaust;
        }
    }
}
