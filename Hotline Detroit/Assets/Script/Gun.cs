using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class Gun : MonoBehaviour
{
    public Transform bulletSpawn;

    public GameObject bulletPrefab;

    public float speed = 20;

    private float shootTimer;

    public Player playerScript;
    public Transform playerTransform;
    public Enemy enemyScript;

    private GameObject gun;

    public FieldOfVision1 fov1;
    public AIDestinationSetter aiDest;
    

    private int magazineMax = 7;
    public int magazineCur;
    private int magNeeded;

    public AudioClip shooting;
    public AudioClip reloading;
    public AudioSource audioSource;

    //variables for on sound enemy follow
    public Collider2D[] possibleEnemiesWhoHeardMe;
    public GameObject temporaryGunObject;
    public int range = 100;
    private LayerMask enemyLayer;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        magazineCur = 0;

        shootTimer = 0.5f;

        gun = GameObject.Find("Gun");
        playerScript = GameObject.Find("Sprite").GetComponent<Player>();
        playerTransform = GameObject.Find("PlayerTransform").GetComponent<Transform>();
        enemyScript = GameObject.Find("EnemySprite").GetComponent<Enemy>();
        fov1 = GameObject.Find("Enemy").GetComponent<FieldOfVision1>();
        aiDest = GameObject.Find("Enemy").GetComponent<AIDestinationSetter>();
        enemyLayer = LayerMask.GetMask("Enemy");
    }

    void Update()
    {
        magNeeded = magazineMax - magazineCur;

        shootTimer -= Time.deltaTime;

        if (playerScript.hasGun)
        {
            if(magazineCur > 0)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (shootTimer < 0)
                    {
                        audioSource.PlayOneShot(shooting, 0.5f);
                        FirePistol();
                        shootTimer = 0.4f;
                        magazineCur--;
                    }
                }
            }
        }
        if(Input.GetKeyUp(KeyCode.R))
        {
            audioSource.PlayOneShot(reloading, 0.5f);
            Invoke("Reload", 2);
        }

        //THIS IS FOR TESTING DELETE LATER
        //if (Input.GetKeyUp(KeyCode.Q))
        //{
        //    audioSource.PlayOneShot(shooting, 0.5f);
        //    Debug.Log("pew");
        //}
        //END OF THE TESTING SCRIPT

        possibleEnemiesWhoHeardMe = Physics2D.OverlapCircleAll(transform.position, range, enemyLayer);
        if (Input.GetKeyUp(KeyCode.Q))
        {
            foreach (Collider2D Enemy in possibleEnemiesWhoHeardMe)
            {
                {
                    temporaryGunTransform(gun, transform);
                    fov1.heardPlayer = true;
                    Debug.Log("pewpew");
                }
            }
        }
    }

    public void temporaryGunTransform(GameObject obj, Transform newTransform)
    {
        GameObject tempObj = new GameObject("Temp Transform");
        tempObj.transform.position = obj.transform.position;
        tempObj.transform.rotation = obj.transform.rotation;

        temporaryGunObject = tempObj;
    }

    void Reload()
    {
        if(playerScript.ammoCount >= 7)
        {
            magazineCur += magazineMax;
            playerScript.ammoCount -= 7;
        }
        if(magazineCur == 0)
        {
            magazineCur = playerScript.ammoCount;
            playerScript.ammoCount = 0;
        }
        
        /*
        if(playerScript.ammoCount >= magNeeded)
        {
            magazineCur += magNeeded;
            playerScript.ammoCount -= magNeeded;
        }
        */
        
    }

    void FirePistol()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(bulletSpawn.right * speed, ForceMode2D.Impulse);
    }
}
