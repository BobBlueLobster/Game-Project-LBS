using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Gun : MonoBehaviour
{
    public Transform bulletSpawn;

    public GameObject bulletPrefab;

    public float speed = 20;

    private float shootTimer;

    public Player playerScript;

    private int magazineMax = 7;
    public int magazineCur;
    private int magNeeded;

    public AudioClip shooting;
    public AudioClip reloading;
    public AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        magazineCur = 0;

        shootTimer = 0.5f;

        playerScript = GameObject.Find("Sprite").GetComponent<Player>();
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
            Invoke("Reload", 3);
        }
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
