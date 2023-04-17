using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;


public class Gun : MonoBehaviour
{
    public Transform bulletSpawn;

    public GameObject bulletPrefab;

    public float speed = 20;

    private float shootTimer = 0.5f;

    public Player playerScript;
    public Transform playerTransform;
    public Enemy enemyScript;

    private GameObject gun;

    public Animator muzFRev;
    public GameObject muzAnim;

    public bool isReloading = false;

    private int magazineMax = 6;
    public int magazineCur = 0;
    private int magNeeded;

    public AudioClip shooting;
    public AudioClip noAmmo;
    public AudioClip reloading;
    public AudioSource audioSource;

    //variables for on sound enemy follow
    public bool heardPlayer;
    public Collider2D[] possibleEnemiesWhoHeardMe;
    public GameObject temporaryGunObject;
    public int range = 100;
    private LayerMask enemyLayer;

    public static Gun instance;

    private void Awake()
    {
        instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        gun = GameObject.Find("Gun");
        playerScript = GameObject.Find("Sprite").GetComponent<Player>();
        playerTransform = GameObject.Find("PlayerTransform").GetComponent<Transform>();
        enemyScript = GameObject.Find("EnemySprite").GetComponent<Enemy>();
        enemyLayer = LayerMask.GetMask("Enemy");

        muzFRev = muzAnim.GetComponent<Animator>();
    }

    void Update()
    {
        possibleEnemiesWhoHeardMe = Physics2D.OverlapCircleAll(transform.position, range, enemyLayer);

        magNeeded = magazineMax - magazineCur;

        shootTimer -= Time.deltaTime;

        if (shootTimer >= 0.05f)
            muzFRev.SetBool("Shot", false);

        if (playerScript.hasGun)
        {
            if(magazineCur > 0 && playerScript.ammoCount >= 0 && isReloading == false)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (shootTimer < 0)
                    {
                        muzFRev.SetBool("Shot", true);
                        audioSource.PlayOneShot(shooting, 0.5f);
                        FirePistol();
                        shootTimer = 0.4f;
                        magazineCur--;

                        Destroy(temporaryGunObject);
                        foreach (Collider2D Enemy in possibleEnemiesWhoHeardMe)
                        {
                            {
                                temporaryGunTransform(gun, transform);
                                heardPlayer = true;
                            }
                        }
                    }
                }
            }
            if(magazineCur == 0 && isReloading == false)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    audioSource.PlayOneShot(noAmmo, 0.5f);
                    shootTimer = 0.4f;
                }
            }
        }

        if (!isReloading && magazineCur < magazineMax && playerScript.ammoCount > 0)
        {
            if (Input.GetKeyUp(KeyCode.R))
            {
                audioSource.PlayOneShot(reloading, 0.5f);
                isReloading = true;
                Invoke("Reload", 3.7f);
            }
        }

        //THIS IS FOR TESTING DELETE LATER
        //if (Input.GetKeyUp(KeyCode.Q))
        //{
        //    audioSource.PlayOneShot(shooting, 0.5f);
        //    Debug.Log("pew");
        //}
        //END OF THE TESTING SCRIPT

        //if (Input.GetKeyUp(KeyCode.Q))
        //{
            
        //}
    }

    public void temporaryGunTransform(GameObject obj, Transform newTransform)
    {
        GameObject tempObj = new GameObject("Temp Transform");
        tempObj.transform.position = obj.transform.position;
        tempObj.transform.rotation = obj.transform.rotation;

        temporaryGunObject = tempObj;

        StartCoroutine(DestroyTempTransform(/*obj, */tempObj, 5));
    }

    IEnumerator DestroyTempTransform(/*GameObject obj, */GameObject tempObj, float duration)
    {
        yield return new WaitForSeconds(duration);

        //obj.transform.parent = null;
        //obj.transform.position = tempObj.transform.position;
        //obj.transform.rotation = tempObj.transform.rotation;

        Destroy(tempObj);
    }

    void Reload()
    {
        if(magazineCur < magazineMax)
        {
            magNeeded = magazineMax - magazineCur;
            magazineCur += Math.Min(magNeeded, playerScript.ammoCount);
            isReloading = false;
            playerScript.ammoCount -= Math.Min(magNeeded, playerScript.ammoCount);
        }


        /*
        if(magazineCur == 0)
        {
            magazineCur = playerScript.ammoCount;
            playerScript.ammoCount = 0;
            isReloading = false;
        }
        */

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
