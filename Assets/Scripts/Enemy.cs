using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float hp = 100;
    public GameObject[] PowerUp = new GameObject[1];
    public float rnd1,rnd2;
    public GameObject Explosion;

    void Start()
    {
        rnd1 = UnityEngine.Random.Range(0f, 4f);
        rnd2 = UnityEngine.Random.Range(-100f, 100f);
    }
    public void TakeDamage(int damage)
    {
        hp -= damage;

    }
    void CreateExplosion()
    {
        GameObject instExplosion = Instantiate(Explosion, new Vector3(transform.position.x,transform.position.y- 2*Convert.ToInt32(GetComponent<FollowThePath>().stop), transform.position.z), transform.rotation) as GameObject; ;
    }
    void CreatePowerUp()
    {
        GameObject instPowerUp;
        if (UnityEngine.Random.Range(0f, 10f) > 5)
        {
            instPowerUp = Instantiate(PowerUp[UnityEngine.Random.Range(0, 3)], transform.position, transform.rotation) as GameObject;
        }
        
    }
    public void FixedUpdate()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
            CreateExplosion();
            CreatePowerUp();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8) 
        {
            if (other.gameObject.name == "Projectile_1(Clone)" && other.gameObject.layer==8) TakeDamage(10);
            if (other.gameObject.name == "Projectile_2(Clone)" && other.gameObject.layer == 8) TakeDamage(70);
            

        }
    }
}
