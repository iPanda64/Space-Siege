using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using JetBrains.Annotations;
using System.Runtime.CompilerServices;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float horizontalInput;
    float verticalInput;
    private Rigidbody rigidbodyComponent;
    private float horizontalSpeed;
    private float verticalSpeed;

    public float BulletEmitterCount = 1;

    public float timeBetweenShots = 0.2f;
    public int bulletType=0;

    private int youDied = 0;
    public int hp = 100;
    public GameObject Explosion;

    public int SpacePressed;
    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
    }
    void TakeDamage(int damage)
    {
        hp -= damage;
    }
    void CreateExplosion()
    {
        GameObject instExplosion = Instantiate(Explosion, new Vector3(transform.position.x, transform.position.y - 2, transform.position.z), transform.rotation) as GameObject; 
    }
    void YouDied()
    {
        if(transform.parent.GetComponent<Level>().endingOnce==true)
        LevelManager.instance.GameOver();
        Destroy(gameObject);
        CreateExplosion();
        youDied=1;

    }
    IEnumerator Waiter1()
    {
        yield return new WaitForSeconds(9);//time powerd up
        BulletEmitterCount--;
    }
    IEnumerator Waiter2()
    {
        yield return new WaitForSeconds(9);//time powerd up
        timeBetweenShots+=0.05f;
    }
    IEnumerator Waiter3()
    {
        yield return new WaitForSeconds(9);//time powerd up
        bulletType--;
    }
    void BulletEmitterBehaviour()
    {

        BulletEmitterCount++;
        StartCoroutine(Waiter1());
    }
    void FireRate()
    {
        
            timeBetweenShots -= 0.05f;
            
            StartCoroutine(Waiter2());
        
        
    }
    void BulletType()
    {
        
            bulletType++;
            StartCoroutine(Waiter3());
        
        
    }
    void Update()
    {

        if (Input.GetButtonDown("Jump")) SpacePressed = 1;
        if (Input.GetButtonUp("Jump") ) SpacePressed = 0;
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        //Horizontal (nume luat din input manager)
    }
    private void Movement()
    {
        
        horizontalSpeed = horizontalInput * 7f;//3
        verticalSpeed = verticalInput * 8.1f;//3.5
        
        if ((transform.position.x >= 1920/140+1.3 && horizontalInput > 0) || (transform.position.x <= -(1920 / 140 + 1.3) && horizontalInput < 0))
        {
            horizontalSpeed = 0f;
        }
        if ((transform.position.y >= 1080 / 140 +1  && verticalInput > 0) || (transform.position.y <= -(1080 / 140+1) && verticalInput < 0))
        {
            verticalSpeed = 0f;
        }
        rigidbodyComponent.velocity = new Vector3(horizontalSpeed , verticalSpeed  , 0);//  /300

    }
    private void FixedUpdate()
    {
        Movement();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            if (other.gameObject.name.Substring(0, 9) == "PowerUp_1") BulletEmitterBehaviour();
            if (other.gameObject.name.Substring(0, 9) == "PowerUp_2") FireRate();
            if (other.gameObject.name.Substring(0, 9) == "PowerUp_3") BulletType();
            Destroy(other.gameObject);
            
        }
        if (other.gameObject.layer == 7 && youDied==0)
        {
            hp = 0;
        }
        if ( other.gameObject.layer == 10) TakeDamage(5);
        if (hp == 0) YouDied();
    }
}
