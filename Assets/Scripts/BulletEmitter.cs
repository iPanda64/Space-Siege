using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UIElements;

public class BulletEmitter : MonoBehaviour
{
    
    public GameObject[] bullet;
    public float speed ;
    private float gunHeat;
  
    int SpacePressed=0;

    public Vector3 relativeToPlayer=new Vector3(0,0,0);
    private Vector3 relativeToPlayerCopy = new Vector3(0,0,0);

    float horizontalInput;
    private bool isActive=true;

    public Transform parent;
    public int serialNumber;
    public float timeBetweenShots;
    private int bulletType;

    public bool isPlayer;
    public bool canShoot=false;

    public AudioSource source;
    public AudioClip[] clip;

    public GameObject[] muzzle;
    [SerializeField]
    int burst;
    int burstValue;
    float timeBetweenShotsB=0.2f;
    float gunHeatB;

    private void Start()
    {
        burstValue = burst;
        relativeToPlayerCopy=relativeToPlayer;
        source.volume = 0.05f;
        if (!isPlayer)
        {
            timeBetweenShots += transform.parent.GetComponent<Enemy>().rnd1;
            speed += transform.parent.GetComponent<Enemy>().rnd2;
            SpacePressed = 1;
        }
        if (isPlayer)
        {
            horizontalInput = parent.GetComponent<Player>().horizontalInput;
            canShoot = true;
        }
    }
    
    void NumberOfEmitters()
    {
            if (transform.parent.GetComponent<Player>().BulletEmitterCount == 1)
            {
                if (serialNumber == 2) isActive = true;
                else isActive = false;
            }
            if (transform.parent.GetComponent<Player>().BulletEmitterCount == 2)
            {
                if (serialNumber == 1 || serialNumber == 3) isActive = true;
                else isActive = false;
            }
            if (transform.parent.GetComponent<Player>().BulletEmitterCount >= 3)
            {
                isActive= true;
            }
    }
    void CreateMuzzle(int x)
    {
        GameObject instMuzzle = Instantiate(muzzle[x], transform.position, transform.rotation) as GameObject;
       
    }
    void CreateBullet(int x)
    {
        
        GameObject instBullet = Instantiate(bullet[x], transform.position, transform.rotation) as GameObject;
        Rigidbody instBulletRigidbody = instBullet.GetComponent<Rigidbody>();
        instBulletRigidbody.AddRelativeForce(Vector3.up*speed);
    }
    void FixPosition()
    {
        if (horizontalInput != 0)
        {
            
            relativeToPlayer.x = Math.Sign(relativeToPlayer.x) * (float)Math.Cos(200)+(relativeToPlayer.x - Math.Sign(relativeToPlayer.x) * (float)Math.Cos(200)) / 2;
        }
        if(horizontalInput ==0)
        {
            relativeToPlayer.x += (relativeToPlayerCopy.x-relativeToPlayer.x)/2;
        }
        transform.localPosition = relativeToPlayer;

    }
    private void Update()
    {
        if (isPlayer)
        {
            horizontalInput = parent.GetComponent<Player>().horizontalInput;
            SpacePressed = parent.GetComponent<Player>().SpacePressed;
        }
    }
    void FixedUpdate()
    {
        if (canShoot)
        {
            if (isPlayer)
            {
                bulletType = transform.parent.GetComponent<Player>().bulletType;
                if (bulletType > 1) bulletType = 1;
                timeBetweenShots = transform.parent.GetComponent<Player>().timeBetweenShots;
                FixPosition();
                NumberOfEmitters();
            }
            if (timeBetweenShots < 0.1f) timeBetweenShots = 0.1f;
            if (gunHeat > 0) gunHeat -= Time.deltaTime;
            if (gunHeat <= 0)
            {
                
                if (burst == 0)
                {

                    if (burstValue == 0)
                    {

                        if (SpacePressed == 1)
                        {

                            source.PlayOneShot(clip[bulletType]);
                            if (isActive)
                            {
                                CreateBullet(bulletType);
                                CreateMuzzle(bulletType);
                            }
                            gunHeat = timeBetweenShots + bulletType * 3 * timeBetweenShots;
                        }
                    }
                    else
                    {
                        burst = burstValue;
                        gunHeat = timeBetweenShots + bulletType * 3 * timeBetweenShots;
                    }
                }
                else
                {
                    if (timeBetweenShotsB < 0.1f) timeBetweenShotsB = 0.1f;
                    if (gunHeatB > 0) gunHeatB -= Time.deltaTime;
                    if (gunHeatB <= 0)
                    {
                            gunHeatB = timeBetweenShotsB ;
                            if (SpacePressed == 1)
                            {

                                source.PlayOneShot(clip[bulletType]);

                            }
                            if (SpacePressed == 1 && isActive)
                            {
                                CreateBullet(bulletType);
                                CreateMuzzle(bulletType);
                            }
                        burst--;
                    }
                }
                    
            }
        }
        else
            canShoot = transform.parent.GetComponent<FollowThePath>().canShoot;
    }
}
