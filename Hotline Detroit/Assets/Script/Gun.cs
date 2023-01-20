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

    void Start()
    {
        magazineCur = 0;

        shootTimer = 0.5f;

        playerScript = GameObject.Find("Sprite").GetComponent<Player>();
    }

    void Update()
    {
        shootTimer -= Time.deltaTime;

        if (playerScript.hasGun)
        {
            if(magazineCur > 0)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (shootTimer < 0)
                    {
                        Fire();
                        shootTimer = 0.4f;
                        magazineCur--;
                    }
                }
            }
        }
        if(Input.GetKey(KeyCode.R))
        {
            //Reload();
            Invoke("Reload", 2);
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
    }

    void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(bulletSpawn.right * speed, ForceMode2D.Impulse);
    }
}
